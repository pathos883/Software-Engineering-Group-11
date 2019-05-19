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

	public abstract class ConstraintForm : Form {
	  public string name;
    public int type;
        

    /*public ConstraintForm(string name, int type, DateTime begin, DateTime end) {
        this.name = name;
        this.type = type;
        this.begin = begin;
        this.end = end;

        InitializeComponent();
        
        textBoxName.Text = name;
        comboBoxType.SelectedIndex = type;
        dateTimePickerStart.Value = begin;
        dateTimePickerEnd.Value = end;

    }*/
		
		abstract void buttonSave_Click(object sender, EventArgs e);
    protected void label3_Click(object sender, EventArgs e) {}
    protected void textBox3_TextChanged(object sender, EventArgs e) {}
    protected void textBox4_TextChanged(object sender, EventArgs e) {}
    protected void label2_Click(object sender, EventArgs e) {}
	}

  public class IncludeConstraint : ConstraintForm {
	  public DateTime begin, end;
		public IncludeConstraint(string name, DateTime begin, DateTime end) {
      this.name = name;
      this.type = 0;
      this.begin = begin;
      this.end = end;

      InitializeComponent();

      textBoxName.Text = name;
      comboBoxType.SelectedIndex = type;
      dateTimePickerStart.Value = begin;
      dateTimePickerEnd.Value = end;
   }
		
      protected void buttonSave_Click(object sender, EventArgs e) {
        name = textBoxName.Text;
        type = comboBoxType.SelectedIndex;
        begin = dateTimePickerStart.Value;
        end = dateTimePickerEnd.Value;
        this.Close();
      }
	}


	public class ExcludeConstraint : ConstraintForm {
		public DateTime begin, end;
		public ExcludeConstraint(string name, DateTime begin, DateTime end) {
      this.name = name;
      this.type = 1;
      this.begin = begin;
      this.end = end;

      InitializeComponent();

      textBoxName.Text = name;
      comboBoxType.SelectedIndex = type;
      dateTimePickerStart.Value = begin;
      dateTimePickerEnd.Value = end;

      }
		
		protected void buttonSave_Click(object sender, EventArgs e) {
      name = textBoxName.Text;
      type = comboBoxType.SelectedIndex;
      begin = dateTimePickerStart.Value;
      end = dateTimePickerEnd.Value;
      this.Close();
      }
	}


	public class QuotaConstraint : ConstraintForm {
		public int hoursRemaining;
		public ConstraintForm(string name, int hours) {
      this.name = name;
      this.type = 2;
      this.hoursRemaining = hours;

      InitializeComponent();

      textBoxName.Text = name;
      comboBoxType.SelectedIndex = type;
			//should be something set to equal hours here
    }
		
	  protected void buttonSave_Click(object sender, EventArgs e) {
      name = textBoxName.Text;
      type = comboBoxType.SelectedIndex;
      //hoursRemaining = something
      this.Close();
    }
	}

}
