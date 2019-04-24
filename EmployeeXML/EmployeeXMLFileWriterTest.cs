using System;
using EmployeeXML;

namespace EmployeeXML.Testing
{ 
   class EmployeeXMLFileWriterTest
   {
       static void Main(string[] args)
        {
            string filename = "sprint01.xml";
            DateTime startTime = new DateTime(2010, 1, 1);
            DateTime endTime = new DateTime(2010, 1, 28);
            EmployeeXMLFileWriter testFile = new EmployeeXMLFileWriter("sprint01", startTime, endTime);
            testFile.CreateXml(filename);
        }
    }

}