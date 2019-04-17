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

    public partial class ConstraintForm : Form {

        public string name;
        public int type;
        public DateTime begin, end;

        public ConstraintForm(string name, int type, DateTime begin, DateTime end) {
            this.name = name;
            this.type = type;
            this.begin = begin;
            this.end = end;

            InitializeComponent();

            textBoxName.Text = name;
            comboBoxType.SelectedIndex = type;
            dateTimePickerStart.Value = begin;
            dateTimePickerEnd.Value = end;

        }

        private void label3_Click(object sender, EventArgs e) {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) {

        }

        private void textBox4_TextChanged(object sender, EventArgs e) {

        }

        private void buttonSave_Click(object sender, EventArgs e) {
            name = textBoxName.Text;
            type = comboBoxType.SelectedIndex;
            begin = dateTimePickerStart.Value;
            end = dateTimePickerEnd.Value;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e) {

        }
    }
}
