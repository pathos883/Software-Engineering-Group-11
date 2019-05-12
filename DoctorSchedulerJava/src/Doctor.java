import java.util.ArrayList;

public class Doctor {
    //unique id of the doctor
    private int ID;

    //name string
    private String name;

    //hours to work
    private double hoursToWork;

    //list of requested days / shifts on or off
    private ArrayList<Request> requests;

    public Doctor(int ID, String name) {
        this.ID = ID;
        this.name = name;
    }

    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getHoursToWork() {
        return hoursToWork;
    }

    public void setHoursToWork(double hoursToWork) {
        this.hoursToWork = hoursToWork;
    }

    public ArrayList<Request> getShiftRequests() {
        return requests;
    }

    public void setShiftRequests(ArrayList<Request> shiftRequests) {
        this.requests = shiftRequests;
    }
}
