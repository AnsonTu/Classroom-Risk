namespace RGSS_Risk2
{
    partial class PlayerCreationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerCreationForm));
            this.lblGameName = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.chklstDepartmentChoices = new System.Windows.Forms.CheckedListBox();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnQuitGame = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGameName
            // 
            this.lblGameName.AutoSize = true;
            this.lblGameName.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameName.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblGameName.Location = new System.Drawing.Point(204, 56);
            this.lblGameName.Name = "lblGameName";
            this.lblGameName.Size = new System.Drawing.Size(354, 73);
            this.lblGameName.TabIndex = 5;
            this.lblGameName.Text = "RGSS RISK";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.ForeColor = System.Drawing.Color.Black;
            this.lblDepartment.Location = new System.Drawing.Point(126, 164);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(206, 40);
            this.lblDepartment.TabIndex = 9;
            this.lblDepartment.Text = "Departments:";
            // 
            // chklstDepartmentChoices
            // 
            this.chklstDepartmentChoices.BackColor = System.Drawing.Color.Lime;
            this.chklstDepartmentChoices.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklstDepartmentChoices.FormattingEnabled = true;
            this.chklstDepartmentChoices.Items.AddRange(new object[] {
            "Computer Science",
            "English",
            "Science",
            "Mathematics",
            "Geography"});
            this.chklstDepartmentChoices.Location = new System.Drawing.Point(63, 217);
            this.chklstDepartmentChoices.Name = "chklstDepartmentChoices";
            this.chklstDepartmentChoices.Size = new System.Drawing.Size(329, 229);
            this.chklstDepartmentChoices.TabIndex = 10;
            // 
            // btnStartGame
            // 
            this.btnStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartGame.Location = new System.Drawing.Point(424, 217);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(230, 102);
            this.btnStartGame.TabIndex = 11;
            this.btnStartGame.Text = "Start Game";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnQuitGame
            // 
            this.btnQuitGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitGame.Location = new System.Drawing.Point(424, 344);
            this.btnQuitGame.Name = "btnQuitGame";
            this.btnQuitGame.Size = new System.Drawing.Size(230, 102);
            this.btnQuitGame.TabIndex = 13;
            this.btnQuitGame.Text = "Quit Game";
            this.btnQuitGame.UseVisualStyleBackColor = true;
            this.btnQuitGame.Click += new System.EventHandler(this.btnQuitGame_Click);
            // 
            // lblInstructions
            // 
            this.lblInstructions.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(675, 9);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(497, 604);
            this.lblInstructions.TabIndex = 15;
            this.lblInstructions.Text = resources.GetString("lblInstructions.Text");
            // 
            // PlayerCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 662);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnQuitGame);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.chklstDepartmentChoices);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.lblGameName);
            this.Name = "PlayerCreationForm";
            this.Text = "PlayerCreationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGameName;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.CheckedListBox chklstDepartmentChoices;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnQuitGame;
        private System.Windows.Forms.Label lblInstructions;
    }
}

