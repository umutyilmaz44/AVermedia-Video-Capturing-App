using AverMediaLib;
using AverMediaTestApp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yuy2;

namespace AverMediaTestApp
{
    public partial class ImageStatsForm : Form
    {
        public ImageStatsForm()
        {
            InitializeComponent();
        }

        private void ImageViewForm_Load(object sender, EventArgs e)
        {
            
        }

        public void PreviewImage(byte[] imgData, VIDEO_SAMPLE_INFO VideoInfo)
        {
            double contrast = Yuy2Decoder.GetContrastYUY2(imgData, (int)VideoInfo.dwWidth, (int)VideoInfo.dwHeight);
            txtContrast.Invoke((Action)(() =>  txtContrast.Text = contrast.ToString() ));

            Bitmap image = Yuy2Decoder.ConvertYUY2ToBitmap(imgData, (int)VideoInfo.dwWidth, (int)VideoInfo.dwHeight);
            Bitmap image2 = image.Clone(new Rectangle(0, 0, image.Width, image.Height), image.PixelFormat);

            // gather statistics
            AForge.Imaging.ImageStatisticsYCbCr stat = new AForge.Imaging.ImageStatisticsYCbCr(image);

            txtCr_Max.Invoke((Action)(() => txtCr_Max.Text = stat.Cr.Max.ToString() ));
            txtCr_Min.Invoke((Action)(() => txtCr_Min.Text = stat.Cr.Min.ToString()));
            txtCr_Mean.Invoke((Action)(() => txtCr_Mean.Text = stat.Cr.Mean.ToString()));
            txtCr_Median.Invoke((Action)(() => txtCr_Median.Text = stat.Cr.Median.ToString()));
            txtCr_StdDev.Invoke((Action)(() => txtCr_StdDev.Text = stat.Cr.StdDev.ToString()));

            txtCr_MaxWB.Invoke((Action)(() => txtCr_MaxWB.Text = stat.CrWithoutBlack.Max.ToString()));
            txtCr_MinWB.Invoke((Action)(() => txtCr_MinWB.Text = stat.CrWithoutBlack.Min.ToString()));
            txtCr_MeanWB.Invoke((Action)(() => txtCr_MeanWB.Text = stat.CrWithoutBlack.Mean.ToString()));
            txtCr_MedianWB.Invoke((Action)(() => txtCr_MedianWB.Text = stat.CrWithoutBlack.Median.ToString()));
            txtCr_StdDevWB.Invoke((Action)(() => txtCr_StdDevWB.Text = stat.CrWithoutBlack.StdDev.ToString()));
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            txtCb_Max.Invoke((Action)(() => txtCb_Max.Text = stat.Cb.Max.ToString()));
            txtCb_Min.Invoke((Action)(() => txtCb_Min.Text = stat.Cb.Min.ToString()));
            txtCb_Mean.Invoke((Action)(() => txtCb_Mean.Text = stat.Cb.Mean.ToString()));
            txtCb_Median.Invoke((Action)(() => txtCb_Median.Text = stat.Cb.Median.ToString()));
            txtCb_StdDev.Invoke((Action)(() => txtCb_StdDev.Text = stat.Cb.StdDev.ToString()));

            txtCb_MaxWB.Invoke((Action)(() => txtCb_MaxWB.Text = stat.CbWithoutBlack.Max.ToString()));
            txtCb_MinWB.Invoke((Action)(() => txtCb_MinWB.Text = stat.CbWithoutBlack.Min.ToString()));
            txtCb_MeanWB.Invoke((Action)(() => txtCb_MeanWB.Text = stat.CbWithoutBlack.Mean.ToString()));
            txtCb_MedianWB.Invoke((Action)(() => txtCb_MedianWB.Text = stat.CbWithoutBlack.Median.ToString()));
            txtCb_StdDevWB.Invoke((Action)(() => txtCb_StdDevWB.Text = stat.CbWithoutBlack.StdDev.ToString()));
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            txtY_Max.Invoke((Action)(() => txtY_Max.Text = stat.Y.Max.ToString()));
            txtY_Min.Invoke((Action)(() => txtY_Min.Text = stat.Y.Min.ToString()));
            txtY_Mean.Invoke((Action)(() => txtY_Mean.Text = stat.Y.Mean.ToString()));
            txtY_Median.Invoke((Action)(() => txtY_Median.Text = stat.Y.Median.ToString()));
            txtY_StdDev.Invoke((Action)(() => txtY_StdDev.Text = stat.Y.StdDev.ToString()));

            txtY_MaxWB.Invoke((Action)(() => txtY_MaxWB.Text = stat.YWithoutBlack.Max.ToString()));
            txtY_MinWB.Invoke((Action)(() => txtY_MinWB.Text = stat.YWithoutBlack.Min.ToString()));
            txtY_MeanWB.Invoke((Action)(() => txtY_MeanWB.Text = stat.YWithoutBlack.Mean.ToString()));
            txtY_MedianWB.Invoke((Action)(() => txtY_MedianWB.Text = stat.YWithoutBlack.Median.ToString()));
            txtY_StdDevWB.Invoke((Action)(() => txtY_StdDevWB.Text = stat.YWithoutBlack.StdDev.ToString()));
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));

            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        public byte[] PadLines(byte[] bytes, int rows, int columns)
        {
            int currentStride = columns; // 3
            int newStride = columns;  // 4
            byte[] newBytes = new byte[newStride * rows];
            for (int i = 0; i < rows; i++)
                Buffer.BlockCopy(bytes, currentStride * i, newBytes, newStride * i, currentStride);
            return newBytes;
        }
    }
}
