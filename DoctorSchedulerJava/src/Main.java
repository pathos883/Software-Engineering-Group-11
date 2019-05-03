import org.optaplanner.core.api.solver.Solver;
import org.optaplanner.core.api.solver.SolverFactory;

import java.time.LocalDateTime;

public class Main {

    public static void main(String[] args) {
        if (args.length != 1) {
            System.out.println("Only takes a filename");
            return;
        }
	    //build the solver
        //Within the solverconfig is a UnionMoveSelector, I need help updating it for our code -Tristan
        SolverFactory<DoctorSchedule> solverFactory = SolverFactory.createFromXmlResource("doctorSchedulerSolverConfig.xml");
        Solver<DoctorSchedule> solver = solverFactory.buildSolver();

        //load the problem
        DoctorSchedule unsolvedDoctorSchedule = new DoctorSchedule();
        unsolvedDoctorSchedule.importXML(args[0]);

        //solve the problem
        DoctorSchedule solvedCloudBalance = solver.solve(unsolvedDoctorSchedule);

        //export to csv
        /*function to export to csv*/
        solvedCloudBalance.exportCSV("export" + LocalDateTime.now().toString() + ".csv");
    }
}
