namespace BD_test
{
    partial class Menu
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
            this.menuButton1 = new System.Windows.Forms.Button();
            this.menuButton2 = new System.Windows.Forms.Button();
            this.menuButton3 = new System.Windows.Forms.Button();
            this.menuButton4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // menuButton1
            // 
            this.menuButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuButton1.Location = new System.Drawing.Point(179, 131);
            this.menuButton1.Name = "menuButton1";
            this.menuButton1.Size = new System.Drawing.Size(211, 31);
            this.menuButton1.TabIndex = 0;
            this.menuButton1.Text = "Обследования";
            this.menuButton1.UseVisualStyleBackColor = false;
            this.menuButton1.Click += new System.EventHandler(this.menuButton1_Click);
            // 
            // menuButton2
            // 
            this.menuButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuButton2.Location = new System.Drawing.Point(179, 168);
            this.menuButton2.Name = "menuButton2";
            this.menuButton2.Size = new System.Drawing.Size(211, 31);
            this.menuButton2.TabIndex = 1;
            this.menuButton2.Text = "Пациенты";
            this.menuButton2.UseVisualStyleBackColor = false;
            this.menuButton2.Click += new System.EventHandler(this.menuButton2_Click);
            // 
            // menuButton3
            // 
            this.menuButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuButton3.Location = new System.Drawing.Point(179, 205);
            this.menuButton3.Name = "menuButton3";
            this.menuButton3.Size = new System.Drawing.Size(211, 31);
            this.menuButton3.TabIndex = 2;
            this.menuButton3.Text = "Доктора (пользователи)";
            this.menuButton3.UseVisualStyleBackColor = false;
            this.menuButton3.Click += new System.EventHandler(this.menuButton3_Click);
            // 
            // menuButton4
            // 
            this.menuButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuButton4.Location = new System.Drawing.Point(179, 279);
            this.menuButton4.Name = "menuButton4";
            this.menuButton4.Size = new System.Drawing.Size(211, 31);
            this.menuButton4.TabIndex = 3;
            this.menuButton4.Text = "Выход";
            this.menuButton4.UseVisualStyleBackColor = false;
            this.menuButton4.Click += new System.EventHandler(this.menuButton4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(122, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Добро пожаловать в главное меню";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(179, 242);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "Подключение БД";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 442);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuButton4);
            this.Controls.Add(this.menuButton3);
            this.Controls.Add(this.menuButton2);
            this.Controls.Add(this.menuButton1);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button menuButton1;
        private System.Windows.Forms.Button menuButton2;
        private System.Windows.Forms.Button menuButton3;
        private System.Windows.Forms.Button menuButton4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}