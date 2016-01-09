using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using tempTrello.Model;

namespace tempTrello
{
    /// <summary>
    /// Логика взаимодействия для AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        BindingList<ListModel> listModelCheckBox;
        public AddTaskWindow()
        {
            InitializeComponent();
            listModelCheckBox = new BindingList<ListModel>();
            MainWindow mainWindows = Application.Current.MainWindow as MainWindow;
            //listModelCheckBox = mainWindows.getListModel() as BindingList<ListModel>;
            this.DataContext = mainWindows.DataContext;
        }

        private void ClosedOk_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindows = Application.Current.MainWindow as MainWindow;
            if (mainWindows.getListId() == "")
            {
                MessageBox.Show("Не указаны куда нужно добавлять задачи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (TasksId.Text != "")
                {
                    mainWindows.parmTaskId(TasksId.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("Не указаны номера заявок для добавления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            
        }

        

        private void ListNameSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListModel listModel = e.AddedItems[0] as ListModel;
            MainWindow mainWindows = Application.Current.MainWindow as MainWindow;
            mainWindows.parmListId(listModel.Id);
        }
    }
}
