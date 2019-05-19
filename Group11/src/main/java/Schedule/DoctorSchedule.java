package Schedule;
import org.optaplanner.core.api.domain.solution.PlanningEntityCollectionProperty;
import org.optaplanner.core.api.domain.solution.PlanningScore;
import org.optaplanner.core.api.domain.solution.PlanningSolution;
import org.optaplanner.core.api.domain.solution.drools.ProblemFactCollectionProperty;
import org.optaplanner.core.api.domain.valuerange.ValueRangeProvider;
import org.optaplanner.core.api.score.buildin.hardsoft.HardSoftScore;
import java.io.File;
import java.io.IOException;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.time.LocalDateTime;
import java.util.ArrayList;

@PlanningSolution
public class DoctorSchedule {
    private LocalDateTime startDate;
    private LocalDateTime endDate;

    //list of the doctors
    @ValueRangeProvider(id = "employeeRange")
    @ProblemFactCollectionProperty
    private ArrayList<Doctor> doctors;

    //list of the days of shifts
    @PlanningEntityCollectionProperty
    private ArrayList<Shift> shifts;

    //score of the shift
    @PlanningScore
    private HardSoftScore score;

    public DoctorSchedule() {
        doctors = new ArrayList<Doctor>();
        shifts = new ArrayList<Shift>();
    }

    public LocalDateTime getStartDate() {
        return startDate;
    }

    //setter for the startdate, used by import
    void setStartDate(LocalDateTime startDate) {
        this.startDate = startDate;
    }

    public LocalDateTime getEndDate() {
        return endDate;
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
        File file = new File(fileName);
        try {
            file.mkdirs();
            file.createNewFile();
        } catch (IOException e1) {
            e1.printStackTrace();
        }
        try {
            PrintWriter writer = new PrintWriter(file);

            writer.println("Subject, Start Date, Start Time, End Date, End Time");

            for (Shift s : shifts) {
                LocalDateTime startTime = s.getStartTime();
                LocalDateTime endTime = s.getEndTime();

                // write name of doctor
                writer.print(s.getShiftDoctor());
                writer.print(",");

                // write start date
                writer.print(startTime.getMonthValue() + "/" + startTime.getDayOfMonth() + "/" + startTime.getYear());
                writer.print(",");

                // write start tiem
                int hour = startTime.getHour();
                boolean pm = (hour / 12 == 1);
                hour = hour % 12;
                if (hour == 0) {
                    writer.print("12");
                } else {
                    writer.print(hour);
                }
                writer.print(":");
                writer.print(startTime.getMinute());
                if (pm) {
                    writer.print("PM");
                } else {
                    writer.print("AM");
                }
                writer.print(",");

                // write end date
                writer.print(endTime.getMonthValue() + "/" + endTime.getDayOfMonth() + "/" + endTime.getYear());
                writer.print(",");

                // write end tiem
                hour = endTime.getHour();
                pm = (hour / 12 == 1);
                hour = hour % 12;
                if (hour == 0) {
                    writer.print("12");
                } else {
                    writer.print(hour);
                }
                writer.print(":");
                writer.print(endTime.getMinute());
                if (pm) {
                    writer.print("PM");
                } else {
                    writer.print("AM");
                }
                writer.println();
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }
}