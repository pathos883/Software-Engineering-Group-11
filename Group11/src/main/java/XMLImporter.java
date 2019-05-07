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
//import DoctorSchedule;

public class XMLImporter {
    public LocalDateTime convertDate(String date) {
        String [] arr = date.split("-");
        return LocalDateTime.of(Integer.parseInt(arr[0]), Integer.parseInt(arr[1]), Integer.parseInt(arr[2]),0,0,0);

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
                    schedule.addDoctor(employee);
                }
            }
            //TODO generate shifts from the remaining information in the XML file


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
