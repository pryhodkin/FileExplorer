using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for DirectoryTreeControl.xaml
    /// </summary>
    public partial class DirectoryTreeControl : UserControl
    {
        DirectoryStructureViewModel ViewModel { get; set; }
        public DirectoryTreeControl()
        {
            ViewModel = new DirectoryStructureViewModel();
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = (TreeView)sender;
            ViewModel.SelectedItem = (DirectoryItemViewModel)tree.SelectedItem;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var dataView = grid.DataContext as DirectoryItemViewModel;
            var data = new DirectoryItem { FullPath = dataView.FullPath, Type = dataView.Type };
            var obj = new DataObject(data); 
            DragDrop.DoDragDrop(grid, obj, DragDropEffects.Copy);
        }
    }
}
