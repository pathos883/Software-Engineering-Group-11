import org.optaplanner.core.api.domain.solution.PlanningEntityCollectionProperty;
import org.optaplanner.core.api.domain.solution.PlanningSolution;

import java.util.ArrayList;

@PlanningSolution
public class DoctorSchedule {
    //list of the doctors
    ArrayList<Doctor> doctors;

    //list of the days of shifts
    @PlanningEntityCollectionProperty
    ArrayList<Shift> shifts;

    //score of the shift
}
