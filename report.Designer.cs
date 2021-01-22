namespace BD_test
{
    partial class report
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.textSize = new System.Windows.Forms.ComboBox();
            this.headerSize = new System.Windows.Forms.ComboBox();
            this.exportComboBox = new System.Windows.Forms.ComboBox();
            this.headerText = new System.Windows.Forms.TextBox();
            this.font = new System.Windows.Forms.ComboBox();
            this.borderSize = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(305, 17);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 0;
            this.nameTextBox.Text = "Exam";
            // 
            // textSize
            // 
            this.textSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textSize.FormattingEnabled = true;
            this.textSize.Items.AddRange(new object[] {
            "4",
            "8",
            "12",
            "14"});
            this.textSize.Location = new System.Drawing.Point(90, 71);
            this.textSize.Name = "textSize";
            this.textSize.Size = new System.Drawing.Size(100, 21);
            this.textSize.TabIndex = 1;
            // 
            // headerSize
            // 
            this.headerSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.headerSize.FormattingEnabled = true;
            this.headerSize.Items.AddRange(new object[] {
            "4",
            "8",
            "12",
            "14",
            "20",
            "24"});
            this.headerSize.Location = new System.Drawing.Point(90, 43);
            this.headerSize.Name = "headerSize";
            this.headerSize.Size = new System.Drawing.Size(100, 21);
            this.headerSize.TabIndex = 2;
            // 
            // exportComboBox
            // 
            this.exportComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exportComboBox.FormattingEnabled = true;
            this.exportComboBox.Items.AddRange(new object[] {
            "Microsoft Word",
            "Microsoft Excel"});
            this.exportComboBox.Location = new System.Drawing.Point(90, 12);
            this.exportComboBox.Name = "exportComboBox";
            this.exportComboBox.Size = new System.Drawing.Size(100, 21);
            this.exportComboBox.TabIndex = 3;
            this.exportComboBox.SelectedIndexChanged += new System.EventHandler(this.exportComboBox_SelectedIndexChanged);
            this.exportComboBox.SelectedValueChanged += new System.EventHandler(this.exportComboBox_SelectedValueChanged);
            // 
            // headerText
            // 
            this.headerText.Location = new System.Drawing.Point(305, 43);
            this.headerText.Name = "headerText";
            this.headerText.Size = new System.Drawing.Size(100, 20);
            this.headerText.TabIndex = 4;
            this.headerText.Text = "Отчет";
            // 
            // font
            // 
            this.font.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.font.FormattingEnabled = true;
            this.font.Items.AddRange(new object[] {
            "Times New Roman",
            "Calibri",
            "Tahoma"});
            this.font.Location = new System.Drawing.Point(305, 69);
            this.font.Name = "font";
            this.font.Size = new System.Drawing.Size(100, 21);
            this.font.TabIndex = 5;
            // 
            // borderSize
            // 
            this.borderSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.borderSize.FormattingEnabled = true;
            this.borderSize.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.borderSize.Location = new System.Drawing.Point(316, 132);
            this.borderSize.Name = "borderSize";
            this.borderSize.Size = new System.Drawing.Size(100, 21);
            this.borderSize.TabIndex = 6;
            this.borderSize.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(133, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 24);
            this.button1.TabIndex = 7;
            this.button1.Text = "Сформировать отчет";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Имя файла";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Размер текста";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Размер шапки";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Название отчета";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(258, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Шрифт";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(209, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Толщина границы";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Вид отчета";
            // 
            // report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 136);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.borderSize);
            this.Controls.Add(this.font);
            this.Controls.Add(this.headerText);
            this.Controls.Add(this.exportComboBox);
            this.Controls.Add(this.headerSize);
            this.Controls.Add(this.textSize);
            this.Controls.Add(this.nameTextBox);
            this.Name = "report";
            this.Text = "Формирование отчета";
            this.Load += new System.EventHandler(this.report_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox textSize;
        private System.Windows.Forms.ComboBox headerSize;
        private System.Windows.Forms.ComboBox exportComboBox;
        private System.Windows.Forms.TextBox headerText;
        private System.Windows.Forms.ComboBox font;
        private System.Windows.Forms.ComboBox borderSize;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}