import org.optaplanner.core.api.domain.solution.PlanningEntityCollectionProperty;
import org.optaplanner.core.api.domain.solution.PlanningScore;
import org.optaplanner.core.api.domain.solution.PlanningSolution;
import org.optaplanner.core.api.score.buildin.hardsoft.HardSoftScore;

import java.time.LocalDateTime;
import java.util.ArrayList;

@PlanningSolution
public class DoctorSchedule {
    LocalDateTime startDate;
    LocalDateTime endDate;

    //list of the doctors
    ArrayList<Doctor> doctors;

    //list of the days of shifts
    @PlanningEntityCollectionProperty
    ArrayList<Shift> shifts;

    //score of the shift
    @PlanningScore
    private HardSoftScore score;

    public DoctorSchedule() {
        doctors = new ArrayList<Doctor>();
        shifts = new ArrayList<Shift>();
    }

    //setter for the startdate, used by import
    void setStartDate(LocalDateTime startDate) {
        this.startDate = startDate;
    }

    //setter for the enddate, used by import
    void setEndDate(LocalDateTime endDate) {
        this.endDate = endDate;
    }

    public ArrayList<Doctor> getDoctors() {
        return doctors;
    }

    public void setDoctors(ArrayList<Doctor> doctors) {
        this.doctors = doctors;
    }

    public void addDoctor(Doctor doctor) {
        doctors.add(doctor);
    }

    public ArrayList<Shift> getShifts() {
        return shifts;
    }

    public void setShifts(ArrayList<Shift> shifts) {
        this.shifts = shifts;
    }

    public void addShift(Shift shift) {
        shifts.add(shift);
    }

    public HardSoftScore getScore() {
        return score;
    }

    public void setScore(HardSoftScore score) {
        this.score = score;
    }

    //function to create from xml
    void importXML(String fileName){
        XMLImporter importer = new XMLImporter();
        importer.readSolution(fileName, this);
    }

    //function to export a csv
    void exportCSV(String fileName){

    }
}
