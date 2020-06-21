namespace AverMediaTestApp
{
    partial class DeviceSettingsForm
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
            this.txt_InputVideoInfo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbVideoSource = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnNTSC = new System.Windows.Forms.RadioButton();
            this.rbtnPAL = new System.Windows.Forms.RadioButton();
            this.cmbFrameRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_InputVideoInfo
            // 
            this.txt_InputVideoInfo.Location = new System.Drawing.Point(22, 27);
            this.txt_InputVideoInfo.Name = "txt_InputVideoInfo";
            this.txt_InputVideoInfo.Size = new System.Drawing.Size(318, 130);
            this.txt_InputVideoInfo.TabIndex = 0;
            this.txt_InputVideoInfo.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Video Source";
            // 
            // cmbVideoSource
            // 
            this.cmbVideoSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVideoSource.FormattingEnabled = true;
            this.cmbVideoSource.Location = new System.Drawing.Point(109, 174);
            this.cmbVideoSource.Name = "cmbVideoSource";
            this.cmbVideoSource.Size = new System.Drawing.Size(231, 21);
            this.cmbVideoSource.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Video Format";
            // 
            // rbtnNTSC
            // 
            this.rbtnNTSC.AutoSize = true;
            this.rbtnNTSC.Location = new System.Drawing.Point(109, 210);
            this.rbtnNTSC.Name = "rbtnNTSC";
            this.rbtnNTSC.Size = new System.Drawing.Size(54, 17);
            this.rbtnNTSC.TabIndex = 4;
            this.rbtnNTSC.TabStop = true;
            this.rbtnNTSC.Text = "NTSC";
            this.rbtnNTSC.UseVisualStyleBackColor = true;
            // 
            // rbtnPAL
            // 
            this.rbtnPAL.AutoSize = true;
            this.rbtnPAL.Location = new System.Drawing.Point(195, 210);
            this.rbtnPAL.Name = "rbtnPAL";
            this.rbtnPAL.Size = new System.Drawing.Size(45, 17);
            this.rbtnPAL.TabIndex = 5;
            this.rbtnPAL.TabStop = true;
            this.rbtnPAL.Text = "PAL";
            this.rbtnPAL.UseVisualStyleBackColor = true;
            // 
            // cmbFrameRate
            // 
            this.cmbFrameRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrameRate.FormattingEnabled = true;
            this.cmbFrameRate.Location = new System.Drawing.Point(109, 279);
            this.cmbFrameRate.Name = "cmbFrameRate";
            this.cmbFrameRate.Size = new System.Drawing.Size(231, 21);
            this.cmbFrameRate.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 282);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Frame Rate";
            // 
            // cmbResolution
            // 
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(109, 242);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(231, 21);
            this.cmbResolution.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Resolution";
            // 
            // DeviceSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 329);
            this.Controls.Add(this.cmbResolution);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbFrameRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbtnPAL);
            this.Controls.Add(this.rbtnNTSC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbVideoSource);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_InputVideoInfo);
            this.Name = "DeviceSettingsForm";
            this.Text = "Device Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeviceSettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.DeviceSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txt_InputVideoInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbVideoSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnNTSC;
        private System.Windows.Forms.RadioButton rbtnPAL;
        private System.Windows.Forms.ComboBox cmbFrameRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.Label label4;
    }
}