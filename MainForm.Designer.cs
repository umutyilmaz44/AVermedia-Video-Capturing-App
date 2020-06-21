namespace AverMediaTestApp
{
    partial class MainForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnDeviceSettings = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoxDeviceList = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControlFilter = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblHwSaturation = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbarHwSaturation = new System.Windows.Forms.TrackBar();
            this.lblHwHue = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbarHwHue = new System.Windows.Forms.TrackBar();
            this.lblHwConstrast = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbarHwConstrast = new System.Windows.Forms.TrackBar();
            this.lblHwBrigthness = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbarHwBrigthness = new System.Windows.Forms.TrackBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblSwSaturation = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbarSwSaturation = new System.Windows.Forms.TrackBar();
            this.lblSwHue = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbarSwHue = new System.Windows.Forms.TrackBar();
            this.lblSwConstrast = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbarSwConstrast = new System.Windows.Forms.TrackBar();
            this.lblSwBrigthness = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tbarSwBrigthness = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnResetRotate = new System.Windows.Forms.Button();
            this.btnRotateCounterclockwise = new System.Windows.Forms.Button();
            this.btnRotateClockwise = new System.Windows.Forms.Button();
            this.cmbVideoMirror = new System.Windows.Forms.ComboBox();
            this.btnResetColorAdjustments = new System.Windows.Forms.Button();
            this.cmbVideoEnhancement = new System.Windows.Forms.ComboBox();
            this.cmbDeInterlance = new System.Windows.Forms.ComboBox();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pboxImage = new System.Windows.Forms.PictureBox();
            this.btnCrosshairKeyTrackingLock = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControlFilter.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHwSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHwHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHwConstrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHwBrigthness)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSwSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSwHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSwConstrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSwBrigthness)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSaveSettings);
            this.panel1.Controls.Add(this.btnDeviceSettings);
            this.panel1.Controls.Add(this.btnCapture);
            this.panel1.Controls.Add(this.btnStartStop);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbBoxDeviceList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 55);
            this.panel1.TabIndex = 1;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackgroundImage = global::AverMediaTestApp.Properties.Resources.save_settings;
            this.btnSaveSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveSettings.Location = new System.Drawing.Point(444, 3);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(52, 46);
            this.btnSaveSettings.TabIndex = 6;
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // btnDeviceSettings
            // 
            this.btnDeviceSettings.BackgroundImage = global::AverMediaTestApp.Properties.Resources.device_settings;
            this.btnDeviceSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeviceSettings.Location = new System.Drawing.Point(386, 3);
            this.btnDeviceSettings.Name = "btnDeviceSettings";
            this.btnDeviceSettings.Size = new System.Drawing.Size(52, 46);
            this.btnDeviceSettings.TabIndex = 5;
            this.btnDeviceSettings.UseVisualStyleBackColor = true;
            this.btnDeviceSettings.Click += new System.EventHandler(this.btnDeviceSettings_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapture.BackgroundImage = global::AverMediaTestApp.Properties.Resources.capture_image;
            this.btnCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCapture.Location = new System.Drawing.Point(918, 3);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(52, 46);
            this.btnCapture.TabIndex = 4;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStop.BackgroundImage = global::AverMediaTestApp.Properties.Resources.start_video;
            this.btnStartStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStartStop.Location = new System.Drawing.Point(860, 3);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(52, 46);
            this.btnStartStop.TabIndex = 3;
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Capture Card";
            // 
            // cmbBoxDeviceList
            // 
            this.cmbBoxDeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxDeviceList.FormattingEnabled = true;
            this.cmbBoxDeviceList.Location = new System.Drawing.Point(11, 26);
            this.cmbBoxDeviceList.Name = "cmbBoxDeviceList";
            this.cmbBoxDeviceList.Size = new System.Drawing.Size(359, 21);
            this.cmbBoxDeviceList.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tabControlFilter);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(784, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 627);
            this.panel2.TabIndex = 3;
            // 
            // tabControlFilter
            // 
            this.tabControlFilter.Controls.Add(this.tabPage1);
            this.tabControlFilter.Controls.Add(this.tabPage2);
            this.tabControlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFilter.Location = new System.Drawing.Point(0, 282);
            this.tabControlFilter.Name = "tabControlFilter";
            this.tabControlFilter.SelectedIndex = 0;
            this.tabControlFilter.Size = new System.Drawing.Size(198, 343);
            this.tabControlFilter.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblHwSaturation);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.tbarHwSaturation);
            this.tabPage1.Controls.Add(this.lblHwHue);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tbarHwHue);
            this.tabPage1.Controls.Add(this.lblHwConstrast);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tbarHwConstrast);
            this.tabPage1.Controls.Add(this.lblHwBrigthness);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tbarHwBrigthness);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(190, 317);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HW";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblHwSaturation
            // 
            this.lblHwSaturation.Location = new System.Drawing.Point(159, 245);
            this.lblHwSaturation.Name = "lblHwSaturation";
            this.lblHwSaturation.Size = new System.Drawing.Size(27, 13);
            this.lblHwSaturation.TabIndex = 24;
            this.lblHwSaturation.Text = "128";
            this.lblHwSaturation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 245);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Saturation";
            // 
            // tbarHwSaturation
            // 
            this.tbarHwSaturation.Location = new System.Drawing.Point(5, 261);
            this.tbarHwSaturation.Maximum = 255;
            this.tbarHwSaturation.Name = "tbarHwSaturation";
            this.tbarHwSaturation.Size = new System.Drawing.Size(183, 45);
            this.tbarHwSaturation.TabIndex = 22;
            this.tbarHwSaturation.Value = 128;
            this.tbarHwSaturation.Scroll += new System.EventHandler(this.tbarHwSaturation_Scroll);
            // 
            // lblHwHue
            // 
            this.lblHwHue.Location = new System.Drawing.Point(159, 165);
            this.lblHwHue.Name = "lblHwHue";
            this.lblHwHue.Size = new System.Drawing.Size(27, 13);
            this.lblHwHue.TabIndex = 21;
            this.lblHwHue.Text = "128";
            this.lblHwHue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Hue";
            // 
            // tbarHwHue
            // 
            this.tbarHwHue.Location = new System.Drawing.Point(5, 181);
            this.tbarHwHue.Maximum = 255;
            this.tbarHwHue.Name = "tbarHwHue";
            this.tbarHwHue.Size = new System.Drawing.Size(183, 45);
            this.tbarHwHue.TabIndex = 19;
            this.tbarHwHue.Value = 128;
            this.tbarHwHue.Scroll += new System.EventHandler(this.tbarHwHue_Scroll);
            // 
            // lblHwConstrast
            // 
            this.lblHwConstrast.Location = new System.Drawing.Point(159, 85);
            this.lblHwConstrast.Name = "lblHwConstrast";
            this.lblHwConstrast.Size = new System.Drawing.Size(27, 13);
            this.lblHwConstrast.TabIndex = 18;
            this.lblHwConstrast.Text = "128";
            this.lblHwConstrast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Constrast";
            // 
            // tbarHwConstrast
            // 
            this.tbarHwConstrast.Location = new System.Drawing.Point(5, 101);
            this.tbarHwConstrast.Maximum = 255;
            this.tbarHwConstrast.Name = "tbarHwConstrast";
            this.tbarHwConstrast.Size = new System.Drawing.Size(183, 45);
            this.tbarHwConstrast.TabIndex = 16;
            this.tbarHwConstrast.Value = 128;
            this.tbarHwConstrast.Scroll += new System.EventHandler(this.tbarHwConstrast_Scroll);
            // 
            // lblHwBrigthness
            // 
            this.lblHwBrigthness.Location = new System.Drawing.Point(157, 11);
            this.lblHwBrigthness.Name = "lblHwBrigthness";
            this.lblHwBrigthness.Size = new System.Drawing.Size(27, 13);
            this.lblHwBrigthness.TabIndex = 15;
            this.lblHwBrigthness.Text = "128";
            this.lblHwBrigthness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Brigthness";
            // 
            // tbarHwBrigthness
            // 
            this.tbarHwBrigthness.Location = new System.Drawing.Point(3, 27);
            this.tbarHwBrigthness.Maximum = 255;
            this.tbarHwBrigthness.Name = "tbarHwBrigthness";
            this.tbarHwBrigthness.Size = new System.Drawing.Size(183, 45);
            this.tbarHwBrigthness.TabIndex = 13;
            this.tbarHwBrigthness.Value = 128;
            this.tbarHwBrigthness.Scroll += new System.EventHandler(this.tbarHwBrigthness_Scroll);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblSwSaturation);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.tbarSwSaturation);
            this.tabPage2.Controls.Add(this.lblSwHue);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.tbarSwHue);
            this.tabPage2.Controls.Add(this.lblSwConstrast);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.tbarSwConstrast);
            this.tabPage2.Controls.Add(this.lblSwBrigthness);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.tbarSwBrigthness);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(190, 317);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SW";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblSwSaturation
            // 
            this.lblSwSaturation.Location = new System.Drawing.Point(159, 245);
            this.lblSwSaturation.Name = "lblSwSaturation";
            this.lblSwSaturation.Size = new System.Drawing.Size(27, 13);
            this.lblSwSaturation.TabIndex = 36;
            this.lblSwSaturation.Text = "128";
            this.lblSwSaturation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 245);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "Saturation";
            // 
            // tbarSwSaturation
            // 
            this.tbarSwSaturation.Location = new System.Drawing.Point(5, 261);
            this.tbarSwSaturation.Maximum = 255;
            this.tbarSwSaturation.Name = "tbarSwSaturation";
            this.tbarSwSaturation.Size = new System.Drawing.Size(183, 45);
            this.tbarSwSaturation.TabIndex = 34;
            this.tbarSwSaturation.Value = 128;
            this.tbarSwSaturation.Scroll += new System.EventHandler(this.tbarSwSaturation_Scroll);
            // 
            // lblSwHue
            // 
            this.lblSwHue.Location = new System.Drawing.Point(159, 165);
            this.lblSwHue.Name = "lblSwHue";
            this.lblSwHue.Size = new System.Drawing.Size(27, 13);
            this.lblSwHue.TabIndex = 33;
            this.lblSwHue.Text = "128";
            this.lblSwHue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 167);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Hue";
            // 
            // tbarSwHue
            // 
            this.tbarSwHue.Location = new System.Drawing.Point(5, 181);
            this.tbarSwHue.Maximum = 255;
            this.tbarSwHue.Name = "tbarSwHue";
            this.tbarSwHue.Size = new System.Drawing.Size(183, 45);
            this.tbarSwHue.TabIndex = 31;
            this.tbarSwHue.Value = 128;
            this.tbarSwHue.Scroll += new System.EventHandler(this.tbarSwHue_Scroll);
            // 
            // lblSwConstrast
            // 
            this.lblSwConstrast.Location = new System.Drawing.Point(159, 85);
            this.lblSwConstrast.Name = "lblSwConstrast";
            this.lblSwConstrast.Size = new System.Drawing.Size(27, 13);
            this.lblSwConstrast.TabIndex = 30;
            this.lblSwConstrast.Text = "128";
            this.lblSwConstrast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(5, 85);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Constrast";
            // 
            // tbarSwConstrast
            // 
            this.tbarSwConstrast.Location = new System.Drawing.Point(5, 101);
            this.tbarSwConstrast.Maximum = 255;
            this.tbarSwConstrast.Name = "tbarSwConstrast";
            this.tbarSwConstrast.Size = new System.Drawing.Size(183, 45);
            this.tbarSwConstrast.TabIndex = 28;
            this.tbarSwConstrast.Value = 128;
            this.tbarSwConstrast.Scroll += new System.EventHandler(this.tbarSwConstrast_Scroll);
            // 
            // lblSwBrigthness
            // 
            this.lblSwBrigthness.Location = new System.Drawing.Point(157, 11);
            this.lblSwBrigthness.Name = "lblSwBrigthness";
            this.lblSwBrigthness.Size = new System.Drawing.Size(27, 13);
            this.lblSwBrigthness.TabIndex = 27;
            this.lblSwBrigthness.Text = "128";
            this.lblSwBrigthness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "Brigthness";
            // 
            // tbarSwBrigthness
            // 
            this.tbarSwBrigthness.Location = new System.Drawing.Point(3, 27);
            this.tbarSwBrigthness.Maximum = 255;
            this.tbarSwBrigthness.Name = "tbarSwBrigthness";
            this.tbarSwBrigthness.Size = new System.Drawing.Size(183, 45);
            this.tbarSwBrigthness.TabIndex = 25;
            this.tbarSwBrigthness.Value = 128;
            this.tbarSwBrigthness.Scroll += new System.EventHandler(this.tbarSwBrigthness_Scroll);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnResetRotate);
            this.panel3.Controls.Add(this.btnRotateCounterclockwise);
            this.panel3.Controls.Add(this.btnRotateClockwise);
            this.panel3.Controls.Add(this.cmbVideoMirror);
            this.panel3.Controls.Add(this.btnResetColorAdjustments);
            this.panel3.Controls.Add(this.cmbVideoEnhancement);
            this.panel3.Controls.Add(this.cmbDeInterlance);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(198, 282);
            this.panel3.TabIndex = 1;
            // 
            // btnResetRotate
            // 
            this.btnResetRotate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetRotate.BackgroundImage = global::AverMediaTestApp.Properties.Resources.reset_rotate;
            this.btnResetRotate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetRotate.Location = new System.Drawing.Point(76, 87);
            this.btnResetRotate.Name = "btnResetRotate";
            this.btnResetRotate.Size = new System.Drawing.Size(34, 32);
            this.btnResetRotate.TabIndex = 30;
            this.btnResetRotate.UseVisualStyleBackColor = true;
            this.btnResetRotate.Click += new System.EventHandler(this.btnRotateReset_Click);
            // 
            // btnRotateCounterclockwise
            // 
            this.btnRotateCounterclockwise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRotateCounterclockwise.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRotateCounterclockwise.BackgroundImage")));
            this.btnRotateCounterclockwise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRotateCounterclockwise.Location = new System.Drawing.Point(40, 87);
            this.btnRotateCounterclockwise.Name = "btnRotateCounterclockwise";
            this.btnRotateCounterclockwise.Size = new System.Drawing.Size(34, 32);
            this.btnRotateCounterclockwise.TabIndex = 29;
            this.btnRotateCounterclockwise.UseVisualStyleBackColor = true;
            this.btnRotateCounterclockwise.Click += new System.EventHandler(this.btnRotateCounterclockwise_Click);
            // 
            // btnRotateClockwise
            // 
            this.btnRotateClockwise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRotateClockwise.BackgroundImage = global::AverMediaTestApp.Properties.Resources.clockwise_rotate;
            this.btnRotateClockwise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRotateClockwise.Location = new System.Drawing.Point(111, 87);
            this.btnRotateClockwise.Name = "btnRotateClockwise";
            this.btnRotateClockwise.Size = new System.Drawing.Size(38, 32);
            this.btnRotateClockwise.TabIndex = 28;
            this.btnRotateClockwise.UseVisualStyleBackColor = true;
            this.btnRotateClockwise.Click += new System.EventHandler(this.btnRotateClockwise_Click);
            // 
            // cmbVideoMirror
            // 
            this.cmbVideoMirror.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVideoMirror.FormattingEnabled = true;
            this.cmbVideoMirror.Items.AddRange(new object[] {
            "Video Mirror",
            "None",
            "Horizontal",
            "Vertical",
            "Both"});
            this.cmbVideoMirror.Location = new System.Drawing.Point(5, 60);
            this.cmbVideoMirror.Name = "cmbVideoMirror";
            this.cmbVideoMirror.Size = new System.Drawing.Size(182, 21);
            this.cmbVideoMirror.TabIndex = 27;
            this.cmbVideoMirror.SelectedIndexChanged += new System.EventHandler(this.cmbVideoMirror_SelectedIndexChanged);
            // 
            // btnResetColorAdjustments
            // 
            this.btnResetColorAdjustments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnResetColorAdjustments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnResetColorAdjustments.Image = ((System.Drawing.Image)(resources.GetObject("btnResetColorAdjustments.Image")));
            this.btnResetColorAdjustments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetColorAdjustments.Location = new System.Drawing.Point(0, 244);
            this.btnResetColorAdjustments.Name = "btnResetColorAdjustments";
            this.btnResetColorAdjustments.Size = new System.Drawing.Size(198, 38);
            this.btnResetColorAdjustments.TabIndex = 26;
            this.btnResetColorAdjustments.Text = "Reset Color Adjustments";
            this.btnResetColorAdjustments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnResetColorAdjustments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetColorAdjustments.UseVisualStyleBackColor = true;
            this.btnResetColorAdjustments.Click += new System.EventHandler(this.btnResetColorAdjustments_Click);
            // 
            // cmbVideoEnhancement
            // 
            this.cmbVideoEnhancement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVideoEnhancement.FormattingEnabled = true;
            this.cmbVideoEnhancement.Items.AddRange(new object[] {
            "Video Enhancement",
            "None",
            "Normal",
            "Split",
            "Compare"});
            this.cmbVideoEnhancement.Location = new System.Drawing.Point(5, 31);
            this.cmbVideoEnhancement.Name = "cmbVideoEnhancement";
            this.cmbVideoEnhancement.Size = new System.Drawing.Size(182, 21);
            this.cmbVideoEnhancement.TabIndex = 3;
            this.cmbVideoEnhancement.SelectedIndexChanged += new System.EventHandler(this.cmbVideoEnhancement_SelectedIndexChanged);
            // 
            // cmbDeInterlance
            // 
            this.cmbDeInterlance.DisplayMember = "None";
            this.cmbDeInterlance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeInterlance.FormattingEnabled = true;
            this.cmbDeInterlance.Items.AddRange(new object[] {
            "DeInterlance",
            "None",
            "Weave",
            "Bob",
            "Blend"});
            this.cmbDeInterlance.Location = new System.Drawing.Point(5, 5);
            this.cmbDeInterlance.Name = "cmbDeInterlance";
            this.cmbDeInterlance.Size = new System.Drawing.Size(182, 21);
            this.cmbDeInterlance.TabIndex = 2;
            this.cmbDeInterlance.SelectedIndexChanged += new System.EventHandler(this.cmbDeInterlance_SelectedIndexChanged);
            // 
            // pnlImage
            // 
            this.pnlImage.Controls.Add(this.btnCrosshairKeyTrackingLock);
            this.pnlImage.Controls.Add(this.pboxImage);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(0, 55);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(784, 627);
            this.pnlImage.TabIndex = 32;
            // 
            // pboxImage
            // 
            this.pboxImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pboxImage.Location = new System.Drawing.Point(0, 0);
            this.pboxImage.Name = "pboxImage";
            this.pboxImage.Size = new System.Drawing.Size(784, 627);
            this.pboxImage.TabIndex = 5;
            this.pboxImage.TabStop = false;
            // 
            // btnCrosshairKeyTrackingLock
            // 
            this.btnCrosshairKeyTrackingLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrosshairKeyTrackingLock.BackgroundImage = global::AverMediaTestApp.Properties.Resources._lock;
            this.btnCrosshairKeyTrackingLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCrosshairKeyTrackingLock.Location = new System.Drawing.Point(744, 590);
            this.btnCrosshairKeyTrackingLock.Name = "btnCrosshairKeyTrackingLock";
            this.btnCrosshairKeyTrackingLock.Size = new System.Drawing.Size(34, 32);
            this.btnCrosshairKeyTrackingLock.TabIndex = 32;
            this.btnCrosshairKeyTrackingLock.UseVisualStyleBackColor = true;
            this.btnCrosshairKeyTrackingLock.Click += new System.EventHandler(this.btnCrosshairKeyTrackingLock_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 682);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Avermedia Capture Card App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControlFilter.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHwSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHwHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHwConstrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarHwBrigthness)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSwSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSwHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSwConstrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSwBrigthness)).EndInit();
            this.panel3.ResumeLayout(false);
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBoxDeviceList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDeviceSettings;
        private System.Windows.Forms.TabControl tabControlFilter;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblHwSaturation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar tbarHwSaturation;
        private System.Windows.Forms.Label lblHwHue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar tbarHwHue;
        private System.Windows.Forms.Label lblHwConstrast;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tbarHwConstrast;
        private System.Windows.Forms.Label lblHwBrigthness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbarHwBrigthness;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblSwSaturation;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar tbarSwSaturation;
        private System.Windows.Forms.Label lblSwHue;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TrackBar tbarSwHue;
        private System.Windows.Forms.Label lblSwConstrast;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TrackBar tbarSwConstrast;
        private System.Windows.Forms.Label lblSwBrigthness;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TrackBar tbarSwBrigthness;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbVideoEnhancement;
        private System.Windows.Forms.ComboBox cmbDeInterlance;
        private System.Windows.Forms.Button btnResetColorAdjustments;
        private System.Windows.Forms.ComboBox cmbVideoMirror;
        private System.Windows.Forms.Button btnResetRotate;
        private System.Windows.Forms.Button btnRotateCounterclockwise;
        private System.Windows.Forms.Button btnRotateClockwise;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox pboxImage;
        private System.Windows.Forms.Button btnCrosshairKeyTrackingLock;
    }
}

