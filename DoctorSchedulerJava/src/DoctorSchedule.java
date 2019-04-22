import org.optaplanner.core.api.domain.solution.PlanningEntityCollectionProperty;
import org.optaplanner.core.api.domain.solution.PlanningScore;
import org.optaplanner.core.api.domain.solution.PlanningSolution;
import org.optaplanner.core.api.score.buildin.hardsoft.HardSoftScore;

import java.util.ArrayList;

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

    }
}
