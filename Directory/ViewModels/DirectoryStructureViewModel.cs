using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FileExplorer
{
    /// <summary>
    /// The view model for applications main Directory view.
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {

        #region Public properties

        /// <summary>
        /// A list of all directories on machine.
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        /// <summary>
        /// Selected directory item.
        /// </summary>
        public DirectoryItemViewModel SelectedItem
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DirectoryStructureViewModel()
        {
            Items = new ObservableCollection<DirectoryItemViewModel>
            (
                DirectoryStructure.GetLogicalDrives().Select
                (
                    item => new DirectoryItemViewModel(item.FullPath, item.Type)
                )
            );
        }

        #endregion

        #region Special methods

        public void Update()
        {
            var expanedItems = GetExpandedItems(Items.ToList());

            Items = new ObservableCollection<DirectoryItemViewModel>
            (
                DirectoryStructure.GetLogicalDrives().Select
                (
                    item => new DirectoryItemViewModel(item.FullPath, item.Type)
                )
            );

            ExpandItems(expanedItems);
        }

        public List<string> GetExpandedItems(List<DirectoryItemViewModel> items)
        {
            List<string> result = new List<string>();
            foreach(var i in items)
            {
                if (i.CanExpand && i.IsExpanded)
                    result.Add(i.FullPath);
                result.AddRange(GetExpandedItems(i, result));
            }
            return result;
        }

        public List<string> GetExpandedItems(DirectoryItemViewModel item, List<string> result)
        {
            var list = item.Children.Where(i => i.CanExpand && i.IsExpanded).ToList();
            result.AddRange(list.Select(i => i.FullPath).ToList());
            foreach(var i in list)
            {
                GetExpandedItems(i, result);
            }
            return result;
        }

        public void ExpandItems(List<string> items)
        {
            ExpandItems(items.Distinct().ToList(), Items);
        }

        public void ExpandItems(List<string> items, ObservableCollection<DirectoryItemViewModel> directoryItems)
        {
            foreach (var item in directoryItems)
            {
                if (item is { })
                {
                    if (items.Contains(item.FullPath) && item.CanExpand)
                    {
                        item.IsExpanded = true;
                        items.Remove(item.FullPath);
                    }
                    ExpandItems(items, item.Children);
                }
            }
        }

        #endregion
    }
}
