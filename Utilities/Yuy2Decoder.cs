using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Yuy2
{
    public static class Yuy2Decoder
    {
        public static byte[] DecompressYUY2(byte[] input, int width, int height)
        {
            byte[] output = new byte[width * height * sizeof(uint)];
            DecompressYUY2(input, width, height, output);
            return output;
        }

        public static void DecompressYUY2(byte[] input, int width, int height, byte[] output)
        {
            int i,j;
            int p = 0;
            int o = 0;
            int y0, u0, y1, v0, c, d, e;
            int halfWidth = width / 2;
            for (j = 0; j < height; j++)
            {
                for (i = 0; i < halfWidth- 1; ++i)
                {
                    y0 = input[p++];
                    u0 = input[p++];
                    y1 = input[p++];
                    v0 = input[p++];
                    c = y0 - 16;
                    d = u0 - 128;
                    e = v0 - 128;
                    output[o++] = ClampByte((298 * c + 516 * d + 128) >> 8);            // blue
                    output[o++] = ClampByte((298 * c - 100 * d - 208 * e + 128) >> 8);  // green
                    output[o++] = ClampByte((298 * c + 409 * e + 128) >> 8);            // red
                    output[o++] = 255;
                    c = y1 - 16;
                    output[o++] = ClampByte((298 * c + 516 * d + 128) >> 8);            // blue
                    output[o++] = ClampByte((298 * c - 100 * d - 208 * e + 128) >> 8);  // green
                    output[o++] = ClampByte((298 * c + 409 * e + 128) >> 8);            // red
                    output[o++] = 255;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte ClampByte(int x)
        {
            return (byte)(byte.MaxValue < x ? byte.MaxValue : (x > byte.MinValue ? x : byte.MinValue));
        }

        public static Bitmap ConvertYUY2ToBitmap(byte[] inputYUY2, int width, int height)
        {
            Bitmap bmp = null;
            using (DirectBitmap bitmap = new DirectBitmap(width, height))
            {
                DecompressYUY2(inputYUY2, width, height, bitmap.Bits);                
                //SaveToPng(bitmap.Bitmap, @"D:\Temp\record-image.png");
                bmp = bitmap.Bitmap.Clone(new Rectangle(0, 0, bitmap.Bitmap.Width, bitmap.Bitmap.Height), bitmap.Bitmap.PixelFormat);
            }

            return bmp;
        }

        
        public static double GetContrastYUY2(byte[] input, int width, int height)
        {
            int i, j;
            int p = 0;
            int o = 0;
            int y0, u0, y1, v0, c, d, e;
            int halfWidth = width / 2;
            double contrast = 0;
            byte red1, green1, blue1, red2, green2, blue2;
            for (j = 0; j < height; j++)
            {
                for (i = 0; i < halfWidth - 1; ++i)
                {
                    y0 = input[p++];
                    u0 = input[p++];
                    y1 = input[p++];
                    v0 = input[p++];
                    c = y0 - 16;
                    d = u0 - 128;
                    e = v0 - 128;
                    red1 = ClampByte((298 * c + 516 * d + 128) >> 8);            // blue
                    green1 = ClampByte((298 * c - 100 * d - 208 * e + 128) >> 8);  // green
                    blue1 = ClampByte((298 * c + 409 * e + 128) >> 8);            // red
                    
                    c = y1 - 16;
                    red2 = ClampByte((298 * c + 516 * d + 128) >> 8);            // blue
                    green2 = ClampByte((298 * c - 100 * d - 208 * e + 128) >> 8);  // green
                    blue2 = ClampByte((298 * c + 409 * e + 128) >> 8);            // red

                    contrast += GetContrast(red1, green1, blue1, red2, green2, blue2);
                }
            }

            contrast = contrast / (width * height);

            return contrast;
        }


        private static double GetLuminanace(byte r, byte g, byte b)
        {
            double r0 = (r + (r / 255)) <= 0.03928 ? (r + (r / 255)) / 12.92 : Math.Pow(((r + (r / 255)) + 0.055) / 1.055, 2.4);
            double g0 = (g + (g / 255)) <= 0.03928 ? (g + (g / 255)) / 12.92 : Math.Pow(((g + (g / 255)) + 0.055) / 1.055, 2.4);
            double b0 = (b + (b / 255)) <= 0.03928 ? (b + (b / 255)) / 12.92 : Math.Pow(((r + (r / 255)) + 0.055) / 1.055, 2.4);

            return r0 * 0.2126 + g0 * 0.7152 + b0 * 0.0722;
        }
        private static double GetContrast(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2)
        {
            var lum1 = GetLuminanace(r1, g1, b1);
            var lum2 = GetLuminanace(r2, g2, b2);
            var brightest = Math.Max(lum1, lum2);
            var darkest = Math.Min(lum1, lum2);
            return (brightest + 0.05) / (darkest + 0.05);
        }

        public static void SaveToPng(Bitmap bitmap, string filePath)
        {            
            FileInfo fileInfo = new FileInfo(filePath);
            if(!fileInfo.Exists)
                bitmap.Save(fileInfo.FullName, ImageFormat.Png);
            else
            {
                int i = 0;
                bool fileExist = true;
                string filePathTmp;
                string fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                string directoryName = fileInfo.Directory.FullName;
                FileInfo fileInfoTmp;
                do
                {
                    i++;
                    fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                    fileName += i + ".png";
                    filePathTmp = Path.Combine(directoryName, fileName);
                    fileInfoTmp = new FileInfo(filePathTmp);
                    fileExist = fileInfoTmp.Exists;
                    if (!fileExist)
                        bitmap.Save(filePathTmp, ImageFormat.Png);
                } while (fileExist);
            }
        }
    }

    public sealed class DirectBitmap : IDisposable
    {
        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new byte[width * height * 4];
            m_bitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(Width, Height, Stride, PixelFormat.Format32bppArgb, m_bitsHandle.AddrOfPinnedObject());
        }

        ~DirectBitmap()
        {
            Dispose(false);
        }

        public void SetPixel(int x, int y, Color color)
        {
            int index = x + (y * Width);
            unchecked
            {
                uint value = (uint)color.ToArgb();
                Bits[index + 0] = (byte)(value >> 0);
                Bits[index + 1] = (byte)(value >> 8);
                Bits[index + 2] = (byte)(value >> 16);
                Bits[index + 3] = (byte)(value >> 24);
            }
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            uint col = BitConverter.ToUInt32(Bits, index);
            return Color.FromArgb(unchecked((int)col));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                Bitmap.Dispose();
                m_bitsHandle.Free();
                m_disposed = true;
            }
        }

        public int Height { get; }
        public int Width { get; }
        public int Stride => Width * 4;
        public Bitmap Bitmap { get; }
        public byte[] Bits { get; }
        public IntPtr BitsPtr => m_bitsHandle.AddrOfPinnedObject();

        private readonly GCHandle m_bitsHandle;
        private bool m_disposed;
    }
}