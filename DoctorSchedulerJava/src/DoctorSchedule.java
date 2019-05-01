import org.optaplanner.core.api.domain.solution.PlanningEntityCollectionProperty;
import org.optaplanner.core.api.domain.solution.PlanningScore;
import org.optaplanner.core.api.domain.solution.PlanningSolution;
import org.optaplanner.core.api.score.buildin.hardsoft.HardSoftScore;

import java.util.ArrayList;
import java.io.PrintWriter;
import java.time.LocalDateTime;

@PlanningSolution
public class DoctorSchedule {
    //list of the doctors
    ArrayList<Doctor> doctors;

    //list of the days of shifts
    @PlanningEntityCollectionProperty
    ArrayList<Shift> shifts;

    //score of the shift
    @PlanningScore
    private HardSoftScore score;

    //function to create from xml
    void importXML(String fileName){

    }

    //function to export a csv
    void exportCSV(String fileName){
    	PrintWriter writer = new PrintWriter("calendar.csv");
    	
    	writer.println("Subject, Start Date, Start Time, End Date, End Time");
    	
    	for (Shift s : shifts) {
    		LocalDateTime startTime = s.getStartTime();
    		LocalDateTime endTime = s.getEndTime();
    		
    		// write name of doctor
    		writer.print(s.getShiftFile().getName());
    		writer.print(",");
    		
    		// write start date
    		writer.print(startTime.getMonthValue() + "/" + startTime.getDayOfMonth() + "/" + startTime.getYear());
    		writer.print(",");
    		
    		// write start tiem
    		int hour = startTime.getHour();
    		boolean pm = (hour / 12 == 1);
    		hour = hour % 12;
    		if (hour == 0) {
    			writer.print("12");
    		} else {
    			writer.print(hour);
    		}
    		writer.print(":");
    		writer.print(startTime.getMinute());
    		if (pm) {
    			writer.print("PM");
    		} else {
    			writer.print("AM");
    		}
    		writer.print(",");
    		
    		// write end date
    		writer.print(endTime.getMonthValue() + "/" + endTime.getDayOfMonth() + "/" + endTime.getYear());
    		writer.print(",");
    		
    		// write end tiem
    		hour = endTime.getHour();
    		pm = (hour / 12 == 1);
    		hour = hour % 12;
    		if (hour == 0) {
    			writer.print("12");
    		} else {
    			writer.print(hour);
    		}
    		writer.print(":");
    		writer.print(endTime.getMinute());
    		if (pm) {
    			writer.print("PM");
    		} else {
    			writer.print("AM");
    		}
    		writer.println();
    	}
    }
}
