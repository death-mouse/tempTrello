using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;
using System.Xml;
using tempTrello.Model;

namespace tempTrello
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void lbl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*Label lbl = (Label)sender;
            DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);*/
        }
        int updateCount = 0;
        double heigth = 0;
        double width = 0;

        int updateCount2 = 0;
        double heigth2 = 0;
        double width2 = 0;

        private void txtTarget_Drop(object sender, DragEventArgs e)
        {
            ((TextBlock)sender).Text = (string)e.Data.GetData(DataFormats.Text);
        }

        private void Tested_Drop(object sender, DragEventArgs e)
        {
            /*ListBox listBoxItem = (ListBox)sender;
            StackPanel stackPanel = (StackPanel)e.Data.GetData("myFormat");
            RemoveChildHelper.RemoveChild(stackPanel.Parent, stackPanel);
            listBoxItem.Items.Add(stackPanel);*/

        }

        private void Tested_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            /*StackPanel control = sender as StackPanel;
            ListBoxItem listBoxItem2 = new ListBoxItem();
            if (control.Parent as ListBoxItem)
             listBoxItem2 = (ListBoxItem)control.Parent;
            else

            ListBox listBoxParent = (ListBox)listBoxItem2.Parent;
            listBoxParent.Items.Remove(listBoxItem2);
            DependencyObject depObj = control.Parent;
            DataObject dragData = new DataObject("myFormat", control);
            DragDrop.DoDragDrop(control, dragData, DragDropEffects.Link);*/
        }

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            /*ListBox listBoxItem = (ListBox)sender;
            StackPanel stackPanel = (StackPanel)e.Data.GetData("myFormat");
            RemoveChildHelper.RemoveChild(stackPanel.Parent, stackPanel);
            listBoxItem.Items.Add(stackPanel);*/
        }

        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {

        }

        private void ListItem_Drop(object sender, DragEventArgs e)
        {

        }

        private void StacPanelList_Drop(object sender, DragEventArgs e)
        {
            StackPanel control = sender as StackPanel;
            //StackPanel parentControl = control.Parent as StackPanel;
            StackPanel stackPanel = (StackPanel)e.Data.GetData("myFormat");
            tempTrello.Model.CardModel cardModelNew = (tempTrello.Model.CardModel)control.DataContext;
            tempTrello.Model.CardModel cardModelOld = (tempTrello.Model.CardModel)stackPanel.DataContext;
            if (cardModelNew == cardModelOld)
                return;
            tempTrello.View.TrelloViewModel trelloViewModel = (tempTrello.View.TrelloViewModel)Board.DataContext;
            BindingList<tempTrello.Model.BoardModel> boardModel = trelloViewModel.boardModel;

            BoardModel boardNew = findBoardModel(boardModel, cardModelNew.IdBoard);
            BindingList<tempTrello.Model.ListModel> listModel = boardNew.ListModel;
            ListModel list = findListModel(listModel, cardModelNew.IdList);

            string listId = cardModelOld.IdList;
            string boardId = cardModelOld.IdBoard;

            BoardModel boardold = findBoardModel(boardModel, boardId);
            ListModel listModelOld = findListModel(boardold.ListModel, listId);
            deleteOld(listModelOld.CardModel, listId, boardId, cardModelOld.Id);
            cardModelOld.IdList = cardModelNew.IdList;
            if(list.CardModel.Count == 0 || list.CardModel.Count == 1)
            {
                if (list.CardModel.Count == 1 && list.CardModel[0].Name == "Пусто")
                    list.CardModel.RemoveAt(0);
            }
            list.CardModel.Add(cardModelOld);

            control.UpdateLayout();
            stackPanel = new StackPanel();
            
            TextBlock foundListBox;



            foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", 0));
            updateCount2 = 0;
            heigth2 = 0;
            width2 = 0;
            heigth = 0;
            while (foundListBox != null)
            {
                stackPanel = foundListBox.Parent as StackPanel;

                if (heigth2 < stackPanel.ActualHeight)
                    heigth2 = stackPanel.ActualHeight;
                /*if (width2 < stackPanel.ActualWidth)
                    width2 = stackPanel.ActualWidth;*/

                updateCount2++;
                foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", updateCount2));
            }
            for (int i = 0; i != updateCount2; i++)
            {
                foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", i));
                stackPanel = foundListBox.Parent as StackPanel;
                updateCount = 0;
                heigth = 0;
                width = 0;
                TextBlock foundTextBox =
                FindChild<TextBlock>(foundListBox, "CardDesc0");
                if (foundTextBox == null)
                {
                    foundTextBox = FindChild<TextBlock>(foundListBox, "CardDesc");
                }
                while (foundTextBox != null)
                {
                    if (heigth <= stackPanel.ActualHeight)
                        heigth = stackPanel.ActualHeight;
                    updateCount++;
                    foundTextBox = FindChild<TextBlock>(Application.Current.MainWindow, string.Format("CardDesc{0}", updateCount));
                }
                if (heigth == 0)
                {
                    ListBox listBox = stackPanel.Children[1] as ListBox;
                    if (heigth < listBox.ActualHeight)
                    {
                        heigth = listBox.ActualHeight;
                        heigth2 = listBox.ActualHeight;
                    }
                }
                for (int y = 0; y != updateCount; y++)
                {
                    foundTextBox =
                        FindChild<TextBlock>(Application.Current.MainWindow, string.Format("CardDesc{0}", y));
                    if (foundTextBox != null)
                    {
                        stackPanel = foundTextBox.Parent as StackPanel;
                        if (heigth > stackPanel.ActualHeight)
                            stackPanel.Height = heigth;

                        stackPanel.RenderSize = new Size(Width, heigth);

                        stackPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                        stackPanel.UpdateLayout();
                    }
                }
                if (heigth2 > stackPanel.Height)
                {
                    stackPanel.Height = heigth2;
                }
                /*if (width2 > stackPanel.Width)
                    stackPanel.Width = width2;*/

                stackPanel.RenderSize = new Size(Width, heigth);

                stackPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                stackPanel.UpdateLayout();


            }
            /*TextBlock foundTextBox =
                FindChild<TextBlock>(Application.Current.MainWindow, "CardDesc");
            foundTextBox.Name = foundTextBox.Name + Convert.ToString(updateCount);
            stackPanel = foundTextBox.Parent as StackPanel;
            stackPanel = stackPanel.Parent as StackPanel;
            if (heigth <= stackPanel.ActualHeight)
                heigth = stackPanel.ActualHeight;
            if (width <= stackPanel.ActualWidth)
                width = stackPanel.ActualWidth;
            for (int i = 0; i != updateCount; i++)
            {
                foundTextBox =
                FindChild<TextBlock>(Application.Current.MainWindow, string.Format("CardDesc{0}", i));
                if (foundTextBox != null)
                {
                    stackPanel = foundTextBox.Parent as StackPanel;
                    if (heigth > stackPanel.ActualHeight)
                    {
                        stackPanel.Height = heigth;
                    }
                    if (width > stackPanel.ActualWidth)
                        stackPanel.Width = width;

                    stackPanel.RenderSize = new Size(Width, heigth);

                    stackPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                    stackPanel.UpdateLayout();
                }
                string test = "";
            }*/

            normalView();


        }
        private void normalView()
        {
            TextBlock foundListBox;
            foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", 0));
            updateCount2 = 0;
            StackPanel stackPanel = new StackPanel();
            while (foundListBox != null)
            {
                stackPanel = foundListBox.Parent as StackPanel;

                if (heigth2 < stackPanel.ActualHeight)
                    heigth2 = stackPanel.ActualHeight;
                /*if (width2 < stackPanel.ActualWidth)
                    width2 = stackPanel.ActualWidth;*/

                updateCount2++;
                foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", updateCount2));
            }
            for (int i = 0; i != updateCount2; i++)
            {
                foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", i));
                stackPanel = foundListBox.Parent as StackPanel;
                //if (heigth2 > stackPanel.Height)
                {
                    stackPanel.Height = heigth2;
                }
                /*if (width2 > stackPanel.Width)
                    stackPanel.Width = width2;*/

                stackPanel.RenderSize = new Size(Width, heigth2);

                stackPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                stackPanel.UpdateLayout();
            }
                string str = "";
        }
        private BoardModel findBoardModel(BindingList<BoardModel> boardModel, string _idBoard)
        {
            ObservableCollection<BoardModel> filtererd = new ObservableCollection<tempTrello.Model.BoardModel>(boardModel.Where(t => t.Id == _idBoard)); ;
            return filtererd[0];
        }
        private CardModel findCardModel(BindingList<CardModel> cardModel, string _idCard)
        {
            ObservableCollection<CardModel> filtererd = new ObservableCollection<CardModel>(cardModel.Where(t => t.Id == _idCard)); ;
            return filtererd[0];
        }
        private ListModel findListModel(BindingList<ListModel> listModel, string _idList)
        {
            ObservableCollection<ListModel> filtererd = new ObservableCollection<ListModel>(listModel.Where(t => t.Id == _idList)); ;
            return filtererd[0];
        }
        private void deleteOld(BindingList<CardModel> cardModel, string _idList, string _idBoard, string _idCard)
        {
            ObservableCollection<CardModel> filtererd = new ObservableCollection<CardModel>(cardModel.Where(t => t.Id == _idCard)); ;
            //filtererd.RemoveAt(0);
            cardModel.Remove(filtererd[0]);
            if(cardModel.Count() == 0)
            {
                cardModel.Add(new CardModel() { IdBoard = _idBoard, Name = "Пусто", IdList = _idList });
            }
        }
        private void StacPanelList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel control = sender as StackPanel;
            
            DataObject dragData = new DataObject("myFormat", control);
            DragDrop.DoDragDrop(control, dragData, DragDropEffects.Link);
        }

       

        private void BoardNameSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            tempTrello.View.TrelloViewModel trelloViewModel = (tempTrello.View.TrelloViewModel)Board.DataContext;
            
            if( (string)BoardNameSelected.SelectedValue != "")
                trelloViewModel.getBoardListAndCard((string)BoardNameSelected.SelectedValue);

            updateCount = 0;
            heigth = 0;
            width = 0;

            updateCount2 = 0;
            heigth2 = 0;
            width2 = 0;
        }

        private void Board_Selected(object sender, RoutedEventArgs e)
        {
        }
        private void ListItems_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            /*ListBox foundTextBox =
                FindChild<ListBox>(Application.Current.MainWindow, "List");
             */
            ListBox send = sender as ListBox;
            
            
            string str = "";
            string tt = "";
        }
        private void MontantTotal_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            TextBlock text = sender as TextBlock;
            StackPanel stackPanel = text.Parent as StackPanel;

            
            TextBlock foundListBox =
               FindChild<TextBlock>(Application.Current.MainWindow, "ListName");
            stackPanel = foundListBox.Parent as StackPanel;
            if (heigth2 <= stackPanel.ActualHeight)
                heigth2 = stackPanel.ActualHeight;
            if (width2 <= stackPanel.ActualWidth)
                width2 = stackPanel.ActualWidth;
            foundListBox.Name = foundListBox.Name + Convert.ToString(updateCount2);
            
            for (int i = 0; i != updateCount + 1; i++)
            {
                foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", i));
                stackPanel = foundListBox.Parent as StackPanel;
                if (heigth2 > stackPanel.ActualHeight)
                {
                    stackPanel.Height = heigth2;
                }
                if (width2 > stackPanel.ActualWidth)
                    stackPanel.Width = width2;

                stackPanel.RenderSize = new Size(Width, heigth);

                stackPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                stackPanel.UpdateLayout();
            }
            updateCount2++;
            TextBlock foundTextBox =
                FindChild<TextBlock>(Application.Current.MainWindow, "CardDesc");
            foundTextBox.Name = foundTextBox.Name + Convert.ToString(updateCount);
            stackPanel = foundTextBox.Parent as StackPanel;
            stackPanel = stackPanel.Parent as StackPanel;
            if (heigth <= stackPanel.ActualHeight)
                heigth = stackPanel.ActualHeight;
            if (width <= stackPanel.ActualWidth)
                width = stackPanel.ActualWidth;
            for (int i = 0; i != updateCount+1; i++ )
            {
                foundTextBox =
                FindChild<TextBlock>(Application.Current.MainWindow, string.Format("CardDesc{0}", i));
                
                stackPanel = foundTextBox.Parent as StackPanel;
                if (heigth > stackPanel.ActualHeight)
                {
                    stackPanel.Height = heigth;
                }
                if(width  > stackPanel.ActualWidth)
                    stackPanel.Width = width;
                
                stackPanel.RenderSize = new Size(Width, heigth);
                
                stackPanel.Measure(new Size(double.PositiveInfinity,double.PositiveInfinity));
                stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                stackPanel.UpdateLayout();
                string test = "";
            }
            updateCount++;
            string str = "";
        }
        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        private void Board_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            listBox.SelectedItem = null;
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = this.Content as Grid;


            int count = 0;
            /*UIElement[] elements = new UIElement[grid.Children.Count];
            MainStack.Children.CopyTo(grid.Children, 0);*/
            UIElementCollection elements = grid.Children;
            ScrollViewer sc = elements[0] as ScrollViewer;
            StackPanel stackPanel = sc.Content as StackPanel;
            elements = stackPanel.Children;
            stackPanel = elements[1] as StackPanel;
            elements = stackPanel.Children;
            ListBox listBox = elements[0] as ListBox;
            FrameworkTemplate fr = listBox.ItemTemplate;
            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;
            listBox = elements[2] as ListBox;
            fr = listBox.ItemTemplate;

            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;

            listBox = elements[1] as ListBox;
            fr = listBox.ItemTemplate;
            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;
            stackPanel = elements[0] as StackPanel;
            elements = stackPanel.Children;
            string str = "";
        }

        private void CardDesc_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void CardDesc_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = this.Content as Grid;


            int count = 0;
            /*UIElement[] elements = new UIElement[grid.Children.Count];
            MainStack.Children.CopyTo(grid.Children, 0);*/
            UIElementCollection elements = grid.Children;
            ScrollViewer sc = elements[0] as ScrollViewer;
            StackPanel stackPanel = sc.Content as StackPanel;
            elements = stackPanel.Children;
            stackPanel = elements[1] as StackPanel;
            elements = stackPanel.Children;
            ListBox listBox = elements[0] as ListBox;
            FrameworkTemplate fr = listBox.ItemTemplate;
            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;
            listBox = elements[2] as ListBox;
            fr = listBox.ItemTemplate;

            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;

            listBox = elements[1] as ListBox;
            fr = listBox.ItemTemplate;
            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;
            stackPanel = elements[0] as StackPanel;
            elements = stackPanel.Children;
            TextBlock textBox = elements[0] as TextBlock;
            double heigth = stackPanel.ActualHeight;
            double width = stackPanel.ActualWidth;
            string str = "";
        }

        private void CardDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            Grid grid = this.Content as Grid;


            int count = 0;
            /*UIElement[] elements = new UIElement[grid.Children.Count];
            MainStack.Children.CopyTo(grid.Children, 0);*/
            UIElementCollection elements = grid.Children;
            ScrollViewer sc = elements[0] as ScrollViewer;
            StackPanel stackPanel = sc.Content as StackPanel;
            elements = stackPanel.Children;
            stackPanel = elements[1] as StackPanel;
            elements = stackPanel.Children;
            ListBox listBox = elements[0] as ListBox;
            FrameworkTemplate fr = listBox.ItemTemplate;
            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;
            listBox = elements[2] as ListBox;
            fr = listBox.ItemTemplate;

            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;

            listBox = elements[1] as ListBox;
            fr = listBox.ItemTemplate;
            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;
            stackPanel = elements[0] as StackPanel;
            elements = stackPanel.Children;
            TextBox textBox = sender as TextBox;
            stackPanel = textBox.Parent as StackPanel;
            double heigth =  stackPanel.ActualHeight;
            double width = stackPanel.ActualWidth;
            string str = "";
        }

        private void StacPanelList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = this.Content as Grid;


            int count = 0;
            /*UIElement[] elements = new UIElement[grid.Children.Count];
            MainStack.Children.CopyTo(grid.Children, 0);*/
            UIElementCollection elements = grid.Children;
            ScrollViewer sc = elements[0] as ScrollViewer;
            StackPanel stackPanel = sc.Content as StackPanel;
            elements = stackPanel.Children;
            stackPanel = elements[1] as StackPanel;
            elements = stackPanel.Children;
            ListBox listBox = elements[0] as ListBox;
            FrameworkTemplate fr = listBox.ItemTemplate;
            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;
            listBox = elements[2] as ListBox;
            fr = listBox.ItemTemplate;

            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;

            listBox = elements[1] as ListBox;
            fr = listBox.ItemTemplate;
            stackPanel = fr.LoadContent() as StackPanel;
            elements = stackPanel.Children;
            stackPanel = elements[0] as StackPanel;
            elements = stackPanel.Children;
            TextBox textBox = sender as TextBox;
            stackPanel = textBox.Parent as StackPanel;
            double heigth = stackPanel.ActualHeight;
            double width = stackPanel.ActualWidth;
            string str = "";
        }

        private void StacPanelList_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void StacPanelList_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            /*StackPanel stackPanel = sender as StackPanel;

            double heigth = stackPanel.ActualHeight;
            double width = stackPanel.ActualWidth;

            StackPanel foundTextBox =
                FindChild<StackPanel>(Application.Current.MainWindow, "StacPanelList");

            string str = "";*/
            TextBlock foundListBox =
               FindChild<TextBlock>(Application.Current.MainWindow, "CardDesc");
            string str = "";
        }

        private void ListItems_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            TextBlock foundListBox;
            foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", 0));
            updateCount2 = 0;
            heigth2 = 0;
            width2 = 0;
            while (foundListBox != null)
            {
                StackPanel stackPanel = foundListBox.Parent as StackPanel;
                if (heigth2 < stackPanel.ActualHeight)
                    heigth2 = stackPanel.ActualHeight;
                /*if (width2 < stackPanel.ActualWidth)
                    width2 = stackPanel.ActualWidth;*/
                updateCount2++;
                foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", updateCount2));
            }
            for(int i = 0; i != updateCount2; i++)
            {
                foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", i));
                StackPanel stackPanel = foundListBox.Parent as StackPanel;
                if (heigth2 > stackPanel.Height)
                {
                    stackPanel.Height = heigth2;
                }
                /*if (width2 > stackPanel.Width)
                    stackPanel.Width = width2;*/

                stackPanel.RenderSize = new Size(Width, heigth);

                stackPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                stackPanel.UpdateLayout();
            }
            /*StackPanel test2 = sender as StackPanel;
            StackPanel stackPanel = new StackPanel();
            TextBlock foundListBox;

            for (int i = 0; i != updateCount2; i++)
            {
                foundListBox =
                    FindChild<TextBlock>(Application.Current.MainWindow, string.Format("ListName{0}", i));
                stackPanel = foundListBox.Parent as StackPanel;
                if (heigth2 > stackPanel.ActualHeight)
                {
                    stackPanel.Height = heigth2 + heigth2;
                }
                if (width2 > stackPanel.ActualWidth)
                    stackPanel.Width = width2;

                stackPanel.RenderSize = new Size(Width, heigth);

                stackPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                stackPanel.UpdateLayout();
            }*/
            /*TextBlock foundTextBox =
                FindChild<TextBlock>(Application.Current.MainWindow, "CardDesc");
            foundTextBox.Name = foundTextBox.Name + Convert.ToString(updateCount);
            stackPanel = foundTextBox.Parent as StackPanel;
            stackPanel = stackPanel.Parent as StackPanel;
            if (heigth <= stackPanel.ActualHeight)
                heigth = stackPanel.ActualHeight;
            if (width <= stackPanel.ActualWidth)
                width = stackPanel.ActualWidth;
            for (int i = 0; i != updateCount; i++)
            {
                foundTextBox =
                FindChild<TextBlock>(Application.Current.MainWindow, string.Format("CardDesc{0}", i));

                stackPanel = foundTextBox.Parent as StackPanel;
                if (heigth > stackPanel.ActualHeight)
                {
                    stackPanel.Height = heigth;
                }
                if (width > stackPanel.ActualWidth)
                    stackPanel.Width = width;

                stackPanel.RenderSize = new Size(Width, heigth);

                stackPanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                stackPanel.Arrange(new Rect(0, 0, stackPanel.DesiredSize.Width, stackPanel.DesiredSize.Height));
                stackPanel.UpdateLayout();
                string test = "";
            }*/
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    public static class RemoveChildHelper
    {
        public static void RemoveChild(this DependencyObject parent, UIElement child)
        {
            var panel = parent as Panel;
            if (panel != null)
            {
                panel.Children.Remove(child);
                return;
            }

            var decorator = parent as Decorator;
            if (decorator != null)
            {
                if (decorator.Child == child)
                {
                    decorator.Child = null;
                }
                return;
            }

            var contentPresenter = parent as ContentPresenter;
            if (contentPresenter != null)
            {
                if (contentPresenter.Content == child)
                {
                    contentPresenter.Content = null;
                }
                return;
            }

            var contentControl = parent as ContentControl;
            if (contentControl != null)
            {
                if (contentControl.Content == child)
                {
                    contentControl.Content = null;
                }
                return;
            }

            // maybe more
        }
    }
}
