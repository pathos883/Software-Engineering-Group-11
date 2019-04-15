public class Request {
    //the shift in question
    private Shift requestedShift;

    //the priority or how many points to lose if broken
    private int priority;

    //whether the shift is wanted or unwanted
    private boolean wanted;

    public void setRequestedShift(Shift requestedShift) {
        this.requestedShift = requestedShift;
    }

    public Shift getRequestedShift() {
        return requestedShift;
    }

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
}
