using System;
using System.Collections.Generic;
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
using System.Windows.Forms;

namespace WpfApp10
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Commit_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Collections.ArrayList list = AttandanceDBLL.LoginBLL.getLogin(this.UsernameBox.Text.ToString());
            if(list.Count != 0)
            {
                bool flag = false;
                foreach (Model.Login l in list)
                {
                    if (l.Password == this.Password.Password.ToString())
                    {
                        this.Close();
                        Administration a = new Administration();
                        a.ShowDialog();
                        flag = true;
                        break;
                    }
                }
                if (flag==false)
                {
                    System.Windows.MessageBox.Show("Password incorrect");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("User doesnot exist");
            }

            //this.Close();
            //Administration a = new Administration();
            //a.ShowDialog();
        }

    }
}
