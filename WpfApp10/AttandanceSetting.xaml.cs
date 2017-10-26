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
    /// AttandanceSetting.xaml 的交互逻辑
    /// </summary>
    public partial class AttandanceSetting : Window
    {
        public AttandanceSetting()
        {
            InitializeComponent();
        }


        private void Commit_Button_Click(object sender, RoutedEventArgs e)
        {
            string morOnDuty = this.MorOnDutyBox.SelectionBoxItem.ToString();
            string morOffDuty = this.MorOffDutyBox.SelectionBoxItem.ToString();
            string aftOnDuty = this.AftOnDutyBox.SelectionBoxItem.ToString();
            string aftOffDuty = this.AftOffDutyBox.SelectionBoxItem.ToString();
            Model.AttandanceSetting temp = new Model.AttandanceSetting(morOnDuty, morOffDuty, aftOnDuty, aftOffDuty);
            AttandanceDBLL.AttandanceSettingBLL.Add(temp);
        }
    }
}
