using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<DirectoryItemViewModel> list { get; set; } = new ObservableCollection<DirectoryItemViewModel>
        {
            new DirectoryItemViewModel("C:\\", DirectoryItemType.Drive),
        };
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Rectangle_Drop(object sender, DragEventArgs e)
        {
            var data = (DirectoryItem)(e.Data.GetData(typeof(DirectoryItem)));
            var dataView = new DirectoryItemViewModel(data.FullPath, data.Type);
            list.Add(dataView);
        }
    }
}
