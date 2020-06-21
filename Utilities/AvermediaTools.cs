using AverMediaLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AverMediaLib
{
    public class AvermediaTools
    {
        private int FALSE = 0;
        private int TRUE = 1;

        uint m_uDeviceNum = 0;
        int m_iCurrentDeviceIndex = -1;

        public bool IsLockCrosshair { get; set; }

        bool m_bIsStartStreaming = false;
        public bool IsStartStreaming { get { return m_bIsStartStreaming; } }

        IntPtr m_hCaptureDevice = IntPtr.Zero;
        public IntPtr CaptureDeviceHandle { get { return m_hCaptureDevice; } }
        public DEMOSTATE m_DemoState = DEMOSTATE.DEMO_STATE_STOP;

        bool m_bHadSetVideoRenderer = false;
        uint m_uCaptureType = 0;
        
        string m_szFileName = null;
        const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        Size sizeCrosshairImage = new Size(32, 32);

        public int HwBrightness = 128;
        public int HwConstrast = 128;
        public int HwHue = 128;
        public int HwSaturation = 128;

        public int SwBrightness=128;
        public int SwConstrast = 128;
        public int SwHue = 128;
        public int SwSaturation = 128;
        public string UserName = "";

        StringBuilder szDeviceName;

        uint m_uCurrentCardType = (uint)CARDTYPE.CARDTYPE_NULL;


        private NOTIFYEVENTCALLBACK m_NotifyEventCallback = new NOTIFYEVENTCALLBACK(NotifyEventCallback);

        uint m_iImageType = 0;
        string m_strSavePath;
        private CAPTURE_IMAGE_INFO m_CaptureImageInfo = new CAPTURE_IMAGE_INFO();

        private VIDEO_COLOR_ADJUSTMENT m_sVideoColorAdjustment = new VIDEO_COLOR_ADJUSTMENT();
        List<OVERLAY_CONTENT> m_OverContentList = new List<OVERLAY_CONTENT>();

        private int m_iIsMaintainEnable = 3;

        
        public VIDEOROTATE videoRotate = VIDEOROTATE.VIDEOROTATE_NONE;
        public VIDEOENHANCE videoEnhance = VIDEOENHANCE.VIDEOENHANCE_NONE;
        public DEINTERLACEMODE deInterlaceMode = DEINTERLACEMODE.DEINTERLACE_NONE;
        public VIDEOMIRROR videoMirrorMode = VIDEOMIRROR.VIDEOMIRROR_NONE;

        VIDEO_RESOLUTION videoResolution;
        uint videoResolutionWidth, videoResolutionHeight;
        uint dwVideoFrameRate;

        public OVERLAY_POSITION crosshairOverlayPosition;

        [DllImport("kernel32.dll")]
        public static extern void GetNativeSystemInfo(ref SYSTEM_INFO SystemInfo);
        [DllImport("kernel32.dll")]
        public static extern uint GetPrivateProfileString(string szSectionName, string szKeyName, string szDefault,
                                                          StringBuilder szRetValue, uint uSize, string szFileName);
        [DllImport("kernel32.dll")]
        public static extern uint WritePrivateProfileString(string szSectionName, string szKeyName, string szValueName, string szFileName);

        public PictureBox picturebox;

        public IDeviceSettings deviceSetting;

        public AvermediaTools(PictureBox picturebox)
        {
            this.picturebox = picturebox;
            picturebox.Paint += new System.Windows.Forms.PaintEventHandler(this.pboxImage_Paint);

            this.crosshairOverlayPosition = new OVERLAY_POSITION();
            m_sVideoColorAdjustment.dwVersion = 1;

            m_iIsMaintainEnable = 1;
            AVerCapAPI.AVerSetMaintainAspectRatioEnabled(m_hCaptureDevice, m_iIsMaintainEnable);

            SetSettingFilePath();
        }

        private void pboxImage_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_hCaptureDevice != IntPtr.Zero)
            {
                AVerCapAPI.AVerRepaintVideo(m_hCaptureDevice);
            }
        }

        private void SetSettingFilePath()
        {
            SYSTEM_INFO SystemInfo = new SYSTEM_INFO();
            GetNativeSystemInfo(ref SystemInfo);
            string str = System.Environment.CurrentDirectory;
            if (SystemInfo.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_AMD64 ||
                SystemInfo.wProcessorArchitecture == PROCESSOR_ARCHITECTURE_IA64)
            {
                //64 bit os
                m_szFileName = str + "\\AVerCapSDK.ini";
            }
            else
            {
                //32 bit os
                m_szFileName = str + "\\AVerCapSDK.ini";
            }
        }

        public static int NotifyEventCallback(uint dwEventCode, IntPtr lpEventData, IntPtr lpUserData)
        {
            switch (dwEventCode)
            {
                case (uint)CAPTUREEVENT.EVENT_CAPTUREIMAGE:
                    {
                        if (lpUserData == null || lpEventData == null)
                            return 0;

                        GCHandle gchThis = GCHandle.FromIntPtr(lpUserData);
                        CAPTUREIMAGE_NOTIFY_INFO CaptureImageNotifyInfo = new CAPTUREIMAGE_NOTIFY_INFO();
                        CaptureImageNotifyInfo = (CAPTUREIMAGE_NOTIFY_INFO)Marshal.PtrToStructure(lpEventData, typeof(CAPTUREIMAGE_NOTIFY_INFO));
                        //if (((Form1)gchThis.Target).m_ShowCaptureImage != null)
                        //    ((Form1)gchThis.Target).m_ShowCaptureImage.ModifyName(ref CaptureImageNotifyInfo);
                    }
                    break;
                case (uint)CAPTUREEVENT.EVENT_CHECKCOPP:
                    {
                        uint plErrorID = (uint)Marshal.ReadInt32(lpEventData);
                        string strErrorID = "";
                        switch (plErrorID)
                        {
                            case (uint)COPPERRCODE.COPP_ERR_UNKNOWN:
                                strErrorID = "COPP_ERR_UNKNOWN";
                                break;
                            case (uint)COPPERRCODE.COPP_ERR_NO_COPP_HW:
                                strErrorID = "COPP_ERR_NO_COPP_HW";
                                break;
                            case (uint)COPPERRCODE.COPP_ERR_NO_MONITORS_CORRESPOND_TO_DISPLAY_DEVICE:
                                strErrorID = "COPP_ERR_NO_MONITORS_CORRESPOND_TO_DISPLAY_DEVICE";
                                break;
                            case (uint)COPPERRCODE.COPP_ERR_CERTIFICATE_CHAIN_FAILED:
                                strErrorID = "COPP_ERR_CERTIFICATE_CHAIN_FAILED";
                                break;
                            case (uint)COPPERRCODE.COPP_ERR_STATUS_LINK_LOST:
                                strErrorID = "COPP_ERR_STATUS_LINK_LOST";
                                break;
                            case (uint)COPPERRCODE.COPP_ERR_NO_HDCP_PROTECTION_TYPE:
                                strErrorID = "COPP_ERR_NO_HDCP_PROTECTION_TYPE";
                                break;
                            case (uint)COPPERRCODE.COPP_ERR_HDCP_REPEATER:
                                strErrorID = "COPP_ERR_HDCP_REPEATER";
                                break;
                            case (uint)COPPERRCODE.COPP_ERR_HDCP_PROTECTED_CONTENT:
                                strErrorID = "COPP_ERR_HDCP_PROTECTED_CONTENT";
                                break;
                            case (uint)COPPERRCODE.COPP_ERR_GET_CRL_FAILED:
                                strErrorID = "COPP_ERR_GET_CRL_FAILED";
                                break;
                        }
                        MessageBox.Show(strErrorID);
                    }
                    break;
                default:
                    return 0;
            }
            return 1;
        }

        public List<AvermediaDeviceInfo> GetVideoCardList()
        {
            AvermediaDeviceInfo avermediaDeviceInfo;
            List<AvermediaDeviceInfo> videoCardList = new List<AvermediaDeviceInfo>();
            szDeviceName = new StringBuilder("", 260);
            if (AVerCapAPI.AVerGetDeviceNum(ref m_uDeviceNum) == (int)ERRORCODE.CAP_EC_SUCCESS)
            {
                uint videoCardType;
                string videoCardTypeText, deviceNameText;
                StringBuilder deviceName;
                for (int i = 0; i < m_uDeviceNum; ++i)
                {
                    deviceName = new StringBuilder("", 260);
                    if (AVerCapAPI.AVerGetDeviceName((uint)i, deviceName) == (int)ERRORCODE.CAP_EC_SUCCESS)
                    {
                        videoCardType = GetVideoCardType(i);
                        if (videoCardType == (uint)CARDTYPE.CARDTYPE_C727 || videoCardType == (uint)CARDTYPE.CARDTYPE_C729 || videoCardType == (uint)CARDTYPE.CARDTYPE_C129)
                        {
                            videoCardTypeText = "SD";
                            deviceNameText = deviceName.ToString() + " - " + videoCardTypeText + " Device";
                            avermediaDeviceInfo = new AvermediaDeviceInfo(i, deviceNameText, videoCardTypeText);
                            videoCardList.Add(avermediaDeviceInfo);

                            videoCardTypeText = "HD";
                            deviceNameText = deviceName.ToString() + " - " + videoCardTypeText + " Device";
                            avermediaDeviceInfo = new AvermediaDeviceInfo(i, deviceNameText, videoCardTypeText);
                            videoCardList.Add(avermediaDeviceInfo);
                        }
                        else
                        {
                            videoCardTypeText = "";
                            deviceNameText = deviceName.ToString();
                            avermediaDeviceInfo = new AvermediaDeviceInfo(i, deviceNameText, videoCardTypeText);
                            videoCardList.Add(avermediaDeviceInfo);
                        }
                    }
                }
            }

            return videoCardList;
        }

        public void InitCapture()
        {
            m_iImageType = (uint)IMAGETYPE.IMAGETYPE_PNG; //png 
            m_CaptureImageInfo.dwVersion = 1;
            m_CaptureImageInfo.dwImageType = (uint)IMAGETYPE.IMAGETYPE_PNG;
            m_CaptureImageInfo.dwCaptureType = (uint)CT_SEQUENCE.CT_SEQUENCE_FRAME;
            m_CaptureImageInfo.bOverlayMix = 1;
            m_CaptureImageInfo.dwDurationMode = (uint)DURATIONMODE.DURATION_COUNT;
            m_CaptureImageInfo.dwCapNumPerSec = 0;
            //m_CaptureImageInfo.lpFileName=m_strSavePath;
            m_CaptureImageInfo.dwDuration = 10;
        }

        public bool HasCaptureSupported()
        {
            bool result = true;
            if (AVerCapAPI.AVerCaptureImageStart(m_hCaptureDevice, ref m_CaptureImageInfo) == (int)ERRORCODE.CAP_EC_NOT_SUPPORTED)
            {
                result = false;
            }

            return result;
        }

        public bool CaptureImage(string imagePath, IMAGETYPE imageType)
        {
            m_CaptureImageInfo.lpFileName = imagePath;
            m_CaptureImageInfo.dwImageType = (uint)imageType;
            m_CaptureImageInfo.dwCaptureType = (uint)CT_SEQUENCE.CT_SEQUENCE_FRAME;
            m_CaptureImageInfo.bOverlayMix = 1;
            try
            {
                System.IO.FileStream Tempfile = System.IO.File.Create(imagePath);
                Tempfile.Close();
                System.IO.File.Delete(imagePath);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Invalid Path.", imagePath);
                return false;
            }

            m_CaptureImageInfo.dwDurationMode = (uint)DURATIONMODE.DURATION_COUNT;
            m_CaptureImageInfo.dwDuration = 1;
            m_CaptureImageInfo.dwVersion = 1;
            int iRetVal = AVerCapAPI.AVerCaptureImageStart(m_hCaptureDevice, ref m_CaptureImageInfo);
            if (iRetVal != 0)
            {
                if (iRetVal == (int)ERRORCODE.CAP_EC_HDCP_PROTECTED_CONTENT)
                {
                    MessageBox.Show("The program content is protected!");
                    return false;
                }
                MessageBox.Show("The width and height of capture area are not a multiple of 2 or out of range.");
                return false;
            }
            System.Threading.Thread.Sleep(1000);
            AVerCapAPI.AVerCaptureImageStop(m_hCaptureDevice, 0);

            return true;
        }

        public string GetSavedDeviceName()
        {
            StringBuilder szValue = new StringBuilder("", 50);
            GetPrivateProfileString("DeviceName", "DeviceName", "", szValue, 50, m_szFileName);

            string strLastDeciveName = szValue.ToString();
            strLastDeciveName = strLastDeciveName.Substring(strLastDeciveName.IndexOf(':') + 1);

            return strLastDeciveName;
        }
        
        public uint GetVideoCardType(int iCurrentDeviceIndex)
        {
            if (iCurrentDeviceIndex < 0)
                return (uint)CARDTYPE.CARDTYPE_NULL;
            uint DeviceType = 0;

            AVerCapAPI.AVerGetDeviceType((uint)iCurrentDeviceIndex, ref DeviceType);
            return DeviceType;
        }

        public void LoadVideoCardInfo(AvermediaDeviceInfo selectedDeviceInfo)
        {
            m_DemoState = DEMOSTATE.DEMO_STATE_STOP;

            m_iCurrentDeviceIndex = selectedDeviceInfo.DeviceNumber;
            
            if (selectedDeviceInfo.DeviceType == "SD")
            {
                m_uCaptureType = (int)CAPTURETYPE.CAPTURETYPE_SD;
            }
            else
            {
                m_uCaptureType = (int)CAPTURETYPE.CAPTURETYPE_HD;
            }

            DeleteCaptureDevice();

            int iRetVal = 0;
            if (m_uCaptureType == (int)CAPTURETYPE.CAPTURETYPE_SD)
            {
                iRetVal = AVerCapAPI.AVerCreateCaptureObjectEx((uint)m_iCurrentDeviceIndex, (uint)CAPTURETYPE.CAPTURETYPE_SD, picturebox.Handle, ref m_hCaptureDevice);
            }
            else
            {
                iRetVal = AVerCapAPI.AVerCreateCaptureObjectEx((uint)m_iCurrentDeviceIndex, (uint)CAPTURETYPE.CAPTURETYPE_HD, picturebox.Handle, ref m_hCaptureDevice);
            }
            switch (iRetVal)
            {
                case (int)ERRORCODE.CAP_EC_SUCCESS:
                    break;
                case (int)ERRORCODE.CAP_EC_DEVICE_IN_USE:
                    MessageBox.Show("The capture device has already been used.", "AVer Capture SDK");
                    return;
                default:
                    MessageBox.Show("Can't initialize the capture device.", "AVer Capture SDK");
                    return;
            }
            
            SetVideoCardSettings(selectedDeviceInfo.DeviceName);
        }

        private void DeleteCaptureDevice()
        {
            if (m_bIsStartStreaming)
            {
                if (m_hCaptureDevice != (IntPtr)0)
                    AVerCapAPI.AVerStopStreaming(m_hCaptureDevice);
                m_bIsStartStreaming = false;
            }
            if (m_hCaptureDevice != (IntPtr)0)
                AVerCapAPI.AVerDeleteCaptureObject(m_hCaptureDevice);
            m_hCaptureDevice = (IntPtr)0;
        }
        
        public void stopStreaming()
        {
            if (m_bIsStartStreaming)
            {
                AVerCapAPI.AVerStopStreaming(m_hCaptureDevice);
                m_bIsStartStreaming = false;
            }
            AVerCapAPI.AVerSetEventCallback(m_hCaptureDevice, null, 0, IntPtr.Zero);
            picturebox.Refresh();
        }

        public void startStreaming(string userName="", bool refreshDeviceSettings=false)
        {
            if (!m_bHadSetVideoRenderer)
                AVerCapAPI.AVerSetVideoRenderer(m_hCaptureDevice, (uint)VIDEORENDERER.VIDEORENDERER_EVR);

            GCHandle gchThis = GCHandle.Alloc(this);
            AVerCapAPI.AVerSetEventCallback(m_hCaptureDevice, m_NotifyEventCallback, 0, GCHandle.ToIntPtr(gchThis));

            //get device setting            
            AVerCapAPI.AVerSetVideoRotateMode(m_hCaptureDevice, (uint)this.videoRotate);

            if (AVerCapAPI.AVerStartStreaming(m_hCaptureDevice) != (int)ERRORCODE.CAP_EC_SUCCESS)
            {
                MessageBox.Show("Can't start streaming", "AVer Capture SDK");
                return;
            }
            m_iIsMaintainEnable = 1;
            AVerCapAPI.AVerSetMaintainAspectRatioEnabled(m_hCaptureDevice, m_iIsMaintainEnable);

            if(!string.IsNullOrEmpty(userName))
                this.UserName = userName;

            SetVideoResolutionInfo();
            InitOverlayImage();
            SetOverlayTime();
            SetOverlayCrosshair();
            SetOverlayText(this.UserName);
            InitColorAdjustments();
            SetDeInterlance(this.deInterlaceMode);
            SetVideoEnhancement(this.videoEnhance);
            SetVideoMirrorMode(this.videoMirrorMode);

            RECT RectClient = new RECT();
            RectClient.Left = 0;
            RectClient.Top = 0;
            RectClient.Right = picturebox.Width;
            RectClient.Bottom = picturebox.Height;
            AVerCapAPI.AVerSetVideoWindowPosition(m_hCaptureDevice, RectClient);
            m_bIsStartStreaming = true;
            UpdateDemoState(DEMOSTATE.DEMO_STATE_PREVIEW, true, refreshDeviceSettings);
        }

        public void UpdateDemoState(DEMOSTATE DemoState, bool bSwitch, bool refreshDeviceSettings)
        {
            if (DemoState == DEMOSTATE.DEMO_STATE_STOP)
            {
                m_DemoState = DEMOSTATE.DEMO_STATE_STOP;
            }
            if (bSwitch)
            {
                m_DemoState = m_DemoState | DemoState;

                if(refreshDeviceSettings && deviceSetting != null)
                    deviceSetting.UpdateDemoWindow(m_DemoState);

                //if (m_ShowPreviewSetting != null)
                //{
                //    m_ShowPreviewSetting.UpdateDemoWindow(m_DemoState);
                //}
                //if (m_ShowCaptureImage != null)
                //{
                //    m_ShowCaptureImage.UpdateDemoWindow(m_DemoState);
                //}
                //if (m_ShowVideoProcess != null)
                //{
                //    m_ShowVideoProcess.UpdateDemoWindow(m_DemoState);
                //}
                //if (m_ShowRecord != null)
                //{
                //    m_ShowRecord.UpdateDemoWindow(m_DemoState);
                //}
            }
            else
            {
                m_DemoState = m_DemoState & (~DemoState);

                if (refreshDeviceSettings && deviceSetting != null)
                    deviceSetting.UpdateDemoWindow(m_DemoState);
                
                //if (m_ShowPreviewSetting != null)
                //{
                //    m_ShowPreviewSetting.UpdateDemoWindow(m_DemoState);
                //}
                //if (m_ShowCaptureImage != null)
                //{
                //    m_ShowCaptureImage.UpdateDemoWindow(m_DemoState);
                //}
                //if (m_ShowVideoProcess != null)
                //{
                //    m_ShowVideoProcess.UpdateDemoWindow(m_DemoState);
                //}

                //if (m_ShowRecord != null)
                //{
                //    m_ShowRecord.UpdateDemoWindow(m_DemoState);
                //}
            }
        }

        public void Resize()
        {
            RECT RectClient = new RECT();
            RectClient.Left = 0;
            RectClient.Top = 0;
            RectClient.Right = picturebox.Width;
            RectClient.Bottom = picturebox.Height;
            AVerCapAPI.AVerSetVideoWindowPosition(m_hCaptureDevice, RectClient);
        }

        public void Close()
        {
            if (m_iCurrentDeviceIndex != -1)
            {
                SaveSettings();
            }
            if (m_hCaptureDevice != (IntPtr)0)
            {
                DeleteCaptureDevice();
            }
        }

        public void SaveSettings(string settingType="")
        {
            if (string.IsNullOrEmpty(settingType) || settingType == "DeviceInfo")
            {
                StringBuilder szValue = new StringBuilder("", 50);
                AVerCapAPI.AVerGetDeviceName((uint)m_iCurrentDeviceIndex, szValue);
                string szCurrName = szValue.ToString();
                szCurrName = szCurrName.Substring(szCurrName.IndexOf(':') + 1);

                string strDeviceType = m_uCaptureType.ToString();

                WritePrivateProfileString("DeviceName", "DeviceName", szCurrName, m_szFileName);
                WritePrivateProfileString("VideoSetting", "DeviceType", strDeviceType, m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "VideoSource")
            {
                //get device setting
                uint dwVideoSource = 0;
                AVerCapAPI.AVerGetVideoSource(m_hCaptureDevice, ref dwVideoSource);
                string strVideoSource = dwVideoSource.ToString();
                WritePrivateProfileString("VideoSource", "VideoSource", strVideoSource, m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "AudioSource")
            {
                uint dwAudioSource = 0;
                AVerCapAPI.AVerGetAudioSource(m_hCaptureDevice, ref dwAudioSource);
                string strAudioSource = dwAudioSource.ToString();
                WritePrivateProfileString("AudioSource", "AudioSource", strAudioSource, m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "VideoFormat")
            {
                uint dwVideoFormat = 0;
                AVerCapAPI.AVerGetVideoFormat(m_hCaptureDevice, ref dwVideoFormat);
                string strVideoFormat = dwVideoFormat.ToString();
                WritePrivateProfileString("VideoFormat", "VideoFormat", strVideoFormat, m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "VideoFrameRate")
            {
                uint dwFrameRate = 0;
                AVerCapAPI.AVerGetVideoInputFrameRate(m_hCaptureDevice, ref dwFrameRate);
                string strFrameRate = dwFrameRate.ToString();
                WritePrivateProfileString("VideoFrameRate", "VideoFrameRate", strFrameRate, m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "VideoResolution")
            {
                VIDEO_RESOLUTION VideoResolution = GetActualVideoResolution();
                string strResolution = VideoResolution.dwVideoResolution.ToString();
                WritePrivateProfileString("VideoResolution", "Resolution", strResolution, m_szFileName);

                if (VideoResolution.bCustom != 0)
                {
                    WritePrivateProfileString("VideoResolution", "IsCustom", "YES", m_szFileName);
                }
                else
                {
                    WritePrivateProfileString("VideoResolution", "IsCustom", "NO", m_szFileName);
                }
                string strResolutionWidth = VideoResolution.dwWidth.ToString();
                WritePrivateProfileString("VideoResolution", "ResolutionWidth", strResolutionWidth, m_szFileName);
                string strResolutionHeight = VideoResolution.dwHeight.ToString();
                WritePrivateProfileString("VideoResolution", "ResolutionHeight", strResolutionHeight, m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "VideoSetting")
            {
                uint a = WritePrivateProfileString("VideoSetting", "IsRestore", "YES", m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "ColorAdjustment")
            {
                WritePrivateProfileString("ColorAdjustment", "HwBrigthness", HwBrightness.ToString(), m_szFileName);
                WritePrivateProfileString("ColorAdjustment", "HwConstrast", HwConstrast.ToString(), m_szFileName);
                WritePrivateProfileString("ColorAdjustment", "HwHue", HwHue.ToString(), m_szFileName);
                WritePrivateProfileString("ColorAdjustment", "HwSaturation", HwSaturation.ToString(), m_szFileName);

                WritePrivateProfileString("ColorAdjustment", "SwBrigthness", SwBrightness.ToString(), m_szFileName);
                WritePrivateProfileString("ColorAdjustment", "SwConstrast", SwConstrast.ToString(), m_szFileName);
                WritePrivateProfileString("ColorAdjustment", "SwHue", SwHue.ToString(), m_szFileName);
                WritePrivateProfileString("ColorAdjustment", "SwSaturation", SwSaturation.ToString(), m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "CrosshairInfo")
            {
                uint x = WritePrivateProfileString("CrosshairInfo", "PosX", crosshairOverlayPosition.dwXPos.ToString(), m_szFileName);
                uint y = WritePrivateProfileString("CrosshairInfo", "PosY", crosshairOverlayPosition.dwYPos.ToString(), m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "RotationInfo")
            {
                uint x = WritePrivateProfileString("RotationInfo", "Rotation", this.videoRotate.ToString(), m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "DeInterlanceInfo")
            {
                uint x = WritePrivateProfileString("DeInterlanceInfo", "DeInterlance", this.deInterlaceMode.ToString(), m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "VideoEnhanceInfo")
            {
                uint x = WritePrivateProfileString("VideoEnhanceInfo", "VideoEnhance", this.videoEnhance.ToString(), m_szFileName);
            }
            if (string.IsNullOrEmpty(settingType) || settingType == "VideoMirrorInfo")
            {
                uint x = WritePrivateProfileString("VideoMirrorInfo", "VideoMirrorMode", this.videoMirrorMode.ToString(), m_szFileName);
            }
        }

        public void SetVideoCardSettings(string deviceName)
        {
            string savedDeviceName = GetSavedDeviceName();

            if (deviceName.Contains(":"))
                deviceName = deviceName.Substring(deviceName.IndexOf(":") + 1);
            if (savedDeviceName == deviceName)
            {
                StringBuilder szValue = new StringBuilder();
                VIDEO_RESOLUTION VideoResolution = new VIDEO_RESOLUTION();

                #region VideoSource
                GetPrivateProfileString("VideoSource", "VideoSource", "", szValue, 10, m_szFileName);
                uint dwVideoSource = uint.Parse(szValue.ToString());
                AVerCapAPI.AVerSetVideoSource(m_hCaptureDevice, dwVideoSource);
                #endregion
                #region AudioSource
                GetPrivateProfileString("AudioSource", "AudioSource", "", szValue, 10, m_szFileName);
                uint dwAudioSource = uint.Parse(szValue.ToString());
                AVerCapAPI.AVerSetAudioSource(m_hCaptureDevice, dwAudioSource);
                #endregion
                #region VideoFormat
                GetPrivateProfileString("VideoFormat", "VideoFormat", "", szValue, 10, m_szFileName);
                uint dwVideoFormat = uint.Parse(szValue.ToString());
                AVerCapAPI.AVerSetVideoFormat(m_hCaptureDevice, dwVideoFormat);
                #endregion
                #region VideoFrameRate
                GetPrivateProfileString("VideoFrameRate", "VideoFrameRate", "", szValue, 10, m_szFileName);
                this.dwVideoFrameRate = uint.Parse(szValue.ToString());
                AVerCapAPI.AVerSetVideoInputFrameRate(m_hCaptureDevice, dwVideoFrameRate);
                #endregion                
                #region ColorAdjustment Loading
               
                int returnValue = (int)GetPrivateProfileString("ColorAdjustment", "HwBrigthness", "128", szValue, 4, m_szFileName);
                if (!(returnValue > 0 && Int32.TryParse(szValue.ToString(), out HwBrightness)))
                    HwBrightness = 128;

                returnValue = (int)GetPrivateProfileString("ColorAdjustment", "HwConstrast", "128", szValue, 4, m_szFileName);
                if (!(returnValue > 0 && Int32.TryParse(szValue.ToString(), out HwConstrast)))
                    HwConstrast = 128;

                returnValue = (int)GetPrivateProfileString("ColorAdjustment", "HwHue", "128", szValue, 4, m_szFileName);
                if (!(returnValue > 0 && Int32.TryParse(szValue.ToString(), out HwHue)))
                    HwHue = 128;

                returnValue = (int)GetPrivateProfileString("ColorAdjustment", "HwSaturation", "128", szValue, 4, m_szFileName);
                if (!(returnValue > 0 && Int32.TryParse(szValue.ToString(), out HwSaturation)))
                    HwSaturation = 128;

                returnValue = (int)GetPrivateProfileString("ColorAdjustment", "SwBrigthness", "128", szValue, 4, m_szFileName);
                if (!(returnValue > 0 && Int32.TryParse(szValue.ToString(), out SwBrightness)))
                    SwBrightness = 128;

                returnValue = (int)GetPrivateProfileString("ColorAdjustment", "SwConstrast", "128", szValue, 4, m_szFileName);
                if (!(returnValue > 0 && Int32.TryParse(szValue.ToString(), out SwConstrast)))
                    SwConstrast = 128;

                returnValue = (int)GetPrivateProfileString("ColorAdjustment", "SwHue", "128", szValue, 4, m_szFileName);
                if (!(returnValue > 0 && Int32.TryParse(szValue.ToString(), out SwHue)))
                    SwHue = 128;

                returnValue = (int)GetPrivateProfileString("ColorAdjustment", "SwSaturation", "128", szValue, 4, m_szFileName);
                if (!(returnValue > 0 && Int32.TryParse(szValue.ToString(), out SwSaturation)))
                    SwSaturation = 128;
                #endregion
                #region VideoResolution Loading                
                VideoResolution.dwVersion = 1;
                GetPrivateProfileString("VideoResolution", "Resolution", "", szValue, 10, m_szFileName);
                VideoResolution.dwVideoResolution = uint.Parse(szValue.ToString());
                GetPrivateProfileString("VideoResolution", "ResolutionWidth", "", szValue, 10, m_szFileName);
                VideoResolution.dwWidth = uint.Parse(szValue.ToString());
                GetPrivateProfileString("VideoResolution", "ResolutionHeight", "", szValue, 10, m_szFileName);
                VideoResolution.dwHeight = uint.Parse(szValue.ToString());
                GetPrivateProfileString("VideoResolution", "IsCustom", "", szValue, 10, m_szFileName);

                string szTemp = szValue.ToString();
                if (szTemp.Equals("NO"))
                {
                    VideoResolution.bCustom = 0;
                }
                else
                {
                    VideoResolution.bCustom = 1;
                }
                AVerCapAPI.AVerSetVideoResolutionEx(m_hCaptureDevice, ref VideoResolution);
                #endregion
                #region CrosshairInfo
                uint dwPos;
                GetPrivateProfileString("CrosshairInfo", "PosX", "10000", szValue, 10, m_szFileName);
                dwPos = uint.Parse(szValue.ToString());
                if (dwPos == 10000)
                    dwPos = (VideoResolution.dwWidth / 2) - (uint)(sizeCrosshairImage.Width / 2);
                crosshairOverlayPosition.dwXPos = dwPos;
                GetPrivateProfileString("CrosshairInfo", "PosY", "10000", szValue, 10, m_szFileName);
                dwPos = uint.Parse(szValue.ToString());
                if (dwPos == 10000)
                    dwPos = (VideoResolution.dwHeight / 2) - (uint)(sizeCrosshairImage.Height / 2);
                crosshairOverlayPosition.dwYPos = dwPos;
                #endregion  
                #region RotationInfo                                
                try
                {
                    GetPrivateProfileString("RotationInfo", "Rotation", "0", szValue, 30, m_szFileName);
                    videoRotate = (VIDEOROTATE)Enum.Parse(typeof(VIDEOROTATE), szValue.ToString());
                }
                catch(Exception ex) { videoRotate = VIDEOROTATE.VIDEOROTATE_NONE; }
                AVerCapAPI.AVerSetVideoRotateMode(m_hCaptureDevice, (uint)this.videoRotate);
                #endregion  
                #region DeInterlanceInfo                                
                try
                {
                    GetPrivateProfileString("DeInterlanceInfo", "DeInterlance", "0", szValue, 30, m_szFileName);
                    this.deInterlaceMode = (DEINTERLACEMODE)Enum.Parse(typeof(DEINTERLACEMODE), szValue.ToString());
                }
                catch (Exception ex) { this.deInterlaceMode = DEINTERLACEMODE.DEINTERLACE_NONE; }
                AVerCapAPI.AVerSetDeinterlaceMode(m_hCaptureDevice, (uint)this.deInterlaceMode);
                #endregion  
                #region VideoEnhanceInfo                
                try
                {
                    GetPrivateProfileString("VideoEnhanceInfo", "VideoEnhance", "0", szValue, 30, m_szFileName);
                    this.videoEnhance = (VIDEOENHANCE)Enum.Parse(typeof(VIDEOENHANCE), szValue.ToString());
                }
                catch (Exception ex) { this.videoEnhance = VIDEOENHANCE.VIDEOENHANCE_NONE; }
                AVerCapAPI.AVerSetVideoEnhanceMode(m_hCaptureDevice, (uint)this.videoEnhance);
                #endregion  
                #region VideMirrorInfo
                try
                {
                    GetPrivateProfileString("VideoMirrorInfo", "VideoMirrorMode", "0", szValue, 30, m_szFileName);
                    this.videoMirrorMode = (VIDEOMIRROR)Enum.Parse(typeof(VIDEOMIRROR), szValue.ToString());
                }
                catch (Exception ex) { this.videoMirrorMode = VIDEOMIRROR.VIDEOMIRROR_NONE; }
                AVerCapAPI.AVerSetDeinterlaceMode(m_hCaptureDevice, (uint)this.deInterlaceMode);
                #endregion 
            }
            else
            {
                #region VideoSource
                uint dwVideoSource = (uint)VIDEOSOURCE.VIDEOSOURCE_SVIDEO;
                AVerCapAPI.AVerSetVideoSource(m_hCaptureDevice, dwVideoSource);
                #endregion
                #region VideoFormat
                uint dwVideoFormat = (uint)VIDEOFORMAT.VIDEOFORMAT_PAL;
                AVerCapAPI.AVerSetVideoFormat(m_hCaptureDevice, dwVideoFormat);
                #endregion
                SetDefaultColorAdjustment();
                #region VideoResolution Loading  
                VIDEO_RESOLUTION VideoResolution = new VIDEO_RESOLUTION();
                VideoResolution.dwVersion = 1;               
                VideoResolution.dwVideoResolution = 0;
                
                VideoResolution.dwWidth = 640;                
                VideoResolution.dwHeight = 480;
                VideoResolution.bCustom = 0;
                AVerCapAPI.AVerSetVideoResolutionEx(m_hCaptureDevice, ref VideoResolution);
                #endregion
                #region CrosshairInfo
                crosshairOverlayPosition.dwXPos = (VideoResolution.dwWidth / 2) - (uint)(sizeCrosshairImage.Width / 2);
                crosshairOverlayPosition.dwYPos = (VideoResolution.dwHeight / 2) - (uint)(sizeCrosshairImage.Height / 2); 
                #endregion
                #region RotationInfo   
                videoRotate = VIDEOROTATE.VIDEOROTATE_NONE;
                AVerCapAPI.AVerSetVideoRotateMode(m_hCaptureDevice, (uint)this.videoRotate);
                #endregion   
                #region DeInterlanceInfo                
                this.deInterlaceMode = DEINTERLACEMODE.DEINTERLACE_NONE;
                AVerCapAPI.AVerSetDeinterlaceMode(m_hCaptureDevice, (uint)this.deInterlaceMode);
                #endregion  
                #region VideoEnhanceInfo                
                this.videoEnhance = VIDEOENHANCE.VIDEOENHANCE_NONE;
                AVerCapAPI.AVerSetVideoEnhanceMode(m_hCaptureDevice, (uint)this.videoEnhance);
                #endregion
                #region VideMirrorInfo
                this.videoMirrorMode = VIDEOMIRROR.VIDEOMIRROR_NONE;
                AVerCapAPI.AVerSetVideoMirrorMode(m_hCaptureDevice, (uint)this.deInterlaceMode);
                #endregion 
            }
        }

        private void GetActualVideoRect(ref uint uWidth, ref uint uHeight)
        {
            VIDEO_RESOLUTION VideoResolution = GetActualVideoResolution();
            
            uWidth = VideoResolution.dwWidth;
            uHeight = VideoResolution.dwHeight;
        }

        public VIDEO_RESOLUTION GetActualVideoResolution()
        {
            VIDEO_RESOLUTION VideoResolution = new VIDEO_RESOLUTION();
            VideoResolution.dwVersion = 1;
            int hr = AVerCapAPI.AVerGetVideoResolutionEx(m_hCaptureDevice, ref VideoResolution);
            if (hr == (int)ERRORCODE.CAP_EC_SUCCESS)
                VideoResolution = AVerCapAPI.GetVideoResolutionByWidthHeight(VideoResolution.dwWidth, VideoResolution.dwHeight);
            else
                VideoResolution = videoResolution;

            return VideoResolution;
        }

        public uint GetActualFrameRate()
        {
            uint dwFrameRate = 0;
            AVerCapAPI.AVerGetVideoInputFrameRate(m_hCaptureDevice, ref dwFrameRate);

            return dwFrameRate;
        }

        private void SetMaintainAspectRation(int isMaintainEnable)
        {
            m_iIsMaintainEnable = isMaintainEnable;
            AVerCapAPI.AVerSetMaintainAspectRatioEnabled(m_hCaptureDevice, m_iIsMaintainEnable);
        }

        public void SetVideoResolutionInfo()
        {
            videoResolution = GetActualVideoResolution();
            videoResolutionWidth = videoResolution.dwWidth;
            videoResolutionHeight = videoResolution.dwHeight;
            if (this.videoRotate != VIDEOROTATE.VIDEOROTATE_NONE)
            {
                videoResolutionWidth = videoResolution.dwHeight;
                videoResolutionHeight = videoResolution.dwWidth;
            }
        }

        public void SetDefaultColorAdjustment()
        {
            HwBrightness = 128;
            HwConstrast = 128;
            HwHue = 128;
            HwSaturation = 128;

            SwBrightness = 128;
            SwConstrast = 128;
            SwHue = 128;
            SwSaturation = 128;

            if (m_bIsStartStreaming)
            {
                SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS, (uint)HwBrightness);
                SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST, (uint)HwConstrast);
                SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE, (uint)HwHue);
                SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION, (uint)HwSaturation);

                SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS, (uint)SwBrightness);
                SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST, (uint)SwConstrast);
                SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE, (uint)SwHue);
                SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION, (uint)SwSaturation);
            }
        }

        public void SetColorAdjustment(COLOR_ADJUSTMENT_MODE mode, VIDEOPROCAMPPROPERTY porperty, uint propertyValue)
        {
            m_sVideoColorAdjustment.dwMode = (uint)mode;
            m_sVideoColorAdjustment.dwProperty = (uint)porperty;
            m_sVideoColorAdjustment.dwPropertyValue = propertyValue;
            int hr = AVerCapAPI.AVerSetVideoColorAdjustment(m_hCaptureDevice, ref m_sVideoColorAdjustment);
            if (hr == (int)ERRORCODE.CAP_EC_SUCCESS)
            {
                switch(mode)
                {
                    case COLOR_ADJUSTMENT_MODE.MODE_HW:
                        switch(porperty)
                        {
                            case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS:
                                HwBrightness = (int)propertyValue;
                                break;
                            case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST:
                                HwConstrast = (int)propertyValue;
                                break;
                            case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE:
                                HwHue = (int)propertyValue;
                                break;
                            case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION:
                                HwSaturation= (int)propertyValue;
                                break;
                        }
                        break;
                    case COLOR_ADJUSTMENT_MODE.MODE_SW:
                        switch (porperty)
                        {
                            case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS:
                                SwBrightness = (int)propertyValue;
                                break;
                            case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST:
                                SwConstrast = (int)propertyValue;
                                break;
                            case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE:
                                SwHue = (int)propertyValue;
                                break;
                            case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION:
                                SwSaturation = (int)propertyValue;
                                break;
                        }
                        break;
                }
            }
        }

        public int GetColorAdjustment(COLOR_ADJUSTMENT_MODE mode, VIDEOPROCAMPPROPERTY property)
        {
            int value = 0;
            m_sVideoColorAdjustment.dwMode = (uint)mode;
            m_sVideoColorAdjustment.dwProperty = (uint)property;
            int result = AVerCapAPI.AVerGetVideoColorAdjustment(m_hCaptureDevice, ref m_sVideoColorAdjustment);
            if (result == (int)ERRORCODE.CAP_EC_SUCCESS)
            {
                value = (int)m_sVideoColorAdjustment.dwPropertyValue;
                
                if(mode == COLOR_ADJUSTMENT_MODE.MODE_SW && value == 128)
                {
                    switch (property)
                    {
                        case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS:
                            value = SwBrightness;
                            break;
                        case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST:
                            value = SwConstrast;
                            break;
                        case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE:
                            value = SwHue;
                            break;
                        case VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION:
                            value = SwSaturation;
                            break;
                    }
                }
            }
            return value;
        }

        public void InitColorAdjustments()
        {
            HwBrightness = GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS);
            HwConstrast = GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST);
            HwHue = GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE);
            HwSaturation = GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION);

            SwBrightness = GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS);
            SwConstrast = GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST);
            SwHue = GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE);
            SwSaturation = GetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION);

            SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS, (uint)HwBrightness);
            SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST, (uint)HwConstrast);
            SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE, (uint)HwHue);
            SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_HW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION, (uint)HwSaturation);

            SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_BRIGHTNESS, (uint)SwBrightness);
            SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST, (uint)SwConstrast);
            SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_HUE, (uint)SwHue);
            SetColorAdjustment(COLOR_ADJUSTMENT_MODE.MODE_SW, VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_SATURATION, (uint)SwSaturation);
        }

        public void InitOverlayImage()
        {
            int hresult;
            OVERLAY_CONTENT_INFO Test = new OVERLAY_CONTENT_INFO();
            Test.OverlayInfo.bEnableOverlay = 1;
            Test.dwContentType = (uint)OVERLAYSETTINGS.OVERLAY_IMAGE;
            Test.dwVersion = 1;
            Test.dwID = 0;

            OVERLAY_IMAGE_INFO OverlayImageInfo = new OVERLAY_IMAGE_INFO();
            GCHandle handlePtr = GCHandle.Alloc(OverlayImageInfo);

            Test.lpContent = GCHandle.ToIntPtr(handlePtr);

            hresult = AVerCapAPI.AVerOverlayMediaContent(m_hCaptureDevice, ref Test);
            handlePtr.Free();
            if (hresult == (int)ERRORCODE.CAP_EC_NOT_SUPPORTED)
                return;

            string imagePath = "";
            imagePath = string.Format("./OverlayMask/overlayMask_{0}x{1}.png", videoResolutionWidth, videoResolutionHeight);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(imagePath);
            if (!fileInfo.Exists)
            {
                MessageBox.Show("Invalid Path.", imagePath);
                return;
            }

            OVERLAY_CONTENT OverContent = new OVERLAY_CONTENT();
            OverContent.OverContentInfo.dwID = 1;
            OverContent.OverContentInfo.dwVersion = 1;
            OverContent.OverContentInfo.dwContentType = (uint)OVERLAYSETTINGS.OVERLAY_IMAGE;

            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwXPos = 0;
            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwYPos = 0;
            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwAlignment = (uint)ALIGNMENT.ALIGNMENT_CENTER;

            OverContent.OverContentInfo.OverlayInfo.dwFontSize = 0;
            OverContent.OverContentInfo.OverlayInfo.dwFontColor = 0;
            OverContent.OverContentInfo.OverlayInfo.dwTransparency = 0;
            OverContent.OverContentInfo.OverlayInfo.bEnableOverlay = TRUE;

            OverContent.OverContentInfo.dwDuration = 0xFFFFFF;
            OverContent.OverContentInfo.dwPriority = 0;

            imagePath = fileInfo.FullName;

            OverlayImageInfo = new OVERLAY_IMAGE_INFO();
            OverlayImageInfo.lpFileName = imagePath;

            IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(OverlayImageInfo));
            Marshal.StructureToPtr<OVERLAY_IMAGE_INFO>(OverlayImageInfo, intPtr, false);
            OverContent.OverContentInfo.lpContent = intPtr;

            m_OverContentList.Add(OverContent);


            OVERLAY_INFO pOverlayInfo = new OVERLAY_INFO();
            pOverlayInfo.bEnableOverlay = 1;
            pOverlayInfo.dwTransparency = 0;
            AVerCapAPI.AVerSetOverlayProperty(m_hCaptureDevice, (uint)OVERLAYSETTINGS.OVERLAY_IMAGE, pOverlayInfo);

            pOverlayInfo = new OVERLAY_INFO();
            hresult = AVerCapAPI.AVerGetOverlayProperty(m_hCaptureDevice, (uint)OVERLAYSETTINGS.OVERLAY_IMAGE, ref pOverlayInfo);

            hresult = AVerCapAPI.AVerOverlayMediaContent(m_hCaptureDevice, ref OverContent.OverContentInfo);
            if (hresult != (int)ERRORCODE.CAP_EC_SUCCESS)
            {
            }
            OverContent.OverContentInfo.lpContent = IntPtr.Zero;            
        }

        public void SetOverlayTime()
        {
            int hresult;
            VIDEO_RESOLUTION videoResolution = GetActualVideoResolution();

            OVERLAY_CONTENT OverContent = new OVERLAY_CONTENT();
            OverContent.OverContentInfo.dwVersion = 1;
            OverContent.OverContentInfo.dwID = 2;
            OverContent.OverContentInfo.dwContentType = (uint)OVERLAYSETTINGS.OVERLAY_TIME;

            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwXPos = videoResolution.dwWidth;
            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwYPos = 0;
            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwAlignment = (uint)ALIGNMENT.ALIGNMENT_RIGHT;

            OverContent.OverContentInfo.OverlayInfo.dwFontSize = (uint)FONTSIZE.FONTSIZE_SMALL;
            OverContent.OverContentInfo.OverlayInfo.dwFontColor = AVerCapAPI.RGB(255, 255, 255);
            OverContent.OverContentInfo.OverlayInfo.dwTransparency = 100;
            OverContent.OverContentInfo.OverlayInfo.bEnableOverlay = TRUE;

            OverContent.OverContentInfo.dwDuration = 0xFFFFFF;
            OverContent.OverContentInfo.dwPriority = 0;

            uint dateFormat = (uint)TIMEFORMAT.FORMAT_DATEANDTIME;
            IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(dateFormat));
            Marshal.StructureToPtr<uint>(dateFormat, intPtr, false);
            OverContent.OverContentInfo.lpContent = intPtr;

            m_OverContentList.Add(OverContent);


            OVERLAY_INFO pOverlayInfo = new OVERLAY_INFO();
            pOverlayInfo.bEnableOverlay = 1;
            pOverlayInfo.dwTransparency = 0;
            AVerCapAPI.AVerSetOverlayProperty(m_hCaptureDevice, (uint)OVERLAYSETTINGS.OVERLAY_TIME, pOverlayInfo);

            pOverlayInfo = new OVERLAY_INFO();
            hresult = AVerCapAPI.AVerGetOverlayProperty(m_hCaptureDevice, (uint)OVERLAYSETTINGS.OVERLAY_TIME, ref pOverlayInfo);

            hresult = AVerCapAPI.AVerOverlayMediaContent(m_hCaptureDevice, ref OverContent.OverContentInfo);
            if (hresult != (int)ERRORCODE.CAP_EC_SUCCESS)
            {
            }
            OverContent.OverContentInfo.lpContent = IntPtr.Zero;
        }
        
        public void SetOverlayCrosshair()
        {
            int hresult;
            string imagePath = "./OverlayMask/crosshair-01.png";

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(imagePath);
            if (!fileInfo.Exists)
            {
                MessageBox.Show("Invalid Path For Crosshair Image.", imagePath);
                return;
            }

            if (crosshairOverlayPosition.dwXPos == 0 && crosshairOverlayPosition.dwYPos == 0)
            {
                crosshairOverlayPosition.dwXPos = (videoResolutionWidth / 2) - (uint)(sizeCrosshairImage.Width / 2);
                crosshairOverlayPosition.dwYPos = (videoResolutionHeight / 2) - (uint)(sizeCrosshairImage.Height / 2); 
            }

            OVERLAY_CONTENT OverContentCrosshair = new OVERLAY_CONTENT();
            OverContentCrosshair.OverContentInfo.dwID = 4;
            OverContentCrosshair.OverContentInfo.dwVersion = 1;
            OverContentCrosshair.OverContentInfo.dwContentType = (uint)OVERLAYSETTINGS.OVERLAY_IMAGE;

            OverContentCrosshair.OverContentInfo.OverlayInfo.WindowPosition = crosshairOverlayPosition;

            OverContentCrosshair.OverContentInfo.OverlayInfo.dwFontSize = 0;
            OverContentCrosshair.OverContentInfo.OverlayInfo.dwFontColor = 0;
            OverContentCrosshair.OverContentInfo.OverlayInfo.dwTransparency = 0;
            OverContentCrosshair.OverContentInfo.OverlayInfo.bEnableOverlay = TRUE;

            OverContentCrosshair.OverContentInfo.dwDuration = 0xFFFFFF;
            OverContentCrosshair.OverContentInfo.dwPriority = 0;

            imagePath = fileInfo.FullName;

            OVERLAY_IMAGE_INFO OverlayImageInfo = new OVERLAY_IMAGE_INFO();
            OverlayImageInfo.lpFileName = imagePath;

            IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(OverlayImageInfo));
            Marshal.StructureToPtr<OVERLAY_IMAGE_INFO>(OverlayImageInfo, intPtr, false);
            OverContentCrosshair.OverContentInfo.lpContent = intPtr;

            m_OverContentList.Add(OverContentCrosshair);


            OVERLAY_INFO pOverlayInfo = new OVERLAY_INFO();
            pOverlayInfo.bEnableOverlay = 1;
            pOverlayInfo.dwTransparency = 0;
            AVerCapAPI.AVerSetOverlayProperty(m_hCaptureDevice, (uint)OVERLAYSETTINGS.OVERLAY_IMAGE, pOverlayInfo);

            pOverlayInfo = new OVERLAY_INFO();
            hresult = AVerCapAPI.AVerGetOverlayProperty(m_hCaptureDevice, (uint)OVERLAYSETTINGS.OVERLAY_IMAGE, ref pOverlayInfo);

            hresult = AVerCapAPI.AVerOverlayMediaContent(m_hCaptureDevice, ref OverContentCrosshair.OverContentInfo);
            if (hresult != (int)ERRORCODE.CAP_EC_SUCCESS)
            {
            }
            OverContentCrosshair.OverContentInfo.lpContent = IntPtr.Zero;
        }

        public void SetOverlayText(string text)
        {
            int hresult;

            OVERLAY_INFO pOverlayInfo = new OVERLAY_INFO();
            pOverlayInfo.bEnableOverlay = 1;
            pOverlayInfo.dwTransparency = 0;
            AVerCapAPI.AVerSetOverlayProperty(m_hCaptureDevice, (uint)OVERLAYSETTINGS.OVERLAY_TEXT, pOverlayInfo);

            OVERLAY_CONTENT OverContent = new OVERLAY_CONTENT();
            OverContent.OverContentInfo.dwVersion = 1;
            OverContent.OverContentInfo.dwID = 3;
            OverContent.OverContentInfo.dwContentType = (uint)OVERLAYSETTINGS.OVERLAY_TEXT;

            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwXPos = 0;
            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwYPos = 0;
            OverContent.OverContentInfo.OverlayInfo.WindowPosition.dwAlignment = (uint)ALIGNMENT.ALIGNMENT_LEFT;
            OverContent.OverContentInfo.OverlayInfo.bEnableOverlay = TRUE;

            OverContent.OverContentInfo.OverlayInfo.dwFontSize = (uint)FONTSIZE.FONTSIZE_SMALL;
            OverContent.OverContentInfo.OverlayInfo.dwFontColor = AVerCapAPI.RGB(255, 255, 255);
            OverContent.OverContentInfo.OverlayInfo.dwTransparency = 100;
            
            OverContent.OverContentInfo.dwDuration = 0xFFFFFF;
            OverContent.OverContentInfo.dwPriority = 0;

            OverContent.OverContentInfo.lpContent = Marshal.StringToHGlobalUni(text);

            hresult = AVerCapAPI.AVerOverlayMediaContent(m_hCaptureDevice, ref OverContent.OverContentInfo);
            if (hresult != (int)ERRORCODE.CAP_EC_SUCCESS)
            {
            }

            m_OverContentList.Add(OverContent);

            OverContent.OverContentInfo.lpContent = IntPtr.Zero;
        }

        public void Rotate(VIDEOROTATE videoRotate)
        {
            if (m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                stopStreaming();
            }
            this.videoRotate = videoRotate;            
            if (m_DemoState == DEMOSTATE.DEMO_STATE_PREVIEW)
            {
                startStreaming(UserName);
            }
        }

        public void SetDeInterlance(DEINTERLACEMODE deInterlaceMode)
        {
            this.deInterlaceMode = deInterlaceMode;
            AVerCapAPI.AVerSetDeinterlaceMode(m_hCaptureDevice, (uint)deInterlaceMode);
        }

        public void SetVideoEnhancement(VIDEOENHANCE videoEnhance)
        {
            this.videoEnhance = videoEnhance;
            AVerCapAPI.AVerSetVideoEnhanceMode(m_hCaptureDevice, (uint)videoEnhance);
        }

        public void SetVideoMirrorMode(VIDEOMIRROR videoMirrorMode)
        {
            this.videoMirrorMode = videoMirrorMode;
            AVerCapAPI.AVerSetVideoMirrorMode(m_hCaptureDevice, (uint)videoMirrorMode);
        }

        public void ChangeVideoSource(uint videoSource)
        {
            AVerCapAPI.AVerSetVideoSource(CaptureDeviceHandle, videoSource);
        }

        public void ChangeVideoResolution(VIDEO_RESOLUTION videoResolution)
        {
            this.videoResolution = videoResolution;
            AVerCapAPI.AVerSetVideoResolutionEx(m_hCaptureDevice, ref this.videoResolution);

            VIDEO_RESOLUTION VideoResolution = new VIDEO_RESOLUTION();
            VideoResolution.dwVersion = 1;
            AVerCapAPI.AVerGetVideoResolutionEx(m_hCaptureDevice, ref VideoResolution);
        }
        public void ChangeFrameRate(uint dwVideoFrameRate)
        {
            this.dwVideoFrameRate = dwVideoFrameRate;
            AVerCapAPI.AVerSetVideoInputFrameRate(m_hCaptureDevice, dwVideoFrameRate);
        }

        public void ChangeCrosshairPosition(int offsetX, int offsetY)
        {
            int posX = (int)crosshairOverlayPosition.dwXPos + offsetX;
            int posY = (int)crosshairOverlayPosition.dwYPos + offsetY;

            if (posX >= 0 && posX < videoResolutionWidth)
                crosshairOverlayPosition.dwXPos = (uint)posX;
            if (posY >= 0 && posY < videoResolutionHeight)
                crosshairOverlayPosition.dwYPos = (uint)posY;

            SetOverlayCrosshair();
        }

        public void ResetCrosshairPosition()
        {
            crosshairOverlayPosition.dwXPos = (videoResolutionWidth / 2) - (uint)(sizeCrosshairImage.Width / 2);
            crosshairOverlayPosition.dwYPos = (videoResolutionHeight / 2) - (uint)(sizeCrosshairImage.Height / 2);

            SetOverlayCrosshair();
        }

        public void test()
        {
            uint pdwPropertyValue = 0;
            int hresult = AVerCapAPI.AVerGetVideoProcAmp(m_hCaptureDevice, (uint)VIDEOPROCAMPPROPERTY.VIDEOPROCAMPPROPERTY_CONTRAST, ref pdwPropertyValue);
        }
    }
}
