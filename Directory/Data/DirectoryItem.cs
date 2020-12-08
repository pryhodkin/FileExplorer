
using System;
using System.Windows;

namespace FileExplorer
{
    /// <summary>
    /// Information about a directory item such as a drive, a file, or a folder
    /// </summary>
    [Serializable]
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item.
        /// </summary>
        public DirectoryItemType Type { get; set; }
        /// <summary>
        /// The absolute path to this item.
        /// </summary>
        public string FullPath { get; set; }
        /// <summary>
        /// The file, folder or drive name.
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
    }
}
