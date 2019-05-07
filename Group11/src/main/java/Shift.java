import org.optaplanner.core.api.domain.entity.PlanningEntity;
import org.optaplanner.core.api.domain.variable.PlanningVariable;

import java.time.LocalDateTime;

@PlanningEntity
public class Shift {
    //day of the week
    private String dayOfWeek;

    //start time
    private LocalDateTime startTime;

    //end time
    private LocalDateTime endTime;

    //doctor to work that shift
    @PlanningVariable(valueRangeProviderRefs = {"employeeRange"})
    private Doctor shiftDoctor;

    public void setDayOfWeek(String dayOfWeek) {
        this.dayOfWeek = dayOfWeek;
    }

    public String getDayOfWeek() {
        return dayOfWeek;
    }

    public LocalDateTime getStartTime() {
        return startTime;
    }

    public void setStartTime(LocalDateTime startTime) {
        this.startTime = startTime;
    }

    public LocalDateTime getEndTime() {
        return endTime;
    }

    public void setEndTime(LocalDateTime endTime) {
        this.endTime = endTime;
    }

    public Doctor getShiftDoctor() {
        return shiftDoctor;
    }

    public void setShiftDoctor(Doctor shiftDoctor) {
        this.shiftDoctor = shiftDoctor;
    }
}
