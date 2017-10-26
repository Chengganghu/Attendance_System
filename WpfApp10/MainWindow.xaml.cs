using System;
using System.Windows;
using MathWorks.MATLAB.NET.Arrays;
using System.Drawing;
using AForge.Video.DirectShow;
using System.IO;
using Microsoft.Win32;
namespace WpfApp10
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Fail_Image.Visibility = Visibility.Hidden;
            this.Success_Image.Visibility = Visibility.Hidden;
            
            //
        }

        /******************************************************/
        //parametres needed when using the camera
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private int selectedDeviceIndex = 0;
        private string g_Path = System.Environment.CurrentDirectory;
        /******************************************************/
        private MWLogicalArray isAntiFake = false;
        private bool isInited = false;
        private bool hasCamera = false;
        /******************************************************/


        //Connect the camera, choose the first one by default
        public VideoCaptureDevice VideoConnect(int deviceIndex = 0, int resolutionIndex = 0)
        {
            if (videoDevices.Count <= 0)
                return null;
            selectedDeviceIndex = deviceIndex;
            videoSource = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString);
            return videoSource;
        }

        //void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        //{
        //    //Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
        //    Bitmap bmp = sourcePlayer.GetCurrentVideoFrame();
        //    string fullPath = g_Path + "\\temp\\";
        //    if (!Directory.Exists(fullPath))
        //        Directory.CreateDirectory(fullPath);
        //    //string img = fullPath + DateTime.Now.Ticks.ToString() + ".bmp";
        //    this.tempPath = fullPath + DateTime.Now.Ticks.ToString() + ".bmp";
        //    bmp.Save(this.tempPath);
        //    videoSource.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);
        //}
        ////
        //public void GrabBitmap(string path)
        //{
        //    if (videoSource == null)
        //        return;
        //    g_Path = path;
        //    videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);///////kanbudong
        //}
        //void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        //{
        //    //Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
        //    Bitmap bmp = sourcePlayer.GetCurrentVideoFrame();
        //    string fullPath = g_Path + "\\temp\\";
        //    if (!Directory.Exists(fullPath))
        //        Directory.CreateDirectory(fullPath);
        //    //string img = fullPath + DateTime.Now.Ticks.ToString() + ".bmp";
        //    this.tempPath = fullPath + DateTime.Now.Ticks.ToString() + ".bmp";
        //    bmp.Save(this.tempPath);
        //    videoSource.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);
        //}
        //
        public string  GrabBitmap(string path)
        {
            Bitmap bmp = sourcePlayer.GetCurrentVideoFrame();
            string fullPath = g_Path + "\\temp\\";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            string img = fullPath + DateTime.Now.Ticks.ToString() + ".bmp";
            bmp.Save(img);
            return img;
        }

        //Get all the availbale cameras
        public FilterInfoCollection GetDevices(out bool flag)
        {
            try
            {
                //枚举所有视频输入设备  
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count != 0)
                {
                    //Console.WriteLine("已找到视频设备.");
                    flag = true;
                    return videoDevices;
                }
                else
                {
                    flag = false;
                    return null;
                }
                    
            }
            catch (Exception ex)
            {
                //Console.WriteLine("error:没有找到视频设备！具体原因：" + ex.Message);
                flag = false;
                return null;
            }
        }

        private void init_Click(object sender, RoutedEventArgs e)
        {
            if (this.isInited == false)
            {
                GetDevices(out hasCamera);
                if(this.hasCamera == true)
                {
                    VideoConnect();
                    sourcePlayer.VideoSource = VideoConnect();
                    sourcePlayer.Start();
                    this.isInited = true;
                    System.Threading.Thread it = new System.Threading.Thread(init);
                    it.Start();
                    this.InfoBox.Text = "初始化成功";
                    this.isInited = true;
                }
                else
                {
                    this.InfoBox.Text = "没有检测到设备，请检查连接";
                }

            }
            else
            {
                this.InfoBox.Text = "已初始化";
            }
        }
        private void init()
        {
            MWArray jpg = ImageOperator.ImageOperator.ReadImg(g_Path + @"\init.bmp");
            register.Cregister rc = new register.Cregister();
            rc.register(jpg, isAntiFake);
            verify.Cverify vc = new verify.Cverify();
            vc.verify(jpg);
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (this.isInited == true)
            {

                string temppath = GrabBitmap(g_Path);
                register.Cregister rt = new register.Cregister();
                MWArray tmp = this.isAntiFake;
                MWArray result = ImageOperator.ImageOperator.ReadImg(temppath);
                MWArray mw2 = rt.register(result, isAntiFake);
                if (mw2.ToString().Equals("-1") || mw2.ToString().Equals("0") || mw2.ToString().Equals("-2"))
                {
                    MessageBox.Show("注册失败");

                    this.InfoBox.Text = "注册失败，请重试";
                    this.Fail_Image.Visibility = Visibility.Hidden;
                    this.Success_Image.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show(mw2.ToString() + "注册成功");

                    this.InfoBox.Text = "注册成功，您的ID是: " + mw2.ToString();
                    this.Fail_Image.Visibility = Visibility.Hidden;
                    this.Success_Image.Visibility = Visibility.Hidden;

                }
            }
            else
            {
                this.InfoBox.Text = "请初始化后再使用";
            }

        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            if (this.isInited == true)
            {
                string temppath = GrabBitmap(g_Path);
                verify.Cverify cv = new verify.Cverify();
                MWArray bmp = ImageOperator.ImageOperator.ReadImg(temppath);
                MWArray result = cv.verify(bmp);
                if (result.ToString().Equals("0"))
                {
                    this.InfoBox.Text = "验证失败，请重试或重新注册";
                    this.Fail_Image.Visibility = Visibility.Visible;
                    this.Success_Image.Visibility = Visibility.Hidden;
                    MessageBox.Show("验证失败，请重试或重新注册");
                }
                else
                {
                    this.InfoBox.Text = result.ToString() + "号验证成功！";
                    this.Fail_Image.Visibility = Visibility.Hidden;
                    this.Success_Image.Visibility = Visibility.Visible;
                    MessageBox.Show(result.ToString() + "号验证成功！");
                }
            }
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
