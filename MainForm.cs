using AverMediaLib;
using AverMediaTestApp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AverMediaTestApp
{
    public partial class MainForm : Form
    {        
        AvermediaTools avermediaTools;
        DeviceSettingsForm deviceSettingsForm;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            InitMainWindow();
        }

        private void KeyHookTool_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (avermediaTools == null || !avermediaTools.IsStartStreaming || avermediaTools.IsLockCrosshair)
                return;

            int widthOffset = 0, heightOffset=0;
            switch(e.KeyCode)
            {
                case Keys.Up:
                    heightOffset--;
                    break;
                case Keys.Down:
                    heightOffset++;
                    break;
                case Keys.Left:
                    widthOffset--;
                    break;
                case Keys.Right:
                    widthOffset++;
                    break;
            }

            avermediaTools.ChangeCrosshairPosition(widthOffset, heightOffset);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            avermediaTools.Close();
            KeyHookTool.UnHook();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            avermediaTools.Resize();
        }

        private int InitMainWindow()
        {
            this.btnStartStop.BackgroundImage = AverMediaTestApp.Properties.Resources.start_video;
            this.cmbBoxDeviceList.Enabled = false;
            this.btnStartStop.Enabled = false;
            this.btnCapture.Enabled = false;
            this.btnDeviceSettings.Enabled = false;
            this.tabControlFilter.Enabled = false;


            cmbBoxDeviceList.SelectedIndexChanged += SelectCaptureDevice_Click;

            avermediaTools = new AvermediaTools(pboxImage);
            List<AvermediaDeviceInfo> avermediaDeviceInfoList = avermediaTools.GetVideoCardList();

            cmbDeInterlance.SelectedIndex = 0;
            cmbVideoEnhancement.SelectedIndex = 0;
            cmbVideoMirror.SelectedIndex = 0;

            string deviceName;
            string savedDeviceName = avermediaTools.GetSavedDeviceName();
            int iSelectedDeviceIndex = -1;
            ComboBoxItem comboBoxItem;

            for (int i = 0; i < avermediaDeviceInfoList.Count; ++i)
            {
                comboBoxItem = new ComboBoxItem();
                comboBoxItem.Text = avermediaDeviceInfoList[i].DeviceName;
                comboBoxItem.Value = avermediaDeviceInfoList[i];
                cmbBoxDeviceList.Items.Add(comboBoxItem);

                deviceName = comboBoxItem.Text.Substring(comboBoxItem.Text.IndexOf(':') + 1);
                if (savedDeviceName == deviceName)
                {
                    iSelectedDeviceIndex = i;
                }
            }

            this.cmbBoxDeviceList.Enabled = true;

            if (iSelectedDeviceIndex != -1)
            {
                cmbBoxDeviceList.SelectedIndex = iSelectedDeviceIndex;
            }

            avermediaTools.IsLockCrosshair = true;
            btnCrosshairKeyTrackingLock.BackgroundImage = AverMediaTestApp.Properties.Resources._lock;

            KeyHookTool.OnKeyDown += KeyHookTool_OnKeyDown;
            KeyHookTool.SetHook();

            return 0;
        }
               
        public void stopStreaming()
        {
            avermediaTools.stopStreaming();
        }

        public void startStreaming()
        {
            avermediaTools.startStreaming("Your Name", true);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (avermediaTools.IsStartStreaming)
            {
                this.btnStartStop.BackgroundImage = AverMediaTestApp.Properties.Resources.start_video;
                stopStreaming();
                SetDefaultColorAdjustments();
                this.btnCapture.Enabled = false;
                this.tabControlFilter.Enabled = false;
                //avermediaTools.UpdateDemoState(DEMOSTATE.DEMO_STATE_STOP, true);
            }
            else
            {
                this.btnStartStop.BackgroundImage = AverMediaTestApp.Properties.Resources.stop_video;
                startStreaming();
                InitColorAdjustments();
                if (avermediaTools.HasCaptureSupported())
                    this.btnCapture.Enabled = true;
                this.tabControlFilter.Enabled = true;
                //avermediaTools.UpdateDemoState(DEMOSTATE.DEMO_STATE_PREVIEW, true);
            }
        }

        private void SelectCaptureDevice_Click(object sender, EventArgs e)
        {
            ComboBoxItem selectedDeviceItem = (sender as ComboBox).SelectedItem as ComboBoxItem;
            AvermediaDeviceInfo avermediaDeviceInfo = selectedDeviceItem.Value as AvermediaDeviceInfo;
            avermediaTools.LoadVideoCardInfo(avermediaDeviceInfo);

            this.btnStartStop.Enabled = true;            
            this.btnDeviceSettings.Enabled = true;

            InitVideoCardInfo();
            InitColorAdjustments();
        }

        private void InitVideoCardInfo()
        {
            cmbDeInterlance.SelectedItem = avermediaTools.deInterlaceMode.ToString().Replace("DEINTERLACE_", "").FirstCharToUpper();
            cmbDeInterlance_SelectedIndexChanged(null, null);

            cmbVideoEnhancement.SelectedItem = avermediaTools.videoEnhance.ToString().Replace("VIDEOENHANCE_", "").FirstCharToUpper();
            cmbVideoEnhancement_SelectedIndexChanged(null, null);

            cmbVideoMirror.SelectedItem = avermediaTools.videoMirrorMode.ToString().Replace("VIDEOMIRROR_", "").FirstCharToUpper();
            cmbVideoMirror_SelectedIndexChanged(null, null);
        }

        private void InitColorAdjustments()
        {
            tbarHwBrigthness.Value = avermediaTools.HwBrightness;
            tbarHwBrigthness_Scroll(null, null);
            tbarHwConstrast.Value = avermediaTools.HwConstrast;
            tbarHwConstrast_Scroll(null, null);
            tbarHwHue.Value = avermediaTools.HwHue;
            tbarHwHue_Scroll(null, null);
            tbarHwSaturation.Value = avermediaTools.HwSaturation;
            tbarHwSaturation_Scroll(null, null);

            tbarSwBrigthness.Value = avermediaTools.SwBrightness;
            tbarSwBrigthness_Scroll(null, null);
            tbarSwConstrast.Value = avermediaTools.SwConstrast;
            tbarSwConstrast_Scroll(null, null);
            tbarSwHue.Value = avermediaTools.SwHue;
            tbarSwHue_Scroll(null, null);
            tbarSwSaturation.Value = avermediaTools.SwSaturation;
            tbarSwSaturation_Scroll(null, null);
        }
        
        private void SetDefaultColorAdjustments()
        {
            tbarHwBrigthness.Value = 128;
            tbarHwBrigthness_Scroll(null, null);
            tbarHwConstrast.Value = 128;
            tbarHwConstrast_Scroll(null, null);
            tbarHwHue.Value = 128;
            tbarHwHue_Scroll(null, null);
            tbarHwSaturation.Value = 128;
            tbarHwSaturation_Scroll(null, null);

            tbarSwBrigthness.Value = 128;
            tbarSwBrigthness_Scroll(null, null);
            tbarSwConstrast.Value = 128;
            tbarSwConstrast_Scroll(null, null);
            tbarSwHue.Value = 128;
            tbarSwHue_Scroll(null, null);
            tbarSwSaturation.Value = 128;
            tbarSwSaturation_Scroll(null, null);
        }

        private void btnDeviceSettings_Click(object sender, EventArgs e)
        {
            if (deviceSettingsForm == null)
            {
                deviceSettingsForm = new DeviceSettingsForm(avermediaTools);
                deviceSettingsForm.Owner = this;
            }
            else
            {
                deviceSettingsForm.Focus();
            }

            avermediaTools.deviceSetting = deviceSettingsForm;

            deviceSettingsForm.ShowDialog();
            deviceSettingsForm.Focus();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.AddExtension = true;
            saveFileDialog.OverwritePrompt = false;
            saveFileDialog.Filter = "PNG File (*.png)|*.png";
            saveFileDialog.FileName = "AvermediaCaptureImage.png";
            saveFileDialog.DefaultExt = "png";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                avermediaTools.CaptureImage(saveFileDialog.FileName, IMAGETYPE.IMAGETYPE_PNG);
            }
        }

        private void tbarHwBrigthness_Scroll(object sender, EventArgs e)
        {
            lblHwBrigthness.Text = tbarHwBrigthness.Value.ToString();

            if (avermediaTools.IsStartStreaming)
                avermediaTools.SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS, (uint)tbarHwBrigthness.Value);
        }

        private void tbarHwConstrast_Scroll(object sender, EventArgs e)
        {
            lblHwConstrast.Text = tbarHwConstrast.Value.ToString();

            if (avermediaTools.IsStartStreaming) 
                avermediaTools.SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST, (uint)tbarHwConstrast.Value);
        }

        private void tbarHwHue_Scroll(object sender, EventArgs e)
        {
            lblHwHue.Text = tbarHwHue.Value.ToString();

            if (avermediaTools.IsStartStreaming)
                avermediaTools.SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE, (uint)tbarHwHue.Value);
        }

        private void tbarHwSaturation_Scroll(object sender, EventArgs e)
        {
            lblHwSaturation.Text = tbarHwSaturation.Value.ToString();

            if (avermediaTools.IsStartStreaming)
                avermediaTools.SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION, (uint)tbarHwSaturation.Value);
        }

        private void tbarSwBrigthness_Scroll(object sender, EventArgs e)
        {
            lblSwBrigthness.Text = tbarSwBrigthness.Value.ToString();

            if (avermediaTools.IsStartStreaming)
                avermediaTools.SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS, (uint)tbarSwBrigthness.Value);

            int value = (int)avermediaTools.GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS);
        }

        private void tbarSwConstrast_Scroll(object sender, EventArgs e)
        {
            lblSwConstrast.Text = tbarSwConstrast.Value.ToString();

            if (avermediaTools.IsStartStreaming)
                avermediaTools.SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST, (uint)tbarSwConstrast.Value);
        }

        private void tbarSwHue_Scroll(object sender, EventArgs e)
        {
            lblSwHue.Text = tbarSwHue.Value.ToString();

            if (avermediaTools.IsStartStreaming)
                avermediaTools.SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE, (uint)tbarSwHue.Value);
        }

        private void tbarSwSaturation_Scroll(object sender, EventArgs e)
        {
            lblSwSaturation.Text = tbarSwSaturation.Value.ToString();

            if (avermediaTools.IsStartStreaming)
                avermediaTools.SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION, (uint)tbarSwSaturation.Value);
        }
                
        private void btnRotateClockwise_Click(object sender, EventArgs e)
        {
            if (avermediaTools != null)
                avermediaTools.Rotate(VIDEOROTATE.VIDEOROTATE_CW90);
        }

        private void btnRotateCounterclockwise_Click(object sender, EventArgs e)
        {
            if (avermediaTools != null)
                avermediaTools.Rotate(VIDEOROTATE.VIDEOROTATE_CCW90);
        }

        private void btnRotateReset_Click(object sender, EventArgs e)
        {
            if (avermediaTools != null)
                avermediaTools.Rotate(VIDEOROTATE.VIDEOROTATE_NONE);
        }

        private void cmbDeInterlance_SelectedIndexChanged(object sender, EventArgs e)
        {
            string videoDeInterlanceText = "";
            if (cmbDeInterlance.SelectedIndex > 0)
                videoDeInterlanceText = cmbDeInterlance.SelectedItem.ToString();
            else
                videoDeInterlanceText = cmbDeInterlance.Items[1].ToString();

            DEINTERLACEMODE deInterlaceMode = DEINTERLACEMODE.DEINTERLACE_NONE;
            if (!string.IsNullOrWhiteSpace(videoDeInterlanceText))
            {
                deInterlaceMode = (DEINTERLACEMODE)Enum.Parse(typeof(DEINTERLACEMODE), "DEINTERLACE_" + videoDeInterlanceText.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            }
            if(avermediaTools != null)
                avermediaTools.SetDeInterlance(deInterlaceMode);
        }

        private void cmbVideoEnhancement_SelectedIndexChanged(object sender, EventArgs e)
        {
            string videoEnhancementText = "";
            if (cmbVideoEnhancement.SelectedIndex > 0)
                videoEnhancementText = cmbVideoEnhancement.SelectedItem.ToString();
            else
                videoEnhancementText = cmbVideoEnhancement.Items[1].ToString();

            VIDEOENHANCE videoEnhance = VIDEOENHANCE.VIDEOENHANCE_NONE;
            if (!string.IsNullOrWhiteSpace(videoEnhancementText))
            {
                videoEnhance = (VIDEOENHANCE)Enum.Parse(typeof(VIDEOENHANCE), "VIDEOENHANCE_" + videoEnhancementText.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            }
            if (avermediaTools != null)
                avermediaTools.SetVideoEnhancement(videoEnhance);
        }

        private void cmbVideoMirror_SelectedIndexChanged(object sender, EventArgs e)
        {
            string videoMirrorText = "";
            if (cmbVideoMirror.SelectedIndex > 0)
                videoMirrorText = cmbVideoMirror.SelectedItem.ToString();
            else
                videoMirrorText = cmbVideoMirror.Items[1].ToString();
                        
            VIDEOMIRROR videoMirrorMode = VIDEOMIRROR.VIDEOMIRROR_NONE;
            if (!string.IsNullOrWhiteSpace(videoMirrorText))
            {
                videoMirrorMode = (VIDEOMIRROR)Enum.Parse(typeof(VIDEOMIRROR), "VIDEOMIRROR_" + videoMirrorText.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            }
            if (avermediaTools != null)
                avermediaTools.SetVideoMirrorMode(videoMirrorMode);
        }

        private void btnResetColorAdjustments_Click(object sender, EventArgs e)
        {
            SetDefaultColorAdjustments();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if (avermediaTools != null)
                avermediaTools.SaveSettings();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            avermediaTools.ChangeCrosshairPosition(10,10);
        }

        private void btnCrosshairKeyTrackingLock_Click(object sender, EventArgs e)
        {
            if (avermediaTools.IsLockCrosshair)
            {
                avermediaTools.IsLockCrosshair = false;
                btnCrosshairKeyTrackingLock.BackgroundImage = AverMediaTestApp.Properties.Resources.unlock;
            }
            else
            {
                avermediaTools.IsLockCrosshair = true;
                btnCrosshairKeyTrackingLock.BackgroundImage = AverMediaTestApp.Properties.Resources._lock;
            }
        }

        bool viewState = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (viewState)
            {
                //avermediaTools.test();

                pboxImage.Width = (pnlImage.Width - 10) / 2;
                pboxImage.Height = (pnlImage.Height - 10) / 2;
                pboxImage.Location = new Point(0, 0);
            }
            else
            {
                pboxImage.Width = pnlImage.Width - 5;
                pboxImage.Height = pnlImage.Height - 5;
                pboxImage.Location = new Point(0, 0);
            }
            viewState = !viewState;

            avermediaTools.Resize();
        }
    }
}
