using System;
using System.Collections.Generic;
using System.Text;

namespace FileExplorer
{
    class FileViewModel : DirectoryItemViewModel
    {
        public string FileType => Name.Substring(Name.LastIndexOf('.') + 1);
        public FileViewModel(string path)
            : base(path, DirectoryItemType.File)
        {
            
        }
    }
}
