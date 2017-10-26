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
    /// kaoqin.xaml 的交互逻辑
    /// </summary>
    public partial class kaoqin : Window
    {
        public kaoqin()
        {
            InitializeComponent();
            List<Model.Attandance> list = AttandanceDBLL.AttandanceBLL.getAllAttant();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].Type == "OutOfTime") list.Remove(list[i]);
            }
            this.AttaForm.ItemsSource = list;
            this.TimeChose.Visibility = Visibility.Hidden;
            this.EmpComboBox.Visibility = Visibility.Hidden;
            this.TypeComboBox.Visibility = Visibility.Hidden;
            this.TimeCheckBox.Visibility = Visibility.Hidden;
            this.EmpCheckBox.Visibility = Visibility.Hidden;
            this.TypeCheckBox.Visibility = Visibility.Hidden;
            List<Model.Employee> emplist= AttandanceDBLL.EmployeeBLL.getAllEmp();
            List<string> namelist = new List<string>();
            foreach(Model.Employee e in emplist)
            {
                namelist.Add(e.Name);
            }
            this.EmpComboBox.ItemsSource =namelist;
        }
        private DateTime? BeginTime;
        private DateTime? EndTime;
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            this.TimeChose.Visibility = Visibility.Visible;
            this.EmpComboBox.Visibility = Visibility.Hidden;
            this.TypeComboBox.Visibility = Visibility.Hidden;
            this.TimeCheckBox.Visibility = Visibility.Visible;
            this.TimeCheckBox.IsChecked = true;
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            this.EmpComboBox.Visibility = Visibility.Visible;
            this.TimeChose.Visibility = Visibility.Hidden;
            this.TypeComboBox.Visibility = Visibility.Hidden;
            this.EmpCheckBox.Visibility = Visibility.Visible;
            this.EmpCheckBox.IsChecked = true;
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            this.TypeComboBox.Visibility = Visibility.Visible;
            this.EmpComboBox.Visibility = Visibility.Hidden;
            this.TimeChose.Visibility = Visibility.Hidden;
            this.TypeCheckBox.Visibility = Visibility.Visible;
            this.TypeCheckBox.IsChecked = true;
        }

        private void BeginTimeBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.BeginTime = this.BeginTimeBox.SelectedDate;
        }

        private void EndTimeBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.EndTime = this.EndTimeBox.SelectedDate;
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            List<Model.Attandance> tempList;

            if (this.TimeCheckBox.IsChecked == true)
            {
                tempList = AttandanceDBLL.AttandanceBLL.getAttantBytiem(this.BeginTime, this.EndTime);
                this.TimeCheckBox.Content = this.BeginTime + "--" + this.EndTime;
            }
            else tempList = AttandanceDBLL.AttandanceBLL.getAllAttant();
            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                if (tempList[i].Type == "OutOfTime") tempList.Remove(tempList[i]);
            }
            if (this.EmpCheckBox.IsChecked == true)
            {
                this.EmpCheckBox.Content = this.EmpComboBox.SelectedValue;
                for(int i = tempList.Count - 1; i >= 0; i--)
                {
                    if (tempList[i].Name != this.EmpComboBox.SelectedValue.ToString()) tempList.Remove(tempList[i]);
                }
            }

            if (this.TypeCheckBox.IsChecked == true)
            {
                string type;
                if (this.TypeComboBox.SelectedIndex == 0) type = "Sign_In";
                else if (this.TypeComboBox.SelectedIndex == 1) type = "Sign_Out";
                else type = "OverTimeWork";
                this.TypeCheckBox.Content = type;
                for (int i = tempList.Count - 1; i >= 0; i--)
                {
                    if (tempList[i].Type != type) tempList.Remove(tempList[i]);
                }
            }
            this.AttaForm.ItemsSource = tempList;
        }

        private void TimeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TimeCheckBox.Visibility = Visibility.Hidden;
            TimeChose.Visibility = Visibility.Hidden;
        }


        private void TypeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            TypeCheckBox.Visibility = Visibility.Hidden;
            TypeComboBox.Visibility = Visibility.Hidden;
        }

        private void EmpCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            EmpCheckBox.Visibility = Visibility.Hidden;
            EmpComboBox.Visibility = Visibility.Hidden;
        }
    }
}
