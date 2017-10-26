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

namespace WpfApp10
{
    /// <summary>
    /// RegisInfo.xaml 的交互逻辑
    /// </summary>
    public partial class RegisInfo : Window
    {
        public RegisInfo()
        {
            InitializeComponent();
            this.Password.Visibility = Visibility.Hidden;
            this.Username.Visibility = Visibility.Hidden;
            this.PasswordText.Visibility = Visibility.Hidden;
            this.UsernameText.Visibility = Visibility.Hidden;
            this.Closed += RegisInfo_Closed;
            this.Combo.SelectedIndex = 0;
            this.Message.Text = "输入基本信息，将手指放入设备后，点击提交按钮";
            if (this.isInited == false)
            {
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
                   // this.InfoBox.Text = "没有检测到设备，请检查连接";
                }

            }
            else
            {
                //this.InfoBox.Text = "已初始化";
            }

        }

        private void RegisInfo_Closed(object sender, EventArgs e)
        {
            this.sourcePlayer.Stop();
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
        private bool isAdmin = false;
        private int fois = 0;
        private byte[,,,] resultHighReliability = new byte[3, 3, 480, 640];
        private int tempId;
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

        public string GrabBitmap(string path)
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
        private void Commit_Click(object sender, RoutedEventArgs e)
        {
            Model.Employee newEmp;
            Model.Login newLogin;
            if (this.isAdmin == false)
            {
                try
                {
                    string[] date = new string[3];
                    date = this.Birthday.Text.ToString().Split('-');
                    newEmp = new Model.Employee(this.Name.Text.ToString(),
                    Int32.Parse(this.Age.Text.ToString()), this.IdNumber.Text.ToString(),
                    new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2])), tempId, this.DepatementID.Text.ToString(),"Normal User");
                    AttandanceDBLL.EmployeeBLL.Add(newEmp);
                    MessageBox.Show("提交成功");
                }
                catch (System.FormatException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //sourcePlayer.Stop();
                //this.Close();
            }
            else
            {
                try
                {
                    string[] date = new string[3];
                    date = this.Birthday.Text.ToString().Split('-');
                    newEmp = new Model.Employee(this.Name.Text.ToString(),
                    Int32.Parse(this.Age.Text.ToString()), this.IdNumber.Text.ToString(),
                    new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2])), tempId, this.DepatementID.Text.ToString(),"Administrator");
                    AttandanceDBLL.EmployeeBLL.Add(newEmp);
                    newLogin = new Model.Login(this.Username.Text.ToString(), this.Password.Password.ToString(), this.IdNumber.Text.ToString());
                    AttandanceDBLL.LoginBLL.Add(newLogin);
                    MessageBox.Show("提交成功");
                }
                catch (System.FormatException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //sourcePlayer.Stop();
                //this.Close();
            }

        }

        private void ReadImgFor3(string str)
        {

            byte[,] R, G, B;
            Bitmap bmp = new Bitmap(str);
            ImageOperator.ImageOperator.GetRGB(bmp, out R, out G, out B);

            for (int i = 0; i < 480; i++)
            {
                for (int j = 0; j < 640; j++)
                {
                    this.resultHighReliability[fois, 0, i, j] = R[i, j];
                }
            }
            for (int i = 0; i < 480; i++)
            {
                for (int j = 0; j < 640; j++)
                {
                    this.resultHighReliability[fois, 0, i, j] = G[i, j];
                }
            }
            for (int i = 0; i < 480; i++)
            {
                for (int j = 0; j < 640; j++)
                {
                    this.resultHighReliability[fois, 0, i, j] = B[i, j];
                }
            }
            fois++;

        }  

        private void Birthday_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.Format.Content = "Fmt: yyyy-mm-dd";
        }

        private void Birthday_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.Format.Content = "";
        }

        private void Normal_User_Selected(object sender, RoutedEventArgs e)
        {
            this.Password.Visibility = Visibility.Hidden;
            this.Username.Visibility = Visibility.Hidden;
            this.PasswordText.Visibility = Visibility.Hidden;
            this.UsernameText.Visibility = Visibility.Hidden;
            this.isAdmin = false;
        }
        private void Administrator_Selected(object sender,RoutedEventArgs e)
        {
            this.Password.Visibility = Visibility.Visible;
            this.Username.Visibility = Visibility.Visible;
            this.PasswordText.Visibility = Visibility.Visible;
            this.UsernameText.Visibility = Visibility.Visible;
            this.isAdmin = true;
        }

        private void Collect_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.isInited == true)
            {

                string temppath = GrabBitmap(g_Path);
                register.Cregister rt = new register.Cregister();
                MWArray tmp = this.isAntiFake;
                MessageBox.Show("第" + (this.fois + 1) + "次采集完成，还需采集" + (2 - this.fois) + "次");
                ReadImgFor3(temppath);
                if (fois == 3)
                {
                    register.Cregister cr = new register.Cregister();
                    MWArray temp = this.isAntiFake;
                    MWNumericArray tmparray = this.resultHighReliability;
                    MWArray tempinput = tmparray;
                    MWArray mw2 = cr.register(tempinput,temp);
                    fois = 0;
                    string tmp01 = mw2.ToString();
                    this.tempId = Int32.Parse(tmp01);
                    if (mw2.ToString().Equals("-1") || mw2.ToString().Equals("0") || mw2.ToString().Equals("-2"))
                    {
                        MessageBox.Show("注册失败，请重试");
                    }
                    else
                    {
                        MessageBox.Show("注册成功，您的ID是: " + mw2.ToString()); 
                    }
                }
            }
        }
    }
}
