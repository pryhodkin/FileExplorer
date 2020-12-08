using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for FileExplorerControl.xaml
    /// </summary>
    public partial class FileExplorerControl : UserControl
    {
        public FileExplorerViewModel viewModel { get; set; } = new FileExplorerViewModel();
        public FileExplorerControl()
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = (TreeView)sender;
            viewModel.SelectedItem = (DirectoryItemViewModel)tree.SelectedItem;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var dataView = grid.DataContext as DirectoryItemViewModel;

            if (dataView.Type != DirectoryItemType.Drive)
            {
                var data = new DirectoryItem { FullPath = dataView.FullPath, Type = dataView.Type };
                var obj = new DataObject(data);

                DragDrop.DoDragDrop(grid, obj, DragDropEffects.Copy);
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            var data = (DirectoryItem)(e.Data.GetData(typeof(DirectoryItem)));
            if(! DirectoryStructure.MoveDirectoryItem(data, viewModel.SelectedItem.FullPath) )
                MessageBox.Show("Неможливо виконати дане переміщення.");
            UpdateViewModelManager.UpdateFileExplorerViewModels();
        }

    }
}
