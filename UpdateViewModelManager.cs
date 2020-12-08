using System;
using System.Collections.Generic;
using System.Text;

namespace FileExplorer
{
    public static class UpdateViewModelManager
    {
        private static List<FileExplorerViewModel> fileExplorerViewModels { get; set; } = new List<FileExplorerViewModel>();

        public static void AddFileExplorerViewModel(FileExplorerViewModel item)
        {
            if (fileExplorerViewModels.Contains(item)) return;
            fileExplorerViewModels.Add(item);
        }

        public static void UpdateFileExplorerViewModels()
        {
            foreach (var vm in fileExplorerViewModels)
                vm.Update();
        }
    }
}
