import java.util.ArrayList;

public class Doctor {
    //name string
    private String name;

    //hours worked so far
    private double hoursWorked;

    //hours to work
    private double hoursToWork;

    //list of requested days / shifts on or off
    private ArrayList<Shift> shiftRequests;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getHoursWorked() {
        return hoursWorked;
    }

    public void setHoursWorked(double hoursWorked) {
        this.hoursWorked = hoursWorked;
    }

    public double getHoursToWork() {
        return hoursToWork;
    }

    public void setHoursToWork(double hoursToWork) {
        this.hoursToWork = hoursToWork;
    }

    public ArrayList<Shift> getShiftRequests() {
        return shiftRequests;
    }

    public void setShiftRequests(ArrayList<Shift> shiftRequests) {
        this.shiftRequests = shiftRequests;
    }
}
