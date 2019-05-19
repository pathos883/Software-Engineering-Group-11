package Schedule;
/*
 * XMLImporter.java
 * Tristan Gantz
 * Takes the xml file and populates the DoctorSchedule with the data before it is solved.
 */

import com.sun.jdi.connect.IllegalConnectorArgumentsException;
import org.w3c.dom.*;
        import org.xml.sax.*;

import javax.print.Doc;
import javax.xml.parsers.*;
        import javax.xml.xpath.*;
        import java.io.*;
import java.time.LocalDateTime;
import java.time.Period;
import java.util.ArrayList;

import static java.time.temporal.ChronoUnit.DAYS;
//import DoctorSchedule;

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
            for (int i = 0; i < shiftNodeList.getLength(); i++) {
                Node shiftNode = shiftNodeList.item(i);
                if (shiftNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element shiftElement = (Element) shiftNode;
                    String start = shiftElement.getElementsByTagName("StartTime").item(0).getTextContent();
                    String end = shiftElement.getElementsByTagName("EndTime").item(0).getTextContent();
                    shiftTypes.add(new ShiftType(convertTime(start), convertTime(end)));
                }
            }
            //generate shifts from the remaining information in the XML file
            for (int i = 0; i < DAYS.between(schedule.getStartDate(), schedule.getEndDate()); i++) {
                LocalDateTime day = schedule.getStartDate().plusDays(i);
                for (ShiftType s : shiftTypes) {
                    if (ShiftType.CrossesDays(s) && i == DAYS.between(schedule.getStartDate(), schedule.getEndDate()) + 1) {continue;}
                    else {
                        Shift shift = new Shift();
                        shift.setStartTime(s.getStartTime().plus(Period.of(day.getYear(), day.getMonth().getValue(), day.getHour())));
                        shift.setEndTime(s.getEndTime().plus(Period.of(day.getYear(), day.getMonth().getValue(), day.getHour())));
                        if (ShiftType.CrossesDays(s)) {
                            shift.setStartTime(shift.getEndTime().plusDays(1));
                        }
                        schedule.addShift(shift);
                    }
                }
            }
            //add all requests


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
