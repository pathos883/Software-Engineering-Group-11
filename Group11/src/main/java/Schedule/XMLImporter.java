package Schedule;
/*
 * XMLImporter.java
 * Tristan Gantz
 * Takes the xml file and populates the DoctorSchedule with the data before it is solved.
 */

import org.w3c.dom.*;
import org.xml.sax.*;
import javax.xml.parsers.*;
import javax.xml.xpath.*;
import java.io.*;
import java.time.LocalDateTime;
import java.time.Period;
import java.util.ArrayList;

import static java.time.temporal.ChronoUnit.DAYS;

public class XMLImporter {
    public LocalDateTime convertDate(String date) {
        String [] arr = date.split("-");
        return LocalDateTime.of(Integer.parseInt(arr[0]), Integer.parseInt(arr[1]), Integer.parseInt(arr[2]),0,0,0);
    }
    public LocalDateTime convertTime(String date) {
        String [] arr = date.split(":");
        return LocalDateTime.of(0,1,1,Integer.parseInt(arr[0]), Integer.parseInt(arr[1]), Integer.parseInt(arr[2]));
    }

    /*
     * filename is the xml file with all the doctor and shift information
     * schedule is the schedule that will be populated with the data
     * upon return, the schedule will be populated with employees and shifts
     * as well as the dates that the shifts will cover.
     */
    public void readSolution(String fileName, DoctorSchedule schedule) {
        try {
            DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder dBuilder;

            dBuilder = dbFactory.newDocumentBuilder();

            File inputFile = new File(fileName);
            Document doc = dBuilder.parse(inputFile);
            doc.getDocumentElement().normalize();

            XPath xPath = XPathFactory.newInstance().newXPath();
            String rootExpression = "/SchedulingPeriod";
            Element rootElement = (Element) xPath.compile(rootExpression).evaluate(
                    doc, XPathConstants.NODE);
            schedule.setStartDate(convertDate(rootElement.getElementsByTagName("StartDate").item(0).getTextContent()));
            schedule.setEndDate(convertDate(rootElement.getElementsByTagName("EndDate").item(0).getTextContent()));

            String employeeExpression = "/SchedulingPeriod/Employees/Employee";
            NodeList employeeNodeList = (NodeList) xPath.compile(employeeExpression).evaluate(
                    doc, XPathConstants.NODESET);
            for (int i = 0; i < employeeNodeList.getLength(); i++) {
                Node employeeNode = employeeNodeList.item(i);
                if (employeeNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element employeeElement = (Element) employeeNode;
                    Doctor employee = new Doctor(Integer.parseInt(employeeElement.getAttribute("ID")), employeeElement.getElementsByTagName("Name").item(0).getTextContent());
                    employee.setHoursToWork(Integer.parseInt(employeeElement.getElementsByTagName("HoursRemaining").item(0).getTextContent()));
                    schedule.addDoctor(employee);
                }
            }
            String shiftExpression = "/SchedulingPeriod/ShiftTypes";
            NodeList shiftNodeList = (NodeList) xPath.compile(shiftExpression).evaluate(
                    doc, XPathConstants.NODE);
            ArrayList<ShiftType> shiftTypes = new ArrayList<ShiftType>();
            ArrayList<String> shiftTypeID = new ArrayList<String>();
            for (int i = 0; i < shiftNodeList.getLength(); i++) {
                Node shiftNode = shiftNodeList.item(i);
                if (shiftNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element shiftElement = (Element) shiftNode;
                    String start = shiftElement.getElementsByTagName("StartTime").item(0).getTextContent();
                    String end = shiftElement.getElementsByTagName("EndTime").item(0).getTextContent();
                    shiftTypes.add(new ShiftType(convertTime(start), convertTime(end)));
                    shiftTypeID.add(shiftElement.getAttribute("ID"));
                }
            }
            //generate shifts from the remaining information in the XML file
            for (int i = 0; i < DAYS.between(schedule.getStartDate(), schedule.getEndDate()); i++) {
                LocalDateTime day = schedule.getStartDate().plusDays(i);
                for (ShiftType s : shiftTypes) {
                    if (ShiftType.CrossesDays(s) && i == DAYS.between(schedule.getStartDate(), schedule.getEndDate()) + 1) {continue;}
                    else {
                        Shift shift = new Shift();
                        shift.setStartTime(s.getStartTime().plus(Period.of(day.getYear(), 0, day.getDayOfYear())));
                        shift.setEndTime(s.getEndTime().plus(Period.of(day.getYear(), 0, day.getDayOfYear())));
                        if (ShiftType.CrossesDays(s)) {
                            shift.setEndTime(shift.getEndTime().plusDays(1));
                        }
                        schedule.addShift(shift);
                    }
                }
            }
            //add all requests
            String shiftUnwantedRequestExpression = "/SchedulingPeriod/ShiftOffRequests";
            NodeList shiftUnwantedRequestNodeList = (NodeList) xPath.compile(shiftUnwantedRequestExpression).evaluate(
                    doc, XPathConstants.NODE);
            for (int i = 0; i < shiftUnwantedRequestNodeList.getLength(); i++) {
                Node shiftUnwantedRequestNode = shiftUnwantedRequestNodeList.item(i);
                if (shiftUnwantedRequestNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element requestElement = (Element) shiftUnwantedRequestNode;
                    Request shiftOff = new Request();
                    shiftOff.setWanted(false);
                    shiftOff.setPriority(Integer.parseInt(requestElement.getAttribute("weight")));
                    ShiftType s = shiftTypes.get(shiftTypeID.indexOf(requestElement.getElementsByTagName("ShiftTypeID").item(0).getTextContent()));
                    Doctor ID = null;
                    for (Doctor d : schedule.getDoctors()) {
                    	if(d.getID() == Integer.parseInt(requestElement.getElementsByTagName("EmployeeID").item(0).getTextContent())) {
                    		ID = d;
                    		break;
                    	}
                    }
                    if (ID == null) continue;
                    LocalDateTime day = convertDate(requestElement.getElementsByTagName("Date").item(0).getTextContent());
                    LocalDateTime start = s.getStartTime().plus(Period.of(day.getYear(), 0, day.getDayOfYear()));
                    LocalDateTime end = s.getEndTime().plus(Period.of(day.getYear(), 0, day.getDayOfYear()));
                    if (ShiftType.CrossesDays(s)) {
                    	end = end.plusDays(1);
                    }
                    shiftOff.setStart(start);
                    shiftOff.setEnd(end);
                    ID.addShiftRequests(shiftOff);
                }
            }
            
            String shiftWantedRequestExpression = "/SchedulingPeriod/ShiftOnRequests";
            NodeList shiftWantedRequestNodeList = (NodeList) xPath.compile(shiftWantedRequestExpression).evaluate(
                    doc, XPathConstants.NODE);
            for (int i = 0; i < shiftWantedRequestNodeList.getLength(); i++) {
                Node shiftWantedRequestNode = shiftWantedRequestNodeList.item(i);
                if (shiftWantedRequestNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element requestElement = (Element) shiftWantedRequestNode;
                    Request shiftOn = new Request();
                    shiftOn.setWanted(true);
                    shiftOn.setPriority(Integer.parseInt(requestElement.getAttribute("weight")));
                    ShiftType s = shiftTypes.get(shiftTypeID.indexOf(requestElement.getElementsByTagName("ShiftTypeID").item(0).getTextContent()));
                    Doctor ID = null;
                    for (Doctor d : schedule.getDoctors()) {
                    	if(d.getID() == Integer.parseInt(requestElement.getElementsByTagName("EmployeeID").item(0).getTextContent())) {
                    		ID = d;
                    		break;
                    	}
                    }
                    if (ID == null) continue;
                    LocalDateTime day = convertDate(requestElement.getElementsByTagName("Date").item(0).getTextContent());
                    LocalDateTime start = s.getStartTime().plus(Period.of(day.getYear(), 0, day.getDayOfYear()));
                    LocalDateTime end = s.getEndTime().plus(Period.of(day.getYear(), 0, day.getDayOfYear()));
                    if (ShiftType.CrossesDays(s)) {
                    	end = end.plusDays(1);
                    }
                    shiftOn.setStart(start);
                    shiftOn.setEnd(end);
                    ID.addShiftRequests(shiftOn);
                }
            }
            
            String dayUnwantedRequestExpression = "/SchedulingPeriod/DayOffRequests";
            NodeList dayUnwantedRequestNodeList = (NodeList) xPath.compile(dayUnwantedRequestExpression).evaluate(
                    doc, XPathConstants.NODE);
            for (int i = 0; i < dayUnwantedRequestNodeList.getLength(); i++) {
                Node dayUnwantedRequestNode = dayUnwantedRequestNodeList.item(i);
                if (dayUnwantedRequestNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element requestElement = (Element) dayUnwantedRequestNode;
                    Request dayOff = new Request();
                    dayOff.setWanted(false);
                    dayOff.setPriority(Integer.parseInt(requestElement.getAttribute("weight")));
                    Doctor ID = null;
                    for (Doctor d : schedule.getDoctors()) {
                    	if(d.getID() == Integer.parseInt(requestElement.getElementsByTagName("EmployeeID").item(0).getTextContent())) {
                    		ID = d;
                    		break;
                    	}
                    }
                    if (ID == null) continue;
                    LocalDateTime start = convertDate(requestElement.getElementsByTagName("Date").item(0).getTextContent());
                    LocalDateTime end = start.plusDays(1);
                    dayOff.setStart(start);
                    dayOff.setEnd(end);
                    ID.addShiftRequests(dayOff);
                }
            }
            
            String dayWantedRequestExpression = "/SchedulingPeriod/DayOnRequests";
            NodeList dayWantedRequestNodeList = (NodeList) xPath.compile(dayWantedRequestExpression).evaluate(
                    doc, XPathConstants.NODE);
            for (int i = 0; i < dayWantedRequestNodeList.getLength(); i++) {
                Node dayWantedRequestNode = dayWantedRequestNodeList.item(i);
                if (dayWantedRequestNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element requestElement = (Element) dayWantedRequestNode;
                    Request dayOn = new Request();
                    dayOn.setWanted(true);
                    dayOn.setPriority(Integer.parseInt(requestElement.getAttribute("weight")));
                    Doctor ID = null;
                    for (Doctor d : schedule.getDoctors()) {
                    	if(d.getID() == Integer.parseInt(requestElement.getElementsByTagName("EmployeeID").item(0).getTextContent())) {
                    		ID = d;
                    		break;
                    	}
                    }
                    if (ID == null) continue;
                    LocalDateTime start = convertDate(requestElement.getElementsByTagName("Date").item(0).getTextContent());
                    LocalDateTime end = start.plusDays(1);
                    dayOn.setStart(start);
                    dayOn.setEnd(end);
                    ID.addShiftRequests(dayOn);
                }
            }
            

        }catch (ParserConfigurationException e) {
            e.printStackTrace();
        } catch (SAXException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (XPathExpressionException e) {
            e.printStackTrace();
        }
    }
}
