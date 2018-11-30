namespace RGSS_Risk2
{
    partial class BattleForm
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
            this.picHeads = new System.Windows.Forms.PictureBox();
            this.picTails = new System.Windows.Forms.PictureBox();
            this.txtWagerAmount = new System.Windows.Forms.TextBox();
            this.lblWagerPrompt = new System.Windows.Forms.Label();
            this.lblPromptChoice = new System.Windows.Forms.Label();
            this.pnlBattleSummary = new System.Windows.Forms.Panel();
            this.btnCloseBattleSummary = new System.Windows.Forms.Button();
            this.lblBattleSummary = new System.Windows.Forms.Label();
            this.lblWinner = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picHeads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTails)).BeginInit();
            this.pnlBattleSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // picHeads
            // 
            this.picHeads.Image = global::RGSS_Risk2.Properties.Resources.heads;
            this.picHeads.Location = new System.Drawing.Point(16, 48);
            this.picHeads.Name = "picHeads";
            this.picHeads.Size = new System.Drawing.Size(150, 150);
            this.picHeads.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeads.TabIndex = 0;
            this.picHeads.TabStop = false;
            this.picHeads.Click += new System.EventHandler(this.picHeads_Click);
            // 
            // picTails
            // 
            this.picTails.Image = global::RGSS_Risk2.Properties.Resources.tails;
            this.picTails.Location = new System.Drawing.Point(172, 48);
            this.picTails.Name = "picTails";
            this.picTails.Size = new System.Drawing.Size(150, 150);
            this.picTails.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTails.TabIndex = 1;
            this.picTails.TabStop = false;
            this.picTails.Click += new System.EventHandler(this.picTails_Click);
            // 
            // txtWagerAmount
            // 
            this.txtWagerAmount.Location = new System.Drawing.Point(172, 6);
            this.txtWagerAmount.Name = "txtWagerAmount";
            this.txtWagerAmount.Size = new System.Drawing.Size(150, 20);
            this.txtWagerAmount.TabIndex = 2;
            // 
            // lblWagerPrompt
            // 
            this.lblWagerPrompt.AutoSize = true;
            this.lblWagerPrompt.Location = new System.Drawing.Point(12, 9);
            this.lblWagerPrompt.Name = "lblWagerPrompt";
            this.lblWagerPrompt.Size = new System.Drawing.Size(154, 13);
            this.lblWagerPrompt.TabIndex = 3;
            this.lblWagerPrompt.Text = "Number of Students to Wager: ";
            // 
            // lblPromptChoice
            // 
            this.lblPromptChoice.AutoSize = true;
            this.lblPromptChoice.Location = new System.Drawing.Point(12, 32);
            this.lblPromptChoice.Name = "lblPromptChoice";
            this.lblPromptChoice.Size = new System.Drawing.Size(120, 13);
            this.lblPromptChoice.TabIndex = 4;
            this.lblPromptChoice.Text = "Choose Heads or Tails: ";
            // 
            // pnlBattleSummary
            // 
            this.pnlBattleSummary.Controls.Add(this.btnCloseBattleSummary);
            this.pnlBattleSummary.Controls.Add(this.lblBattleSummary);
            this.pnlBattleSummary.Controls.Add(this.lblWinner);
            this.pnlBattleSummary.Location = new System.Drawing.Point(12, 9);
            this.pnlBattleSummary.Name = "pnlBattleSummary";
            this.pnlBattleSummary.Size = new System.Drawing.Size(314, 185);
            this.pnlBattleSummary.TabIndex = 5;
            this.pnlBattleSummary.Visible = false;
            // 
            // btnCloseBattleSummary
            // 
            this.btnCloseBattleSummary.Location = new System.Drawing.Point(226, 149);
            this.btnCloseBattleSummary.Name = "btnCloseBattleSummary";
            this.btnCloseBattleSummary.Size = new System.Drawing.Size(75, 23);
            this.btnCloseBattleSummary.TabIndex = 8;
            this.btnCloseBattleSummary.Text = "Close";
            this.btnCloseBattleSummary.UseVisualStyleBackColor = true;
            this.btnCloseBattleSummary.Click += new System.EventHandler(this.btnCloseBattleSummary_Click);
            // 
            // lblBattleSummary
            // 
            this.lblBattleSummary.AutoSize = true;
            this.lblBattleSummary.Location = new System.Drawing.Point(112, 79);
            this.lblBattleSummary.Name = "lblBattleSummary";
            this.lblBattleSummary.Size = new System.Drawing.Size(87, 13);
            this.lblBattleSummary.TabIndex = 7;
            this.lblBattleSummary.Text = "lblBattleSummary";
            this.lblBattleSummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWinner
            // 
            this.lblWinner.AutoSize = true;
            this.lblWinner.Location = new System.Drawing.Point(130, 37);
            this.lblWinner.Name = "lblWinner";
            this.lblWinner.Size = new System.Drawing.Size(51, 13);
            this.lblWinner.TabIndex = 6;
            this.lblWinner.Text = "lblWinner";
            this.lblWinner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 209);
            this.Controls.Add(this.pnlBattleSummary);
            this.Controls.Add(this.lblPromptChoice);
            this.Controls.Add(this.lblWagerPrompt);
            this.Controls.Add(this.txtWagerAmount);
            this.Controls.Add(this.picTails);
            this.Controls.Add(this.picHeads);
            this.Name = "BattleForm";
            this.Text = "BattleForm";
            ((System.ComponentModel.ISupportInitialize)(this.picHeads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTails)).EndInit();
            this.pnlBattleSummary.ResumeLayout(false);
            this.pnlBattleSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picHeads;
        private System.Windows.Forms.PictureBox picTails;
        private System.Windows.Forms.TextBox txtWagerAmount;
        private System.Windows.Forms.Label lblWagerPrompt;
        private System.Windows.Forms.Label lblPromptChoice;
        private System.Windows.Forms.Panel pnlBattleSummary;
        private System.Windows.Forms.Button btnCloseBattleSummary;
        private System.Windows.Forms.Label lblBattleSummary;
        private System.Windows.Forms.Label lblWinner;
    }
}