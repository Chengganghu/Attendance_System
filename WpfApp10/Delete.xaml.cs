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
using MathWorks.MATLAB.NET.Arrays;
namespace WpfApp10
{
    /// <summary>
    /// Delete.xaml 的交互逻辑
    /// </summary>
    public partial class Delete : Window
    {
        public Delete()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mr = MessageBox.Show("Confirm your choice.\nAre you sure to delete this user?", "Delete", MessageBoxButton.YesNo);
            clear_mat.Cclearmat ccl = new clear_mat.Cclearmat();
            if (mr == MessageBoxResult.Yes)
            {
                Model.Employee temp = AttandanceDBLL.EmployeeBLL.getEmp(this.Id.Text.ToString());
                if (temp == null)
                {
                    MessageBox.Show("User doesnot exist");
                }
                else
                {
                    if (temp.Usertype == "Administrator")
                    {
                        AttandanceDBLL.EmployeeBLL.Delete(this.Id.Text.ToString());
                        AttandanceDBLL.LoginBLL.Delete(this.Id.Text.ToString());
                        MWNumericArray mwn = temp.FingerId;
                        ccl.clear_mat(mwn);
                        MessageBox.Show("User " + temp.Name + " has been deleted");
                        
                    }
                    else
                    {
                        AttandanceDBLL.EmployeeBLL.Delete(this.Id.Text.ToString());
                        MWNumericArray mwn = temp.FingerId;
                        ccl.clear_mat(mwn);
                        MessageBox.Show("User " + temp.Name + " has been deleted");
                    }
                }
            }
        }
    }
}
