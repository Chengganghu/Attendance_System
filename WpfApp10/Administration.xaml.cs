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

namespace WpfApp10
{
    /// <summary>
    /// Administration.xaml 的交互逻辑
    /// </summary>
    public partial class Administration : Window
    {
        public Administration()
        {
            InitializeComponent();

            
        }



        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            RegisInfo ri = new RegisInfo();
            ri.ShowDialog();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            Delete d = new Delete();
            d.ShowDialog();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Edit ed = new Edit();
            ed.ShowDialog();
        }

        private void Info_Button_Click(object sender, RoutedEventArgs e)
        {
            kaoqin kaoqin = new kaoqin();
            kaoqin.ShowDialog();
        }
    }
}
