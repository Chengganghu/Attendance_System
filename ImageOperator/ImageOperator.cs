using System;
using MathWorks.MATLAB.NET.Arrays;
using System.Drawing;

namespace ImageOperator
{
    public class ImageOperator
    {
        public static bool GetRGB(System.Drawing.Bitmap source, out byte[,] R, out byte[,] G, out byte[,] B)
        {
            try
            {
                int iWidth = source.Width;
                int iHeight = source.Height;
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, iWidth, iHeight);
                System.Drawing.Imaging.BitmapData bmpData = source.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, source.PixelFormat);
                IntPtr iPtr = bmpData.Scan0;
                int iBytes = iWidth * iHeight * 3;
                byte[] PixelValues = new byte[iBytes];
                System.Runtime.InteropServices.Marshal.Copy(iPtr, PixelValues, 0, iBytes);
                source.UnlockBits(bmpData);
                R = new byte[iHeight, iWidth];
                G = new byte[iHeight, iWidth];
                B = new byte[iHeight, iWidth];
                int iPoint = 0;
                for (int i = 0; i < iHeight; i++)
                {
                    for (int j = 0; j < iWidth; j++)
                    {
                        B[i, j] = Convert.ToByte(PixelValues[iPoint++]);
                        G[i, j] = Convert.ToByte(PixelValues[iPoint++]);
                        R[i, j] = Convert.ToByte(PixelValues[iPoint++]);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                R = null; G = null; B = null;
                return false;
            }
        }

        public static MWArray ReadImg(string str)
        {

            byte[,] R, G, B;
            Bitmap bmp = new Bitmap(str);
            GetRGB(bmp, out R, out G, out B);

            byte[,,] array = new byte[3, 480, 640];
            for (int i = 0; i < 480; i++)
            {
                for (int j = 0; j < 640; j++)
                {
                    array[0, i, j] = R[i, j];
                }
            }
            for (int i = 0; i < 480; i++)
            {
                for (int j = 0; j < 640; j++)
                {
                    array[1, i, j] = G[i, j];
                }
            }
            for (int i = 0; i < 480; i++)
            {
                for (int j = 0; j < 640; j++)
                {
                    array[2, i, j] = B[i, j];
                }
            }
            MWNumericArray mw = array;
            MWArray re = mw;
            return re;

        }
    }

}
