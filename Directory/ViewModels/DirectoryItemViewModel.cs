using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FileExplorer
{
    /// <summary>
    /// A view model for each directory item.
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {

        #region Public properties

        /// <summary>
        /// The type of this item.
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The full path to the directory item.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of directory item.
        /// </summary>
        public string Name
        {
            get
            {
                if (Type == DirectoryItemType.Drive)
                    return FullPath;
                return DirectoryStructure.GetFileFolderName(FullPath);
            }
        }

        /// <summary>
        /// All directory items contained inside this item.
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded.
        /// </summary>
        public bool CanExpand => Type != DirectoryItemType.File;

        /// <summary>
        /// Indicates if the current item is expanded or not.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return Children?.Count(item => item != null) > 0;
            }
            set
            {
                //If the UI tells us to expand... 
                if (value)
                    //Find all children items.
                    Expand();
                //If the UI tells us to collapse...
                else
                    //Clear children
                    ClearChildren();
            }
        }

        /// <summary>
        /// Indicates if the current item is selected.
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion

        #region Public commands

        /// <summary>
        /// The command to expand this item.
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default construstor.
        /// </summary>
        /// <param name="fullPath">The full pfth of thid item.</param>
        /// <param name="Type">The type of item.</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            //Create commands.
            ExpandCommand = new RelayCommand(Expand);

            //Set type and path.
            FullPath = fullPath;
            Type = type;

            //Setup the children as needed
            ClearChildren();
        }

        #endregion

        #region Expand/Collapse methods

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            if (Type == DirectoryItemType.File)
                return;

            var children = DirectoryStructure.GetDirectoryContents(FullPath);
            Children = new ObservableCollection<DirectoryItemViewModel>
                (
                    children.Select(item => new DirectoryItemViewModel(item.FullPath, item.Type))
                );
        }

        /// <summary>
        /// Removes all children from the list, adding a dummy item to show the expand icon.
        /// </summary>
        private void ClearChildren()
        {
            //Clear items
            Children = new ObservableCollection<DirectoryItemViewModel>();

            //Add dummy item
            if(Type != DirectoryItemType.File)
                Children.Add(null);
        }

        #endregion
    }
}
