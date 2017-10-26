using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VedioOperator
{
    public class Class1
    {
        public VideoCaptureDevice VideoConnect(int deviceIndex = 0, int resolutionIndex = 0)
        {
            if (videoDevices.Count <= 0)
                return null;
            selectedDeviceIndex = deviceIndex;
            videoSource = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString);
            return videoSource;
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            Bitmap bmp = sourcePlayer.GetCurrentVideoFrame();
            string fullPath = g_Path + "temp\\";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            string img = fullPath + DateTime.Now.Ticks.ToString() + ".bmp";
            this.tempPath = img;
            bmp.Save(img);
            videoSource.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);
        }
        public void GrabBitmap(string path)
        {
            if (videoSource == null)
                return;
            g_Path = path;
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);///////kanbudong
        }
        public FilterInfoCollection GetDevices()
        {
            try
            {
                //枚举所有视频输入设备  
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count != 0)
                {
                    Console.WriteLine("已找到视频设备.");
                    return videoDevices;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error:没有找到视频设备！具体原因：" + ex.Message);
                return null;
            }
        }

    }
}
