using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FileExplorer
{
    public class FileExplorerViewModel : BaseViewModel
    {

        #region Public properties

        public DirectoryStructureViewModel Tree { get; set; } = new DirectoryStructureViewModel();

        public string[] DirsToFile { get => SelectedItem?.FullPath.Split('\\'); set { } }

        public void Update()
        {
            var selectedPath = SelectedItem?.FullPath ?? null;
            Tree.Update();

            var data = DirectoryStructure.GetDirectoryItem(selectedPath);

            SelectedItem = new DirectoryItemViewModel(data?.FullPath, data?.Type ?? DirectoryItemType.File);
            SelectedItem.IsExpanded = true;
        }


        private DirectoryItemViewModel selectedItem;

        public FileExplorerViewModel()
        {
            UpdateViewModelManager.AddFileExplorerViewModel(this);
        }

        /// <summary>
        /// Current directory.
        /// </summary>
        public DirectoryItemViewModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = new DirectoryItemViewModel(value.FullPath, value.Type);
                if (SelectedItem.CanExpand)
                    SelectedItem.IsExpanded = true;
                selectedItem.Children = new ObservableCollection<DirectoryItemViewModel>
                (
                    selectedItem.Children.Where
                    (
                        item => item.Type == DirectoryItemType.File
                    ).ToList()
                );
            }
        }

        #endregion

        #region Public Commands

        public ICommand SelectItemCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand MoveDirectoryItemCommand { get; set; }

        #endregion
    }
}
