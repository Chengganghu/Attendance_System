using AForge.Video.DirectShow;
using MathWorks.MATLAB.NET.Arrays;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ImageOperator;
using AForge.Vision;
using AForge.Vision.Motion;
using AForge.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace WpfApp10
{
    /// <summary>
    /// Attandance.xaml 的交互逻辑
    /// </summary>
    public partial class Attandance : Window
    {
        public Attandance()
        {
            InitializeComponent();
            this.getSetting(out this.moron, out this.moroff, out this.afton, out this.aftoff);
            this.Name.Visibility = Visibility.Hidden;
            this.Time.Visibility = Visibility.Hidden;
            this.Dep.Visibility = Visibility.Hidden;
            this.Smile.Visibility = Visibility.Hidden;
            this.Cry.Visibility = Visibility.Hidden;
            this.Finger.Visibility = Visibility.Hidden;
            this.Type.Visibility = Visibility.Hidden;
            this.Closed += Attandance_Closed;
            GetDevices(out hasCamera);
            if (this.hasCamera == true)
            {
                VideoConnect();
                sourcePlayer.VideoSource = VideoConnect();
                sourcePlayer.Start();
                this.isInited = true;
                //MWArray jpg = ImageOperator.ImageOperator.ReadImg(g_Path + @"\init.bmp");
                //register.Class1 rc = new register.Class1();
                //rc.register(jpg, isAntiFake);
                //verify_threads.Cverify vc = new verify_threads.Cverify();
                //vc.verify_threads(jpg);
                //this.InfoBox.Text = "初始化成功";
                //this.isInited = true;
            }
            else
            {
                System.Windows.MessageBox.Show("没有检测到设备，请检查连接");
                // this.InfoBox.Text = "没有检测到设备，请检查连接";
            }


        }

        private void Attandance_Closed(object sender, EventArgs e)
        {
            sourcePlayer.Stop();
        }

        /******************************************************/
        //parametres needed when using the camera
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private int selectedDeviceIndex = 0;
        private string g_Path = System.Environment.CurrentDirectory;
        /******************************************************/
        private MathWorks.MATLAB.NET.Arrays.MWLogicalArray isAntiFake = false;
        private bool isInited = false;
        private bool hasCamera = false;
        public delegate void dele();
        private string[] moron;
        private string[] moroff;
        private string[] afton;
        private string[] aftoff;
        private System.Windows.Forms.Timer Ts = new System.Windows.Forms.Timer();
        /******************************************************/


        //Capture a photo
        public string GrabBitmap(string path)
        {
            Bitmap bmp = sourcePlayer.GetCurrentVideoFrame();
            string fullPath = g_Path + "\\temp\\";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            string img = fullPath + DateTime.Now.Ticks.ToString() + ".bmp";
            try
            {
                bmp.Save(img);
            }
            catch (Exception ex) { }
            return img;
        }

        //Connect the camera, choose the first one by default
        public VideoCaptureDevice VideoConnect(int deviceIndex = 0, int resolutionIndex = 0)
        {
            if (videoDevices.Count <= 0)
                return null;
            selectedDeviceIndex = deviceIndex;
            videoSource = new VideoCaptureDevice(videoDevices[deviceIndex].MonikerString);
            return videoSource;
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AttandanceSetting AS = new AttandanceSetting();
            AS.ShowDialog();
        }

        private bool AttaType(DateTime t,out string type,out string MorOrAft)
        {
            if (t.Hour >= 8 && t.Hour < 10)
            {
                //shangwuonduty
                if (t.Hour < Int32.Parse(moron[0]))
                {
                    type = "Sign_In";
                    MorOrAft = "AM";
                    return false;
                }
                else if (t.Minute < Int32.Parse(moron[1]) + 10)
                {
                    type = "Sign_In";
                    MorOrAft = "AM";
                    return false;
                }
                else
                {
                    type = "Sign_In";
                    MorOrAft = "AM";
                    return true;
                }
            }
            else if (t.Hour >= 13 && t.Hour < 15)
            {
                //xiawuonduty
                if (t.Hour < Int32.Parse(afton[0]))
                {
                    type = "Sign_In";
                    MorOrAft = "PM";
                    return false;
                }
                else if (t.Minute < Int32.Parse(afton[1]) + 10)
                {
                    type = "Sign_In";
                    MorOrAft = "PM";
                    return false;
                }

                else
                {
                    type = "Sign_In";
                    MorOrAft = "PM";
                    return true;
                }
            }
            else if (t.Hour >= 11 && t.Hour < 12)
            {
                //shangwuoffduty
                if (t.Hour < Int32.Parse(moroff[0]))
                {
                    type = "Sign_Out";
                    MorOrAft = "AM";
                    return true;
                }
                else if (t.Minute < Int32.Parse(moroff[1]) - 10)
                {
                    type = "Sign_Out";
                    MorOrAft = "AM";
                    return true;
                }

                else
                {
                    type = "Sign_Out";
                    MorOrAft = "AM";
                    return false;
                }
            }

            else if (t.Hour >= 17 && t.Hour < 19)
            {
                //xiawuoffduty
                if (t.Hour < Int32.Parse(aftoff[0]))
                {
                    type = "Sign_Out";
                    MorOrAft = "PM";
                    return true;
                }
                else if (t.Minute < Int32.Parse(aftoff[1]) - 10)
                {
                    type = "Sign_Out";
                    MorOrAft = "PM";
                    return true;
                }
                else
                {
                    type = "Sign_Out";
                    MorOrAft = "PM";
                    return false;
                }
            }
            else if (t.Hour >= 19)
            {
                type = "OverTimeWork";
                MorOrAft = "PM";
                return false;
            }
            else
            {
                type = "OutOfTime";
                MorOrAft = null;
                return true;
            }
        }



        public void func()
        {
            MotionDetector detector = new MotionDetector(
                new SimpleBackgroundModelingDetector(),
                new MotionAreaHighlighting());

            while (sourcePlayer.GetCurrentVideoFrame() != null)
            {
                Bitmap temp = sourcePlayer.GetCurrentVideoFrame();

                if ((double)detector.ProcessFrame(temp) > 0.2)
                {
                    float temoo = detector.ProcessFrame(temp);
                    Thread.Sleep(2000);
                    string temppath = GrabBitmap(g_Path);
                    verify.Cverify cv = new verify.Cverify();
                    MWArray result;
                    try
                    {
                        MWArray bmp = ImageOperator.ImageOperator.ReadImg(temppath);
                        result = cv.verify(bmp);
                    }
                    catch (Exception ex)
                    {
                        result = 0;
                    }
                    if (result.ToString().Equals("0"))
                    {
                        //this.InfoBox.Text = "验证失败，请重试或重新注册";
                        //this.Fail_Image.Visibility = Visibility.Visible;
                        //this.Success_Image.Visibility = Visibility.Hidden;
                        this.Ts.Tick += Timer_Tick;
                        this.Ts.Interval = 2000;
                        this.Ts.Enabled = true;
                        this.Dispatcher.BeginInvoke((Action)delegate ()
                        {

                            this.Photo.Visibility = Visibility.Hidden;
                            this.Smile.Visibility = Visibility.Hidden;
                            this.Cry.Visibility = Visibility.Visible;
                            this.Name.Visibility = Visibility.Hidden;
                            this.Time.Visibility = Visibility.Hidden;
                            this.Dep.Visibility = Visibility.Hidden;
                            this.Type.Visibility = Visibility.Hidden;
                            this.Name_Box.Visibility = Visibility.Hidden;
                            this.Dep_Box.Visibility = Visibility.Hidden;
                            this.Time_Box.Visibility = Visibility.Hidden;
                            this.Type_Box.Visibility = Visibility.Hidden;
                        });
                        System.Windows.MessageBox.Show("验证失败，请重试或重新注册");
                        //this.Smile.Visibility = Visibility.Hidden;
                        //this.Cry.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DateTime t = new DateTime();
                        t = DateTime.Now;
                        string type;
                        string mororaft;
                        TimeSpan timeSpan;
                        bool res = AttaType(t, out type, out mororaft);
                        int tmp;
                        if (res) tmp = 1;
                        else tmp = 0;
                        if (type.Equals("Sign_In"))
                        {
                            if (res == false) timeSpan = TimeSpan.Parse("00:00:00");
                            else
                            {
                                if (mororaft.Equals("AM")) timeSpan = t.TimeOfDay - DateTime.Parse(moron[0] + ":" + moron[1]).TimeOfDay;
                                else timeSpan = t.TimeOfDay - DateTime.Parse(afton[0] + ":" + afton[1]).TimeOfDay;
                            }
                        }
                        else if (type.Equals("Sign_Out"))
                        {
                            //timeSpan = TimeSpan.Parse("00:00:00");
                            if (res == false) timeSpan = TimeSpan.Parse("00:00:00");
                            else
                            {
                                if (mororaft.Equals("AM")) timeSpan = DateTime.Parse(moroff[0] + ":" + moroff[1]).TimeOfDay - t.TimeOfDay;
                                else timeSpan = DateTime.Parse(aftoff[0] + ":" + aftoff[1]).TimeOfDay - t.TimeOfDay;
                            }
                        }
                        else if (type.Equals("OverTimeWork"))
                        {
                            timeSpan = t.TimeOfDay - DateTime.Parse("19:00").TimeOfDay;
                        }
                        else timeSpan = TimeSpan.Parse("00:00:00");
                        Model.Employee tempemp = AttandanceDBLL.EmployeeBLL.getEmpByFinger(result.ToString());
                        Model.Attandance newAttant = new Model.Attandance(t, t.ToString(), Int32.Parse(result.ToString()), type, tmp, tempemp.Name, timeSpan);
                        AttandanceDBLL.AttandanceBLL.Add(newAttant);
                        this.Dispatcher.BeginInvoke((Action)delegate ()
                        {
                            this.Photo.Visibility = Visibility.Visible;
                            //Bitmap bt = new Bitmap("Photo/" + tempemp.Id + ".jpg");
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.UriSource = new Uri(@"Photo\" + tempemp.Id + ".jpg", UriKind.RelativeOrAbsolute);
                            bi.EndInit();
                            this.Photo.Source = bi;
                            this.Smile.Visibility = Visibility.Hidden;
                            this.Cry.Visibility = Visibility.Hidden;
                            this.Name.Visibility = Visibility.Visible;
                            this.Time.Visibility = Visibility.Visible;
                            this.Dep.Visibility = Visibility.Visible;
                            this.Type.Visibility = Visibility.Visible;
                            this.Name_Box.Visibility = Visibility.Visible;
                            this.Dep_Box.Visibility = Visibility.Visible;
                            this.Time_Box.Visibility = Visibility.Visible;
                            this.Type_Box.Visibility = Visibility.Visible;
                            this.Name_Box.Text = tempemp.Name;
                            this.Dep_Box.Text = tempemp.DepatementId;
                            this.Time_Box.Text = newAttant.AttantTime.ToString();
                            this.Type_Box.Text = type;
                        });
                        this.Ts.Tick += Timer_Tick;
                        this.Ts.Interval = 2000;
                        this.Ts.Enabled = true;
                        System.Windows.MessageBox.Show(tempemp.DepatementId + " " + tempemp.Name + "打卡成功！\n" + DateTime.Now.ToString());


                    }
                    //MessageBox.Show("i saw u!!!");
                    Thread.Sleep(2000);
                }

            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer)sender).Enabled = false;
            SendKeys.SendWait("{Enter}");
        }

        private void getSetting(out string[] moron, out string[] moroff, out string[] afton, out string[] aftoff)
        {
            Model.AttandanceSetting setting = AttandanceDBLL.AttandanceSettingBLL.getSetting();
            moron = setting.MorOnDuty.Split('：');
            afton = setting.AftOnDuty.Split('：');
            moroff = setting.MorOffDuty.Split('：');
            aftoff = setting.AftOffDuty.Split('：');
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.sourcePlayer.Stop();
            Login l = new Login();
            l.ShowDialog();
            VideoConnect();
            sourcePlayer.VideoSource = VideoConnect();
            sourcePlayer.Start();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            dele d = func;
            Thread t = new Thread(func);
            t.IsBackground = true;
            t.Start();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            if (this.Finger.Visibility == Visibility.Visible)
            {
                this.WFH.Visibility = Visibility.Visible;
                this.Finger.Visibility = Visibility.Hidden;
            }
            else
            {
                this.Finger.Visibility = Visibility.Visible;
                this.WFH.Visibility = Visibility.Hidden;
            }

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("mspaint.exe");
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }
    }
}