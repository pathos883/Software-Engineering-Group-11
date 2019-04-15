import org.optaplanner.core.api.domain.entity.PlanningEntity;
import org.optaplanner.core.api.domain.variable.PlanningVariable;

@PlanningEntity
public class Shift {
    //holiday status

    //day of the week
    private String dayOfWeek;

    //start time
    private String startTime;

    //end time
    private String endTime;

    //doctor to work that shift
    @PlanningVariable
    private Doctor shiftDoctor;

    public void setDayOfWeek(String dayOfWeek) {
        this.dayOfWeek = dayOfWeek;
    }

    public String getDayOfWeek() {
        return dayOfWeek;
    }

    public String getStartTime() {
        return startTime;
    }

    public void setStartTime(String startTime) {
        this.startTime = startTime;
    }

    public String getEndTime() {
        return endTime;
    }

    public void setEndTime(String endTime) {
        this.endTime = endTime;
    }

    public Doctor getShiftDoctor() {
        return shiftDoctor;
    }

    public void setShiftDoctor(Doctor shiftDoctor) {
        this.shiftDoctor = shiftDoctor;
    }
}
