using AverMediaLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AverMediaTestApp
{
    public partial class DeviceSettingsForm : Form, IDeviceSettings
    {
        AvermediaTools avermediaTools;

        private uint m_uVideoSource;
        private uint m_uVideoFormat;
        private bool m_bIsRangeFramerate;
        private bool m_bIsRangeResolution;
        public bool isLoading;
        private bool hasAddedEvents;
        private int lastResolution;
        private int lastFrameRate;
        private int lastVideoSource;

        private Timer m_Timer;

        private List<uint> m_VideoSourceList;
        private List<uint> m_VideoResolutionList;

        public DeviceSettingsForm(AvermediaTools avermediaTools)
        {
            InitializeComponent();

            m_Timer = new Timer();
            m_uVideoSource = 0;
            m_uVideoFormat = 0;
            m_bIsRangeFramerate = false;
            m_bIsRangeResolution = false;
            m_VideoSourceList = new List<uint>();
            m_VideoResolutionList = new List<uint>();

            this.avermediaTools = avermediaTools;
            hasAddedEvents = false;
        }

        private void DeviceSettingsForm_Load(object sender, EventArgs e)
        {
            isLoading = true;
            uint dwVideoSource = 3;//hdmi
            uint dwFormat = 0;
            RESOLUTION_RANGE_INFO ResolutionRangeInfo = new RESOLUTION_RANGE_INFO();
            ResolutionRangeInfo.dwVersion = 1;
            int hr = AVerCapAPI.AVerGetVideoResolutionRangeSupported(avermediaTools.CaptureDeviceHandle, dwVideoSource, dwFormat, ref ResolutionRangeInfo);
            if (hr == (int)ERRORCODE.CAP_EC_SUCCESS)
            {
                if (ResolutionRangeInfo.bRange == 0)
                {
                    m_bIsRangeResolution = false;
                }
                else
                {
                    m_bIsRangeResolution = true;
                }
            }
            FRAMERATE_RANGE_INFO FrameRateRangeInfo = new FRAMERATE_RANGE_INFO();
            FrameRateRangeInfo.dwVersion = 1;
            hr = AVerCapAPI.AVerGetVideoInputFrameRateRangeSupported(avermediaTools.CaptureDeviceHandle, dwVideoSource, dwFormat, 0, 0, ref FrameRateRangeInfo);

            // AVerCapAPI.AVerGetVideoInput (CaptureDeviceHandle, dwVideoSource, dwFormat, ref  ResolutionRangeInfo);

            if (hr == (int)ERRORCODE.CAP_EC_SUCCESS)
            {
                if (FrameRateRangeInfo.bRange == 0)
                {
                    m_bIsRangeFramerate = false;
                }
                else
                {
                    m_bIsRangeFramerate = true;
                }
            }
            //add range
            InitVideoDevice();

            if (!hasAddedEvents)
            {
                cmbFrameRate.SelectedIndexChanged += cmbFrameRate_DropDownClosed;
                cmbResolution.SelectedIndexChanged += cmbResolution_DropDownClosed;
                cmbVideoSource.SelectedIndexChanged += cmbVideoSource_DropDownClosed;
                rbtnNTSC.CheckedChanged += rbtnNTSC_Checked;
                rbtnPAL.CheckedChanged += rbtnPAL_Checked;
            }
            isLoading = false;
            hasAddedEvents = true;
        }
        
        private void InitVideoDevice()
        {
            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_STOP || avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                txt_InputVideoInfo.Enabled = true;
                cmbVideoSource.Enabled = true;
                rbtnNTSC.Enabled = true;
                rbtnPAL.Enabled = true;
                cmbResolution.Enabled = true;                
                cmbFrameRate.Enabled = true;
            }
            else
            {
                txt_InputVideoInfo.Enabled = false;
                cmbVideoSource.Enabled = false;
                rbtnNTSC.Enabled = false;
                rbtnPAL.Enabled = false;
                cmbResolution.Enabled = false;
                cmbFrameRate.Enabled = false;
            }
            InitVideoFormat();
            InitVideoSource();
            InitInputVideoInfo();
            InitVideoResolution();
            InitFrameRate();

            if (m_Timer.Enabled == true)
            {
                m_Timer.Stop();
            }
            m_Timer.Interval = 1000;
            m_Timer.Tick += new EventHandler(m_Timer_Tick);
            m_Timer.Start();
        }

        private void m_Timer_Tick(object sender, EventArgs e)
        {
            InitInputVideoInfo();
        }

        private void InitVideoFormat()
        {
            uint uVideoFormat = 0;
            int iReturn = AVerCapAPI.AVerGetVideoFormat(avermediaTools.CaptureDeviceHandle, ref uVideoFormat);
            if (iReturn == (int)ERRORCODE.CAP_EC_NOT_SUPPORTED)
            {
                rbtnNTSC.Enabled = false;
                rbtnPAL.Enabled = false;
                return;
            }
            switch (uVideoFormat)
            {
                case (uint)VIDEOFORMAT.VIDEOFORMAT_NTSC:
                    rbtnNTSC.Checked = true;
                    break;
                case (uint)VIDEOFORMAT.VIDEOFORMAT_PAL:
                    rbtnPAL.Checked = true;
                    break;
            }
        }

        private void InitVideoResolution()
        {
            m_VideoResolutionList.Clear();
            cmbResolution.Items.Clear();
            
            AVerCapAPI.AVerGetVideoSource(avermediaTools.CaptureDeviceHandle, ref m_uVideoSource);
            AVerCapAPI.AVerGetVideoFormat(avermediaTools.CaptureDeviceHandle, ref m_uVideoFormat);
            uint SolutionNum = 0;
            AVerCapAPI.AVerGetVideoResolutionSupported(avermediaTools.CaptureDeviceHandle, m_uVideoSource, m_uVideoFormat, null, ref SolutionNum);
            uint[] pdwSupported = new uint[SolutionNum];
            AVerCapAPI.AVerGetVideoResolutionSupported(avermediaTools.CaptureDeviceHandle, m_uVideoSource, m_uVideoFormat, pdwSupported, ref SolutionNum);

            uint m_uVideoResolution = 0;
            VIDEO_RESOLUTION VideoResolution = avermediaTools.GetActualVideoResolution();
            m_uVideoResolution = VideoResolution.dwVideoResolution;
            int index = 0;
            foreach (uint i in pdwSupported)
            {
                m_VideoResolutionList.Add(i);
                
                switch (i)
                {
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_640X480:
                        cmbResolution.Items.Add("640X480");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_704X576:
                        cmbResolution.Items.Add("704X576");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_720X480:
                        cmbResolution.Items.Add("720X480");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_720X576:
                        cmbResolution.Items.Add("720X576");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1920X1080:
                        cmbResolution.Items.Add("1920X1080");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_160X120:
                        cmbResolution.Items.Add("160X120");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_176X144:
                        cmbResolution.Items.Add("176X144");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_240X176:
                        cmbResolution.Items.Add("240X176");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_240X180:
                        cmbResolution.Items.Add("240X180");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_320X240:
                        cmbResolution.Items.Add("320X240");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_352X240:
                        cmbResolution.Items.Add("352X240");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_352X288:
                        cmbResolution.Items.Add("352X288");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_640X240:
                        cmbResolution.Items.Add("640X240");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_640X288:
                        cmbResolution.Items.Add("640X288");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_720X240:
                        cmbResolution.Items.Add("720X240");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_720X288:
                        cmbResolution.Items.Add("720X288");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_80X60:
                        cmbResolution.Items.Add("80X60  ");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_88X72:
                        cmbResolution.Items.Add("88X72  ");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_128X96:
                        cmbResolution.Items.Add("128X96 ");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_640X576:
                        cmbResolution.Items.Add("640X576");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_180X120:
                        cmbResolution.Items.Add("180X120");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_180X144:
                        cmbResolution.Items.Add("180X144");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_360X240:
                        cmbResolution.Items.Add("360X240");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_360X288:
                        cmbResolution.Items.Add("360X288");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_768X576:
                        cmbResolution.Items.Add("768X576");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_384x288:
                        cmbResolution.Items.Add("384x288");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_192x144:
                        cmbResolution.Items.Add("192x144 ");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1280X720:
                        cmbResolution.Items.Add("1280X720");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1024X768:
                        cmbResolution.Items.Add("1024X768");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1280X800:
                        cmbResolution.Items.Add("1280X800");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1280X1024:
                        cmbResolution.Items.Add("1280X1024");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1440X900:
                        cmbResolution.Items.Add("1440X900");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1600X1200:
                        cmbResolution.Items.Add("1600X1200");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1680X1050:
                        cmbResolution.Items.Add("1680X1050");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_800X600:
                        cmbResolution.Items.Add("800X600");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1280X768:
                        cmbResolution.Items.Add("1280X768");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1360X768:
                        cmbResolution.Items.Add("1360X768");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1152X864:
                        cmbResolution.Items.Add("1152X864");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1280X960:
                        cmbResolution.Items.Add("1280X960");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_702X576:
                        cmbResolution.Items.Add("702X576");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_720X400:
                        cmbResolution.Items.Add("720X400");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1152X900:
                        cmbResolution.Items.Add("1152X900");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1360X1024:
                        cmbResolution.Items.Add("1360X1024");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1366X768:
                        cmbResolution.Items.Add("1366X768");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1400X1050:
                        cmbResolution.Items.Add("1400X1050");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1440X480:
                        cmbResolution.Items.Add("1440X480");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1440X576:
                        cmbResolution.Items.Add("1440X576");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1600X900:
                        cmbResolution.Items.Add("1600X900");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1920X1200:
                        cmbResolution.Items.Add("1920X1200");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1440X1080:
                        cmbResolution.Items.Add("1440X1080");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1600X1024:
                        cmbResolution.Items.Add("1600X1024");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_3840X2160:
                        cmbResolution.Items.Add("3840X2160");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1152X768:
                        cmbResolution.Items.Add("1152X768");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_176X120:
                        cmbResolution.Items.Add("176X120");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_704X480:
                        cmbResolution.Items.Add("704X480");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1792X1344:
                        cmbResolution.Items.Add("1792X1344");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1856X1392:
                        cmbResolution.Items.Add("1856X1392");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_1920X1440:
                        cmbResolution.Items.Add("1920X1440");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_2048X1152:
                        cmbResolution.Items.Add("2048X1152");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_2560X1080:
                        cmbResolution.Items.Add("2560X1080");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_2560X1440:
                        cmbResolution.Items.Add("2560X1440");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_2560X1600:
                        cmbResolution.Items.Add("2560X1600");
                        index = index + 1;
                        break;
                    case (uint)VIDEORESOLUTION.VIDEORESOLUTION_4096X2160:
                        cmbResolution.Items.Add("4096X2160");
                        index = index + 1;
                        break;

                }

                if (m_uVideoResolution == i)
                {

                    cmbResolution.SelectedIndex = index - 1;
                    lastResolution = cmbResolution.SelectedIndex;
                }
            }

            string videoWidth = VideoResolution.dwWidth.ToString();
            string videoHeight = VideoResolution.dwHeight.ToString();

            if (VideoResolution.bCustom == 1)
            {
                cmbResolution.Enabled = false;
            }
            else
            {
                cmbResolution.Enabled = true;
            }
        }

        public bool InitFrameRate()
        {
            if (m_bIsRangeFramerate)
            {
                cmbFrameRate.Enabled = false;
                //textBox_FrameRate.IsEnabled = true;
                //button_SetFrameRate.IsEnabled = true;
                uint uFrameRate = 0;
                int ir = AVerCapAPI.AVerGetVideoInputFrameRate(avermediaTools.CaptureDeviceHandle, ref uFrameRate);
                if (ir != (int)ERRORCODE.CAP_EC_SUCCESS)
                {
                    return false;
                }
                //textBox_FrameRate.Text = uFrameRate.ToString();
            }
            else
            {
                cmbFrameRate.Enabled = true;
                cmbFrameRate.Items.Clear();
                uint uNum = 0;
                int lr = 0;

                VIDEO_RESOLUTION VideoResolution = avermediaTools.GetActualVideoResolution();
                lr = AVerCapAPI.AVerGetVideoInputFrameRateSupportedEx(avermediaTools.CaptureDeviceHandle, m_uVideoSource, m_uVideoFormat, VideoResolution.dwVideoResolution, null, ref uNum);
                uint[] pdwSupported = new uint[uNum];
                if (lr != (int)ERRORCODE.CAP_EC_SUCCESS)
                {
                    if (lr == (int)ERRORCODE.CAP_EC_NOT_SUPPORTED)
                    {
                        cmbFrameRate.Enabled = false;
                        return true;
                    }
                    return false;
                }
                uint dwVideoFrameRate = 0;
                AVerCapAPI.AVerGetVideoInputFrameRate(avermediaTools.CaptureDeviceHandle, ref dwVideoFrameRate);
                lr = AVerCapAPI.AVerGetVideoInputFrameRateSupportedEx(avermediaTools.CaptureDeviceHandle, m_uVideoSource, m_uVideoFormat, VideoResolution.dwVideoResolution, pdwSupported, ref uNum);
                if (lr != (int)ERRORCODE.CAP_EC_SUCCESS)
                {
                    return false;
                }
                bool bSelected = false;
                for (int i = 0; i < uNum; i++)
                {
                    cmbFrameRate.Items.Add(pdwSupported[i].ToString());
                    if (pdwSupported[i] == dwVideoFrameRate)
                    {
                        cmbFrameRate.SelectedIndex = i;
                        lastFrameRate = cmbFrameRate.SelectedIndex;
                        bSelected = true;
                    }
                }
                if (!bSelected)
                {
                    cmbFrameRate.SelectedIndex = 0;
                    lastFrameRate = cmbFrameRate.SelectedIndex;
                }
            }
            return true;
        }

        private void InitVideoSource()
        {
            //Clear
            //Clear
            m_VideoSourceList.Clear();
            cmbVideoSource.Items.Clear();

            //Init


            uint SourceNum = 0;
            AVerCapAPI.AVerGetVideoSourceSupported(avermediaTools.CaptureDeviceHandle, null, ref SourceNum);
            uint[] pdwSupported = new uint[SourceNum];
            AVerCapAPI.AVerGetVideoSourceSupported(avermediaTools.CaptureDeviceHandle, pdwSupported, ref SourceNum);

            int index = 0;
            foreach (int i in pdwSupported)
            {
                switch (i)
                {
                    case 0:
                        cmbVideoSource.Items.Add("composite");
                        m_VideoSourceList.Add(0);
                        index = index + 1;
                        break;
                    case 1:
                        cmbVideoSource.Items.Add("S-Video");
                        m_VideoSourceList.Add(1);
                        index = index + 1;
                        break;
                    case 2:
                        cmbVideoSource.Items.Add("component");
                        m_VideoSourceList.Add(2);
                        index = index + 1;
                        break;
                    case 3:
                        cmbVideoSource.Items.Add("HDMI");
                        m_VideoSourceList.Add(3);
                        index = index + 1;
                        break;
                    case 4:
                        cmbVideoSource.Items.Add("VGA");
                        m_VideoSourceList.Add(4);
                        index = index + 1;
                        break;
                    case 5:
                        cmbVideoSource.Items.Add("SDI");
                        m_VideoSourceList.Add(5);
                        index = index + 1;
                        break;
                    case 6:
                        cmbVideoSource.Items.Add("ASI");
                        m_VideoSourceList.Add(6);
                        index = index + 1;
                        break;
                    case 7:
                        cmbVideoSource.Items.Add("DVI");
                        m_VideoSourceList.Add(7);
                        index = index + 1;
                        break;

                }
            }

            uint uVideoSource = 0;
            int nSelIndex = 0;
            AVerCapAPI.AVerGetVideoSource(avermediaTools.CaptureDeviceHandle, ref uVideoSource);
            for (int i = 0; i < index; i++)
            {
                if (uVideoSource == m_VideoSourceList[i])
                {
                    nSelIndex = i;
                    break;
                }
            }
            if (cmbVideoSource.Items.Count > 0)
            {
                cmbVideoSource.SelectedIndex = nSelIndex;
                lastVideoSource = cmbVideoSource.SelectedIndex;
            }
        }

        private void InitInputVideoInfo()
        {
            INPUT_VIDEO_INFO InputVideoInfo = new INPUT_VIDEO_INFO();
            InputVideoInfo.dwVersion = 2;
            int bSignalPresence = 0;
            String strInputInfo, strVideoInfo, strSignalPresence, strHDCPProtected, strAudioSamplingRate;
            if ((int)ERRORCODE.CAP_EC_SUCCESS != AVerCapAPI.AVerGetSignalPresence(avermediaTools.CaptureDeviceHandle, ref bSignalPresence))
            {
                txt_InputVideoInfo.Text = "Can't get signal presence.";
                return;
            }
            if (bSignalPresence != 0)
            {
                strSignalPresence = "     Signal Presence:    TRUE";
            }
            else
            {
                strSignalPresence = "     Signal Presence:    FALSE";
            }
            if ((int)ERRORCODE.CAP_EC_SUCCESS != AVerCapAPI.AVerGetVideoInfo(avermediaTools.CaptureDeviceHandle, ref InputVideoInfo))
            {
                txt_InputVideoInfo.Text = "Get input video info failed!";
                return;
            }
            double dFrameRate = InputVideoInfo.dwFramerate / 100.0;
            uint uWidth = InputVideoInfo.dwWidth;
            uint uHeight = InputVideoInfo.dwHeight;
            if (InputVideoInfo.bProgressive == 1)
            {
                strVideoInfo = string.Format("     Video:                    {0,-4:d}*{1,-4:d}@{2,4:f2}p", uWidth, uHeight, dFrameRate);
            }
            else
            {
                strVideoInfo = string.Format("     Video:                    {0,-4:d}*{1,-4:d}@{2,4:f2}i", uWidth, uHeight, dFrameRate * 2);
            }
            uint dwMode = 0;
            AVerCapAPI.AVerGetMacroVisionMode(avermediaTools.CaptureDeviceHandle, ref dwMode);
            if (dwMode > 0)
            {
                strHDCPProtected = "     HDCP Protected:   TRUE";
            }
            else
            {
                strHDCPProtected = "     HDCP Protected:   FALSE";
            }
            INPUT_AUDIO_INFO InputAudioInfo = new INPUT_AUDIO_INFO();
            InputAudioInfo.dwVersion = 1;
            if ((int)ERRORCODE.CAP_EC_SUCCESS != AVerCapAPI.AVerGetAudioInfo(avermediaTools.CaptureDeviceHandle, ref InputAudioInfo))
            {
                strAudioSamplingRate = "     Audio:                    Analog Not Support";
            }
            else
            {
                strAudioSamplingRate = string.Format("     Audio:                    {0,-6:d}Hz", InputAudioInfo.dwSamplingRate);
            }
            strInputInfo = "Input Source Status:\r\n\r\n" + strSignalPresence + "\r\n\r\n" + strHDCPProtected + "\r\n\r\n" + strVideoInfo + "\r\n\r\n" + strAudioSamplingRate;
            txt_InputVideoInfo.Text = strInputInfo;
        }

        private void DeviceSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Timer.Enabled == true)
            {
                m_Timer.Stop();
            }

            avermediaTools.SaveSettings();
        }

        public void UpdateDemoWindow(DEMOSTATE DemoState)
        {
            InitVideoDevice();
        }

        private void rbtnNTSC_Checked(object sender, EventArgs e)
        {
            if (isLoading)
                return;

            if(!rbtnNTSC.Checked)
                return;

            //add not supported
            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                avermediaTools.stopStreaming();
            }

            int lr = (int)ERRORCODE.CAP_EC_INIT_DEVICE_FAILED;
            try
            {
                lr = AVerCapAPI.AVerSetVideoFormat(avermediaTools.CaptureDeviceHandle, (uint)VIDEOFORMAT.VIDEOFORMAT_PAL);
            }
            catch (Exception exc) { lr = (int)ERRORCODE.CAP_EC_INIT_DEVICE_FAILED; }

            if (lr != (int)ERRORCODE.CAP_EC_SUCCESS)
            {
                rbtnNTSC.Checked = false;
            }

            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                avermediaTools.startStreaming(refreshDeviceSettings: false);
            }
            else
            {
                InitVideoDevice();
            }
        }

        private void rbtnPAL_Checked(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            if (!rbtnPAL.Checked)
                return;

            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                avermediaTools.stopStreaming();
            }
            AVerCapAPI.AVerSetVideoFormat(avermediaTools.CaptureDeviceHandle, (uint)VIDEOFORMAT.VIDEOFORMAT_PAL);
            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                avermediaTools.startStreaming(refreshDeviceSettings: false);
            }
        }

        private void cmbFrameRate_DropDownClosed(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            if (lastFrameRate == cmbFrameRate.SelectedIndex)
                return;

            uint dwVideoFrameRate = 0;
            if (uint.TryParse(cmbFrameRate.SelectedItem.ToString(), out dwVideoFrameRate))
            {
                lastFrameRate = cmbFrameRate.SelectedIndex;

                avermediaTools.ChangeFrameRate(dwVideoFrameRate);

                if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
                {
                    avermediaTools.stopStreaming();
                }
                
                if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
                {
                    avermediaTools.startStreaming(refreshDeviceSettings: false);
                }
            }
        }

        private void cmbResolution_DropDownClosed(object sender, EventArgs e)
        {
            if (isLoading)
                return;

            if (cmbResolution.SelectedIndex == -1 || lastResolution == cmbResolution.SelectedIndex)
                return;

            lastResolution = cmbResolution.SelectedIndex;

            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                avermediaTools.stopStreaming();
            }

            uint nVideoResolution = 0;
            int nSelIndex = cmbResolution.SelectedIndex;
            nVideoResolution = (uint)m_VideoResolutionList[nSelIndex];

            VIDEO_RESOLUTION VideoResolution = AVerCapAPI.GetVideoResolutionByIndex(nVideoResolution);
            VideoResolution.dwVersion = 1;
            VideoResolution.bCustom = 0;
            avermediaTools.ChangeVideoResolution(VideoResolution);
           
            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                avermediaTools.startStreaming(refreshDeviceSettings: false);
            }
        }

        private void cmbVideoSource_DropDownClosed(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            if (cmbVideoSource.SelectedIndex == lastVideoSource)
                return;

            lastVideoSource = cmbVideoSource.SelectedIndex;
            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                avermediaTools.stopStreaming();
            }
            uint VideoSource = m_VideoSourceList[(cmbVideoSource.SelectedIndex)];
            int lr = AVerCapAPI.AVerSetVideoSource(avermediaTools.CaptureDeviceHandle, VideoSource);
            
            if (lr != (int)ERRORCODE.CAP_EC_SUCCESS)
            {
                MessageBox.Show("Set Video Source failed!");
            }
            if (avermediaTools.m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                avermediaTools.startStreaming(refreshDeviceSettings: false);
            }
        }

    }
}


