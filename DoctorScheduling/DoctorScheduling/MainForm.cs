using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorScheduling {
    public partial class MainForm : Form {

        List<Schedule> schedules;

        public class Constraint {

            public string name;
            public int type; //0 = include range, 1 = exclude range
            public DateTime start, end;

            public Constraint(string name, int type, DateTime start, DateTime end) {

                this.name = name;
                this.type = type;
                this.start = start;
                this.end = end;

            }

            public override string ToString() {
                return name;
            }

        }

        public class Schedule {

            public string name;
            public List<Constraint> constraints;

            public Schedule (string name) {

                this.name = name;
                constraints = new List<Constraint>();

            }

            public void AddConstraint(Constraint c) {

                constraints.Add(c);

            }

            public void AddConstraint(string name, int type, DateTime start, DateTime end) {

                constraints.Add(new Constraint(name, type, start, end));

            }

            public override string ToString() {
                return name;
            }

        }

        public MainForm() {

            InitializeComponent();
            schedules = new List<Schedule>();

            Schedule s1 = new Schedule("(Global)");
            //s1.AddConstraint("test1", 0, DateTime.Now, DateTime.Now);
            //s1.AddConstraint("test2", 0, DateTime.Now, DateTime.Now);
            schedules.Add(s1);

            updateSchedules();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {

            listBoxDoctors.Items.Clear();
            foreach (Schedule s in schedules) {
                listBoxDoctors.Items.Add(s);
            }

        }

        private void listBoxDoctors_SelectedIndexChanged(object sender, EventArgs e) {
            
            listBoxConstraints.Items.Clear();
            Schedule s = (Schedule)listBoxDoctors.SelectedItem;
            if (s != null)
                foreach (Constraint c in s.constraints) {
                    listBoxConstraints.Items.Add(c);
                }

        }

        private void listBoxConstraints_SelectedIndexChanged(object sender, EventArgs e) {



        }

        private void buttonRemoveSchedule_Click(object sender, EventArgs e) {

        }

        private void buttonNewDoctor_Click(object sender, EventArgs e) {
            DoctorForm df = new DoctorForm("");
            df.ShowDialog();
            schedules.Add(new Schedule(df.name));
            updateSchedules();
        }

        private void updateSchedules() {

            listBoxDoctors.Items.Clear();
            foreach (Schedule s in schedules) {
                listBoxDoctors.Items.Add(s);
            }

        }

        private void updateConstraints() {

            listBoxConstraints.Items.Clear();
            Schedule s = (Schedule)listBoxDoctors.SelectedItem;
            if (s != null)
                foreach (Constraint c in s.constraints) {
                    listBoxConstraints.Items.Add(c);
                }
            listBoxConstraints.Refresh();

        }

        private void buttonEditDoctor_Click(object sender, EventArgs e) {

            Schedule s = (Schedule)listBoxDoctors.SelectedItem;
            DoctorForm df = new DoctorForm(s.name);
            df.ShowDialog();
            s.name = df.name;
            updateSchedules();

        }

        private void buttonNewConstraint_Click(object sender, EventArgs e) {

            ConstraintForm cf = new ConstraintForm("", 0, DateTime.Now, DateTime.Now);
            cf.ShowDialog();
            Schedule s = (Schedule)listBoxDoctors.SelectedItem;
            s.AddConstraint(cf.name, cf.type, cf.begin, cf.end);
            updateConstraints();

        }

        private void buttonEditConstraint_Click(object sender, EventArgs e) {

            Constraint c = (Constraint)listBoxConstraints.SelectedItem;
            ConstraintForm cf = new ConstraintForm(c.name, c.type, c.start, c.end);
            //Schedule s = (Schedule)listBoxDoctors.SelectedItem;
            cf.ShowDialog();
            c.name = cf.name;
            c.type = cf.type;
            c.start = cf.begin;
            c.end = cf.end;
            updateConstraints();

        }

        private void buttonRemoveDoctor_Click(object sender, EventArgs e) {
            Schedule s = (Schedule)listBoxDoctors.SelectedItem;
            schedules.Remove(s);
            updateSchedules();
            listBoxConstraints.Items.Clear();
        }

        private void buttonRemoveConstraint_Click(object sender, EventArgs e) {
            Schedule s = (Schedule)listBoxDoctors.SelectedItem;
            Constraint c = (Constraint)listBoxConstraints.SelectedItem;
            s.constraints.Remove(c);
            updateConstraints();
        }
    }
}
