namespace DoctorScheduling {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listBoxDoctors = new System.Windows.Forms.ListBox();
            this.listBoxConstraints = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonNewDoctor = new System.Windows.Forms.Button();
            this.buttonRemoveDoctor = new System.Windows.Forms.Button();
            this.buttonEditDoctor = new System.Windows.Forms.Button();
            this.buttonNewConstraint = new System.Windows.Forms.Button();
            this.buttonEditConstraint = new System.Windows.Forms.Button();
            this.buttonRemoveConstraint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxDoctors
            // 
            this.listBoxDoctors.FormattingEnabled = true;
            this.listBoxDoctors.ItemHeight = 16;
            this.listBoxDoctors.Location = new System.Drawing.Point(12, 40);
            this.listBoxDoctors.Name = "listBoxDoctors";
            this.listBoxDoctors.Size = new System.Drawing.Size(256, 404);
            this.listBoxDoctors.TabIndex = 0;
            this.listBoxDoctors.SelectedIndexChanged += new System.EventHandler(this.listBoxDoctors_SelectedIndexChanged);
            // 
            // listBoxConstraints
            // 
            this.listBoxConstraints.FormattingEnabled = true;
            this.listBoxConstraints.ItemHeight = 16;
            this.listBoxConstraints.Location = new System.Drawing.Point(274, 40);
            this.listBoxConstraints.Name = "listBoxConstraints";
            this.listBoxConstraints.Size = new System.Drawing.Size(256, 404);
            this.listBoxConstraints.TabIndex = 2;
            this.listBoxConstraints.SelectedIndexChanged += new System.EventHandler(this.listBoxConstraints_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Doctors";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Constraints";
            // 
            // buttonNewDoctor
            // 
            this.buttonNewDoctor.Location = new System.Drawing.Point(12, 450);
            this.buttonNewDoctor.Name = "buttonNewDoctor";
            this.buttonNewDoctor.Size = new System.Drawing.Size(256, 23);
            this.buttonNewDoctor.TabIndex = 7;
            this.buttonNewDoctor.Text = "New Doctor...";
            this.buttonNewDoctor.UseVisualStyleBackColor = true;
            this.buttonNewDoctor.Click += new System.EventHandler(this.buttonNewDoctor_Click);
            // 
            // buttonRemoveDoctor
            // 
            this.buttonRemoveDoctor.Location = new System.Drawing.Point(12, 508);
            this.buttonRemoveDoctor.Name = "buttonRemoveDoctor";
            this.buttonRemoveDoctor.Size = new System.Drawing.Size(256, 23);
            this.buttonRemoveDoctor.TabIndex = 8;
            this.buttonRemoveDoctor.Text = "Remove Doctor";
            this.buttonRemoveDoctor.UseVisualStyleBackColor = true;
            this.buttonRemoveDoctor.Click += new System.EventHandler(this.buttonRemoveDoctor_Click);
            // 
            // buttonEditDoctor
            // 
            this.buttonEditDoctor.Location = new System.Drawing.Point(12, 479);
            this.buttonEditDoctor.Name = "buttonEditDoctor";
            this.buttonEditDoctor.Size = new System.Drawing.Size(256, 23);
            this.buttonEditDoctor.TabIndex = 9;
            this.buttonEditDoctor.Text = "Edit Doctor...";
            this.buttonEditDoctor.UseVisualStyleBackColor = true;
            this.buttonEditDoctor.Click += new System.EventHandler(this.buttonEditDoctor_Click);
            // 
            // buttonNewConstraint
            // 
            this.buttonNewConstraint.Location = new System.Drawing.Point(274, 450);
            this.buttonNewConstraint.Name = "buttonNewConstraint";
            this.buttonNewConstraint.Size = new System.Drawing.Size(256, 23);
            this.buttonNewConstraint.TabIndex = 10;
            this.buttonNewConstraint.Text = "New Constraint...";
            this.buttonNewConstraint.UseVisualStyleBackColor = true;
            this.buttonNewConstraint.Click += new System.EventHandler(this.buttonNewConstraint_Click);
            // 
            // buttonEditConstraint
            // 
            this.buttonEditConstraint.Location = new System.Drawing.Point(274, 479);
            this.buttonEditConstraint.Name = "buttonEditConstraint";
            this.buttonEditConstraint.Size = new System.Drawing.Size(256, 23);
            this.buttonEditConstraint.TabIndex = 11;
            this.buttonEditConstraint.Text = "Edit Constraint...";
            this.buttonEditConstraint.UseVisualStyleBackColor = true;
            this.buttonEditConstraint.Click += new System.EventHandler(this.buttonEditConstraint_Click);
            // 
            // buttonRemoveConstraint
            // 
            this.buttonRemoveConstraint.Location = new System.Drawing.Point(274, 508);
            this.buttonRemoveConstraint.Name = "buttonRemoveConstraint";
            this.buttonRemoveConstraint.Size = new System.Drawing.Size(256, 23);
            this.buttonRemoveConstraint.TabIndex = 12;
            this.buttonRemoveConstraint.Text = "Remove Constraint";
            this.buttonRemoveConstraint.UseVisualStyleBackColor = true;
            this.buttonRemoveConstraint.Click += new System.EventHandler(this.buttonRemoveConstraint_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 539);
            this.Controls.Add(this.buttonRemoveConstraint);
            this.Controls.Add(this.buttonEditConstraint);
            this.Controls.Add(this.buttonNewConstraint);
            this.Controls.Add(this.buttonEditDoctor);
            this.Controls.Add(this.buttonRemoveDoctor);
            this.Controls.Add(this.buttonNewDoctor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxConstraints);
            this.Controls.Add(this.listBoxDoctors);
            this.Name = "MainForm";
            this.Text = "Schedule Planner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxDoctors;
        private System.Windows.Forms.ListBox listBoxConstraints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonNewDoctor;
        private System.Windows.Forms.Button buttonRemoveDoctor;
        private System.Windows.Forms.Button buttonEditDoctor;
        private System.Windows.Forms.Button buttonNewConstraint;
        private System.Windows.Forms.Button buttonEditConstraint;
        private System.Windows.Forms.Button buttonRemoveConstraint;
    }
}

