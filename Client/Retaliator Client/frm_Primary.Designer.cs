namespace Retaliator_Client
{
    partial class frm_Primary
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
            this.components = new System.ComponentModel.Container();
            this.lbl_X = new System.Windows.Forms.Label();
            this.lbl_Y = new System.Windows.Forms.Label();
            this.updateTImer = new System.Windows.Forms.Timer(this.components);
            this.ckbx_fire = new System.Windows.Forms.CheckBox();
            this.lbl_Throttle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_X
            // 
            this.lbl_X.AutoSize = true;
            this.lbl_X.Location = new System.Drawing.Point(13, 30);
            this.lbl_X.Name = "lbl_X";
            this.lbl_X.Size = new System.Drawing.Size(26, 13);
            this.lbl_X.TabIndex = 0;
            this.lbl_X.Text = "X: 0";
            // 
            // lbl_Y
            // 
            this.lbl_Y.AutoSize = true;
            this.lbl_Y.Location = new System.Drawing.Point(13, 50);
            this.lbl_Y.Name = "lbl_Y";
            this.lbl_Y.Size = new System.Drawing.Size(26, 13);
            this.lbl_Y.TabIndex = 1;
            this.lbl_Y.Text = "Y: 0";
            // 
            // updateTImer
            // 
            this.updateTImer.Interval = 10;
            this.updateTImer.Tick += new System.EventHandler(this.updateTImer_Tick);
            // 
            // ckbx_fire
            // 
            this.ckbx_fire.AutoSize = true;
            this.ckbx_fire.Enabled = false;
            this.ckbx_fire.Location = new System.Drawing.Point(16, 93);
            this.ckbx_fire.Name = "ckbx_fire";
            this.ckbx_fire.Size = new System.Drawing.Size(50, 17);
            this.ckbx_fire.TabIndex = 2;
            this.ckbx_fire.Text = "FIRE";
            this.ckbx_fire.UseVisualStyleBackColor = true;
            // 
            // lbl_Throttle
            // 
            this.lbl_Throttle.AutoSize = true;
            this.lbl_Throttle.Location = new System.Drawing.Point(13, 70);
            this.lbl_Throttle.Name = "lbl_Throttle";
            this.lbl_Throttle.Size = new System.Drawing.Size(55, 13);
            this.lbl_Throttle.TabIndex = 3;
            this.lbl_Throttle.Text = "Throttle: 0";
            // 
            // frm_Primary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lbl_Throttle);
            this.Controls.Add(this.ckbx_fire);
            this.Controls.Add(this.lbl_Y);
            this.Controls.Add(this.lbl_X);
            this.Name = "frm_Primary";
            this.Text = "Retaliator Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_X;
        private System.Windows.Forms.Label lbl_Y;
        private System.Windows.Forms.Timer updateTImer;
        private System.Windows.Forms.CheckBox ckbx_fire;
        private System.Windows.Forms.Label lbl_Throttle;
    }
}

