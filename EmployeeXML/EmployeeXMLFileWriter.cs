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
    //private XmlNode SkillsNode;
    private XmlNode ShiftTypesNode;
    //private XmlNode PatternsNode;
    //private XmlNode ContractsNode;
    private XmlNode EmployeesNode;
    //private XmlNode CoverRequirementsNode;
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
        /*SkillsNode = xmlDoc.CreateElement("Skills");
        rootNode.AppendChild(SkillsNode);
        ShiftTypesNode = xmlDoc.CreateElement("ShiftTypes");
        rootNode.AppendChild(ShiftTypesNode);
        PatternsNode = xmlDoc.CreateElement("Patterns");
        rootNode.AppendChild(PatternsNode);
        ContractsNode = xmlDoc.CreateElement("Contracts");
        rootNode.AppendChild(ContractsNode);*/
        EmployeesNode = xmlDoc.CreateElement("Employees");
        rootNode.AppendChild(EmployeesNode);
        /*CoverRequirementsNode = xmlDoc.CreateElement("CoverRequirements");
        rootNode.AppendChild(CoverRequirementsNode);*/
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
     * Write Skill writes the given name as a type of skill
     * an employee can have. Shifts and employees are assigned 
     * skills and only an employee with all the skills assigned
     * to a shift can work that shift.
     *
    public void WriteSkill(string skillName)
    {
        XmlNode skillNode = xmlDoc.CreateElement("Skill");
        skillNode.InnerText = skillName;
        SkillsNode.AppendChild(skillNode);
    } */


    /*
     * WriteShift defines the types of shifts that exist so that
     * the shifts can be refrenced in Patterns, Contracts, and
     * ShiftOff Requests. The ShiftID is how the shift is refrenced
     * later. Desc for the description of the shift and skills[]
     * for any required skills needed for this shift type.
     */
    public void WriteShift(string shiftID, DateTime startTime, DateTime endTime, string desc, string[] skills)
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
     * Patterns are slightly more complex in that they are
     * nested. A pattern is made up of PatternEntries,
     * which are a ShiftType (a shiftID), and a Day.
     * A Day can be a day of the week, Any, None.
     * patternEntries is an array of [x][2], where [x][0] is 
     * the shifttype and [x][1] is the day
     *
    public void WritePattern(string patternID, string patternWeight, string[][] patternEntries)
	{
		XmlNode patternNode = xmlDoc.CreateElement("Pattern");
		XmlAttribute idAttribute = xmlDoc.CreateAttribute("ID");
		idAttribute.Value = patternID;
		patternNode.Attributes.Append(idAttribute);
		XmlAttribute weightAttribute = xmlDoc.CreateAttribute("weight");
		weightAttribute.Value = patternWeight;
		patternNode.Attributes.Append(weightAttribute);
		XmlNode patternEntriesNode = xmlDoc.CreateElement("PatternEntries");
		for (int i = 0; i < patternEntries.Length; i++)
		{
			XmlNode patternEntryNode = xmlDoc.CreateElement("PatternEntry");
			XmlAttribute patternEntryIndexAttribute = xmlDoc.CreateAttribute("index");
			patternEntryIndexAttribute.Value = "" + i;
			patternEntryNode.Attributes.Append(patternEntryIndexAttribute);
			XmlNode shiftTypeNode = xmlDoc.CreateElement("ShiftType");
			shiftTypeNode.InnerText = patternEntries[i][0];
			patternEntryNode.AppendChild(shiftTypeNode);
			XmlNode dayNode = xmlDoc.CreateElement("Day");
			dayNode.InnerText = patternEntries[i][1];
			patternEntryNode.AppendChild(dayNode);
            patternNode.AppendChild(patternEntriesNode);
        }
        PatternsNode.AppendChild(patternNode);
	}*/

    /*
     * Contracts are defined in the schema file.
     * More planning is needed before I get to
     * creating custom contracts will be possible.
     * It is likely that a contract class will be needed
     * to support a high-level custom contract.
     */
    //public void WriteContract(string contractID)

    /*
     * WriteEmployee takes an employee ID to be 
     * refrenced in Day Off and Shift Off requests.
     * Contract ID to specify the full-time status
     * employee's name
     * list of skills for the shifts they can work.
     */
    public void WriteEmployee(string employeeID, string contractID, string name, string[] skills)
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
        XmlNode employeeSkillsNode = xmlDoc.CreateElement("Skills");
        foreach (string i in skills)
        {
            XmlNode employeeSkillNode = xmlDoc.CreateElement("Skill");
            employeeSkillNode.InnerText = i;
            employeeSkillsNode.AppendChild(employeeSkillNode);
        }
        ShiftTypesNode.AppendChild(employeeSkillsNode);
    }
	
	/*
	 * Cover requirements defines how the shifts are laid out accross the week and how
     * many doctors are needed at each shift. Implementing this will increase the
     * flexibility of how many options there are to connfigure shifts
	 *
	public void WriteCoverRequirements(string day, string[][] covers)
	{
		XmlNode dayOfWeekCoverNode = xmlDoc.CreateElement("DayOfWeekCover");
		XmlNode dayNode = xmlDoc.CreateElement("Day");
		dayNode.InnerText = day;
		dayOfWeekCoverNode.AppendChild(dayNode);
		for (int i = 0; i < covers.Length; i++)
		{
			XmlNode coverNode = xmlDoc.CreateElement("Cover");
			XmlNode coverShiftNode = xmlDoc.CreateElement("Shift");
			coverShiftNode.InnerText = covers[i][0];
			coverNode.AppendChild(coverShiftNode);
			XmlNode coverPreferredNode = xmlDoc.CreateElement("Preferred");
			coverPreferredNode.InnerText = covers[i][1];
			coverNode.AppendChild(coverPreferredNode);
			dayOfWeekCoverNode.AppendChild(coverNode);
		}
		CoverRequirementsNode.AppendChild(dayOfWeekCoverNode);
	}*/
	
	/*
	 * WriteDayOffRequest takes the weight (priority) of the request, the
	 * employeeID of the person making the request, and the day that they
	 * request off in a DateTime.
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
