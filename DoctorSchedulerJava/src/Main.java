import org.optaplanner.core.api.solver.Solver;
import org.optaplanner.core.api.solver.SolverFactory;

public class Main {

    public static void main(String[] args) {
	    //build the solver
        SolverFactory<DoctorSchedule> solverFactory = SolverFactory.createFromXmlResource(/*the configuration xml file from the ui program*/);
        Solver<DoctorSchedule> solver = solverFactory.buildSolver();

        //load the problem
        DoctorSchedule unsolvedDoctorSchedule = /*create doctor schedule from xml file*/;

        //solve the problem
        DoctorSchedule solvedCloudBalance = solver.solve(unsolvedDoctorSchedule);

        //export to csv
        /*function to export to csv*/
    }
}
