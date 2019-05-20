package Schedule;
import java.time.LocalDateTime;

public class ShiftType {
    //start time
    private LocalDateTime startTime;

    //end time
    private LocalDateTime endTime;

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

    public ShiftType(LocalDateTime startTime, LocalDateTime endTime) {
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public static boolean CrossesDays(ShiftType shift) {
        return shift.startTime.compareTo(shift.endTime) <= 0;
    }
}
