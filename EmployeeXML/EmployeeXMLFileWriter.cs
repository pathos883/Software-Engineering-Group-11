using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// EmployeeXMLFileWriter
/// </summary>

public class EmployeeXMLFileWriter
{
    private XmlDocument xmlDoc;
    private XmlNode rootNode;
    private XmlNode ShiftTypesNode;
    private XmlNode EmployeesNode;
	private XmlNode DayOffRequestsNode;
	private XmlNode ShiftOffRequestsNode;

    public EmployeeXMLFileWriter(string ID, DateTime startTime, DateTime endTime)
	{
		xmlDoc = new XmlDocument();
        XmlDeclaration xml_declaration;
        xml_declaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
        XmlElement document_element = xmlDoc.DocumentElement;
        xmlDoc.InsertBefore(xml_declaration, document_element);
        rootNode = xmlDoc.CreateElement("SchedulingPeriod");
        XmlAttribute rootID = xmlDoc.CreateAttribute("ID");
        rootID.Value = ID;
        rootNode.Attributes.Append(rootID);
        XmlAttribute rootNamespace = xmlDoc.CreateAttribute("xmlns:xsi");
        rootNamespace.Value = "http://www.w3.org/2001/XMLSchema-instance";
        rootNode.Attributes.Append(rootNamespace);
        //Note: The prefix xsi is removed from the xml file. Hopefully this is not an issue.
        XmlAttribute rootSchema = xmlDoc.CreateAttribute("xsi:noNamespaceSchemaLocation");
        rootSchema.Value = "competition.xsd";
        rootNode.Attributes.Append(rootSchema);
        XmlNode startNode = xmlDoc.CreateElement("StartTime");
        startNode.InnerText = ConvertDTDate(startTime);
        rootNode.AppendChild(startNode);
        XmlNode endNode = xmlDoc.CreateElement("EndTime");
        endNode.InnerText = ConvertDTDate(endTime);
        rootNode.AppendChild(endNode);
        EmployeesNode = xmlDoc.CreateElement("Employees");
        rootNode.AppendChild(EmployeesNode);
		DayOffRequestsNode = xmlDoc.CreateElement("DayOffRequests");
        rootNode.AppendChild(DayOffRequestsNode);
		ShiftOffRequestsNode = xmlDoc.CreateElement("ShiftOffRequests");
        rootNode.AppendChild(ShiftOffRequestsNode);
        xmlDoc.AppendChild(rootNode);
    }

    public void CreateXml(string filename)
    {
        xmlDoc.Save(filename);
    }

    /*
     * WriteShift defines the Shifts that will occur every day between startTime and endTime.
     * The ShiftID is how the shift will be refrenced in ShiftOff Requests
     * startTime and endTime look at the hour, minute, second of the DateTime parameters
     * Desc for the description of the shift
     */
    public void WriteShift(string shiftID, DateTime startTime, DateTime endTime, string desc)
    {
        XmlNode shiftNode = xmlDoc.CreateElement("Shift");
        XmlAttribute ID = xmlDoc.CreateAttribute("ID");
        ID.Value = shiftID;
        XmlNode startNode = xmlDoc.CreateElement("StartTime");
        startNode.InnerText = ConvertDTTime(startTime);
        ShiftTypesNode.AppendChild(startNode);
        XmlNode endNode = xmlDoc.CreateElement("EndTime");
        endNode.InnerText = ConvertDTTime(endTime);
        ShiftTypesNode.AppendChild(endNode);
        XmlNode descNode = xmlDoc.CreateElement("Description");
        descNode.InnerText = desc;
        ShiftTypesNode.AppendChild(descNode);
        XmlNode shiftSkillsNode = xmlDoc.CreateElement("Skills");
        foreach(string i in skills)
        {
            XmlNode shiftSkillNode = xmlDoc.CreateElement("Skill");
            shiftSkillNode.InnerText = i;
            shiftSkillsNode.AppendChild(shiftSkillNode);
        }
        ShiftTypesNode.AppendChild(shiftSkillsNode);
    }

    /*
     * WriteEmployee takes an employee ID to be 
     * refrenced in Day Off and Shift Off requests.
     * Contract ID to specify the full-time status (1, .75, .6, .5, AN)
     * employee's name
     */
    public void WriteEmployee(string employeeID, string contractID, string name)
    {
        XmlNode employeeNode = xmlDoc.CreateElement("Employee");
        XmlAttribute idAttribute = xmlDoc.CreateAttribute("ID");
        idAttribute.Value = employeeID;
		employeeNode.Attributes.Append(idAttribute);
        XmlNode contractNode = xmlDoc.CreateElement("ContractID");
        contractNode.InnerText = contractID;
        employeeNode.AppendChild(contractNode);
        XmlNode nameNode = xmlDoc.CreateElement("Name");
        contractNode.InnerText = name;
        employeeNode.AppendChild(nameNode);
    }
	
	/*
	 * WriteDayOffRequest takes the weight (priority) of the request, the
	 * employeeID of the person making the request, and the day that they
	 * request off in a DateTime.
     * the parameter date will only look at the day, month, and year of the DateTime
	 */
	public void WriteDayOffRequest(string weight, string employeeID, DateTime date)
	{
		XmlNode dayOffNode = xmlDoc.CreateElement("DayOff");
		XmlAttribute weightAttribute = xmlDoc.CreateAttribute("weight");
		weightAttribute.Value = weight;
		dayOffNode.Attributes.Append(weightAttribute);
		XmlNode dayOffEmployeeNode = xmlDoc.CreateElement("EmployeeID");
		dayOffEmployeeNode.InnerText = employeeID;
		dayOffNode.AppendChild(dayOffEmployeeNode);
		XmlNode dayOffDateNode = xmlDoc.CreateElement("Date");
		dayOffDateNode.InnerText = ConvertDTDate(date);
		dayOffNode.AppendChild(dayOffDateNode);
		DayOffRequestsNode.AppendChild(dayOffNode);
	}

    /*
     * WriteDayOffRequest takes the weight (priority) of the request, the
      * employeeID of the person making the request, and the day that they
      * request off in a DateTime.
      * the parameter date will only look at the day, month, and year of the DateTime
      */
    public void WriteShiftOffRequest(string weight, string shiftID, string employeeID, DateTime date)
    {
        XmlNode shiftOffNode = xmlDoc.CreateElement("ShiftOff");
        XmlAttribute weightAttribute = xmlDoc.CreateAttribute("weight");
        weightAttribute.Value = weight;
        shiftOffNode.Attributes.Append(weightAttribute);
        XmlNode shiftOffShiftIDNode = xmlDoc.CreateElement("ShiftTypeID");
        shiftOffShiftIDNode.InnerText = shiftID;
        shiftOffNode.AppendChild(shiftOffShiftIDNode);
        XmlNode shiftOffEmployeeNode = xmlDoc.CreateElement("EmployeeID");
        shiftOffEmployeeNode.InnerText = employeeID;
        shiftOffNode.AppendChild(shiftOffEmployeeNode);
        XmlNode shiftOffDateNode = xmlDoc.CreateElement("Date");
        shiftOffDateNode.InnerText = ConvertDTDate(date);
        shiftOffNode.AppendChild(shiftOffDateNode);
        ShiftOffRequestsNode.AppendChild(shiftOffNode);
    }

    /*
     * ConvertDTDate takes in a DateTime and spits out the
     * Date in a string of the format yyyy-mm-dd,
     * the format needed for dates in this XML file.
     */
    public string ConvertDTDate(DateTime dt)
    {
        if (dt.Month < 10)
            return dt.Year + "-0" + dt.Month + "-" + dt.Day;
        else
            return dt.Year + "-" + dt.Month + "-" + dt.Day;
    }

    /*
     * ConvertDTTime takes in a DateTime and spits out the
     * Time in a string of the format hh:mm:ss,
     * the format needed for time in this XML file.
     */
    public string ConvertDTTime(DateTime dt)
    {
        string time = "";
        if (dt.Hour < 10)
            time += "0" + dt.Hour;
        else
            time += dt.Hour;
        if (dt.Minute < 10)
            time += ":0" + dt.Minute;
        else
            time += ":" + dt.Minute;
        if (dt.Minute < 10)
            time += ":0" + dt.Second;
        else
            time += ":" + dt.Second;
        return time;
    }
}
