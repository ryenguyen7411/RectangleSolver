namespace RectangleSolver
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.cbb_attributes = new System.Windows.Forms.ComboBox();
			this.txt_value = new LollipopTextBox();
			this.lbl_equal = new LollipopLabel();
			this.btn_solve = new LollipopButton();
			this.txt_result = new LollipopTextBox();
			this.txt_info = new LollipopTextBox();
			this.btn_add_attribute = new LollipopButton();
			this.SuspendLayout();
			// 
			// cbb_attributes
			// 
			this.cbb_attributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbb_attributes.FormattingEnabled = true;
			this.cbb_attributes.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "a",
            "b",
            "S",
            "P",
            "p",
            "m",
            "n",
            "r"});
			this.cbb_attributes.Location = new System.Drawing.Point(12, 26);
			this.cbb_attributes.Name = "cbb_attributes";
			this.cbb_attributes.Size = new System.Drawing.Size(94, 21);
			this.cbb_attributes.TabIndex = 0;
			// 
			// txt_value
			// 
			this.txt_value.FocusedColor = "#508ef5";
			this.txt_value.FontColor = "#999999";
			this.txt_value.IsEnabled = true;
			this.txt_value.Location = new System.Drawing.Point(139, 23);
			this.txt_value.MaxLength = 32767;
			this.txt_value.Multiline = false;
			this.txt_value.Name = "txt_value";
			this.txt_value.ReadOnly = false;
			this.txt_value.Size = new System.Drawing.Size(121, 24);
			this.txt_value.TabIndex = 6;
			this.txt_value.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.txt_value.UseSystemPasswordChar = false;
			this.txt_value.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_value_KeyUp);
			// 
			// lbl_equal
			// 
			this.lbl_equal.AutoSize = true;
			this.lbl_equal.BackColor = System.Drawing.Color.Transparent;
			this.lbl_equal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lbl_equal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.lbl_equal.Location = new System.Drawing.Point(112, 28);
			this.lbl_equal.Name = "lbl_equal";
			this.lbl_equal.Size = new System.Drawing.Size(16, 17);
			this.lbl_equal.TabIndex = 5;
			this.lbl_equal.Text = "=";
			// 
			// btn_solve
			// 
			this.btn_solve.BackColor = System.Drawing.Color.Transparent;
			this.btn_solve.BGColor = "#508ef5";
			this.btn_solve.FontColor = "#ffffff";
			this.btn_solve.Location = new System.Drawing.Point(139, 53);
			this.btn_solve.Name = "btn_solve";
			this.btn_solve.Size = new System.Drawing.Size(121, 37);
			this.btn_solve.TabIndex = 4;
			this.btn_solve.Text = "Solve";
			this.btn_solve.Click += new System.EventHandler(this.btn_solve_Click);
			// 
			// txt_result
			// 
			this.txt_result.FocusedColor = "#508ef5";
			this.txt_result.FontColor = "#999999";
			this.txt_result.IsEnabled = true;
			this.txt_result.Location = new System.Drawing.Point(266, 96);
			this.txt_result.MaxLength = 32767;
			this.txt_result.Multiline = true;
			this.txt_result.Name = "txt_result";
			this.txt_result.ReadOnly = false;
			this.txt_result.Size = new System.Drawing.Size(300, 190);
			this.txt_result.TabIndex = 3;
			this.txt_result.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.txt_result.UseSystemPasswordChar = false;
			// 
			// txt_info
			// 
			this.txt_info.FocusedColor = "#508ef5";
			this.txt_info.FontColor = "#999999";
			this.txt_info.IsEnabled = true;
			this.txt_info.Location = new System.Drawing.Point(12, 96);
			this.txt_info.MaxLength = 32767;
			this.txt_info.Multiline = true;
			this.txt_info.Name = "txt_info";
			this.txt_info.ReadOnly = false;
			this.txt_info.Size = new System.Drawing.Size(248, 190);
			this.txt_info.TabIndex = 2;
			this.txt_info.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.txt_info.UseSystemPasswordChar = false;
			// 
			// btn_add_attribute
			// 
			this.btn_add_attribute.BackColor = System.Drawing.Color.Transparent;
			this.btn_add_attribute.BGColor = "#508ef5";
			this.btn_add_attribute.FontColor = "#ffffff";
			this.btn_add_attribute.Location = new System.Drawing.Point(12, 53);
			this.btn_add_attribute.Name = "btn_add_attribute";
			this.btn_add_attribute.Size = new System.Drawing.Size(121, 37);
			this.btn_add_attribute.TabIndex = 1;
			this.btn_add_attribute.Text = "Add";
			this.btn_add_attribute.Click += new System.EventHandler(this.btn_add_attribute_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(577, 299);
			this.Controls.Add(this.txt_value);
			this.Controls.Add(this.lbl_equal);
			this.Controls.Add(this.btn_solve);
			this.Controls.Add(this.txt_result);
			this.Controls.Add(this.txt_info);
			this.Controls.Add(this.btn_add_attribute);
			this.Controls.Add(this.cbb_attributes);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TestForm";
			this.ResumeLayout(false);
			this.PerformLayout();

        }



		#endregion

		private System.Windows.Forms.ComboBox cbb_attributes;
		private LollipopButton btn_add_attribute;
		private LollipopTextBox txt_info;
		private LollipopTextBox txt_result;
		private LollipopButton btn_solve;
		private LollipopLabel lbl_equal;
		private LollipopTextBox txt_value;
	}
}

