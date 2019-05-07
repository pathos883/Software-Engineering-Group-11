import java.time.LocalDateTime;

public class Request {
    //the shift in question

    //start time
    private LocalDateTime start;

    //end time
    private LocalDateTime end;

    //the priority or how many points to lose if broken
    private int priority;

    //whether the shift is wanted or unwanted
    private boolean wanted;
    
    public void setPriority(int priority) {
        this.priority = priority;
    }

    public int getPriority() {
        return priority;
    }

    public void setWanted(boolean wanted) {
        this.wanted = wanted;
    }

    public boolean getWanted() {
        return wanted;
    }

    public LocalDateTime getStart() {
        return start;
    }

    public void setStart(LocalDateTime start) {
        this.start = start;
    }

    public LocalDateTime getEnd() {
        return end;
    }

    public void setEnd(LocalDateTime end) {
        this.end = end;
    }
}
