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
    private XmlNode SkillsNode;
    private XmlNode ShiftTypesNode;
    private XmlNode PatternsNode;
    private XmlNode ContractsNode;
    private XmlNode EmployeesNode;
    private XmlNode CoverRequirementsNode;

    public EmployeeXMLFileWriter(string ID, DateTime startTime, DateTime endTime)
	{
		xmlDoc = new XmlDocument();
        rootNode = xmlDoc.CreateElement("Scheduling Period");
        XmlAttribute rootID = xmlDoc.CreateAttribute("ID");
        rootID.Value = ID;
        rootNode.Attributes.Append(rootID);
        XmlNode startNode = xmlDoc.CreateElement("StartTime");
        startNode.InnerText = ConvertDTDate(startTime);
        rootNode.AppendChild(startNode);
        XmlNode endNode = xmlDoc.CreateElement("EndTime");
        endNode.InnerText = ConvertDTDate(endTime);
        rootNode.AppendChild(endNode);
        SkillsNode = xmlDoc.CreateElement("Skills");
        rootNode.appendChild(SkillsNode);
        ShiftTypesNode = xmlDoc.CreateElement("Skills");
        rootNode.appendChild(ShiftTypesNode);
        PatternsNode = xmlDoc.CreateElement("Skills");
        rootNode.appendChild(PatternsNode);
        ContractsNode = xmlDoc.CreateElement("Skills");
        rootNode.appendChild(ContractsNode);
        EmployeesNode = xmlDoc.CreateElement("Skills");
        rootNode.appendChild(EmployeesNode);
        CoverRequirementsNode = xmlDoc.CreateElement("Skills");
        rootNode.appendChild(CoverRequirementsNode);
    }

    /* 
     * Write Skill writes the given name as a type of skill
     * an employee can have. Shifts and employees are assigned 
     * skills and only an employee with all the skills assigned
     * to a shift can work that shift.
     */
    public void WriteSkill(string skillName)
    {
        XmlNode skillNode = XmlDoc.CreateElement("Skill");
        skillNode.InnerText = skillName;
        SkillsNode.appendChild(skillNode);
    }


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
        ShiftTypesNode.appendChild(startNode);
        XmlNode endNode = xmlDoc.CreateElement("EndTime");
        endNode.InnerText = ConvertDTTime(endTime);
        ShiftTypesNode.appendChild(endNode);
        XmlNode descNode = xmlDoc.CreateElement("Description");
        descNode.InnerText = desc;
        ShiftTypesNode.appendChild(descNode);
        XmlNode shiftSkillsNode = xmlDoc.CreateElement("Skills");
        foreach(string i in skills)
        {
            XmlNode shiftSkillNode = xmlDoc.CreateElement("Skill");
            shiftSkillNode.InnerText = i;
            shiftSkillsNode.appendChild(shiftSkillNode);
        }
        ShiftTypesNode.appendChild(shiftSkillsNode);
    }

    /*
     * Patterns are slightly more complex in that they are
     * nested. A pattern is made up of PatternEntries,
     * which are a ShiftType (a shiftID), and a Day.
     * A Day can be a day of the week, Any, None.
     */
    //public void WritePattern(string patternID)

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
        XmlAttribute ID = xmlDoc.CreateAttribute("ID");
        ID.Value = employeeID;
        XmlNode contractNode = xmlDoc.CreateElement("ContractID");
        contractNode.InnerValue = contractID;
        employeeNode.appendChild(contractNode);
        XmlNode nameNode = xmlDoc.CreateElement("Name");
        contractNode.InnerValue = name;
        employeeNode.appendChild(nameNode);
        XmlNode employeeSkillsNode = xmlDoc.CreateElement("Skills");
        foreach (string i in skills)
        {
            XmlNode employeeSkillNode = xmlDoc.CreateElement("Skill");
            employeeSkillNode.InnerText = i;
            employeeSkillsNode.appendChild(employeeSkillNode);
        }
        ShiftTypesNode.appendChild(employeeSkillsNode);
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
    }
}
