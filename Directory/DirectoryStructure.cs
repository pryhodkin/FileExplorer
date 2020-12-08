using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileExplorer
{
    /// <summary>
    /// A helper class to query information about directories from OS.
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on the computer.
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // For every logical drive on machine create DirectoryItem
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem()
            {
                FullPath = drive,
                Type = DirectoryItemType.Drive
            }).ToList();
        }

        /// <summary>
        /// Gets the direcories top-level contents.
        /// </summary>
        /// <param name="fullPath">The full path to the directory.</param>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {

            var items = new List<DirectoryItem>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
                
            }
            catch { }

            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            return items;
        }


        /// <summary>
        /// Find the file or folder name from the full path.
        /// </summary>
        /// <param name="path">The full path to file.</param>
        public static string GetFileFolderName(string path)
        {
            //If we have an empty path, return also empty.
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            //Replace all slaches with backslashes.
            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            //Return all after the last backslash in path.
            return path.Substring(lastIndex + 1);
        }

        /// <summary>
        /// Moves a folder or a file to the specified path.
        /// </summary>
        /// <param name="item">A file or a folder.</param>
        /// <param name="path">Destination path.</param>
        /// <returns></returns>
        public static bool MoveDirectoryItem(DirectoryItem item, string path)
        {
            if (item.FullPath == path) return true;
            switch(item.Type)
            {
                case DirectoryItemType.File:
                    File.Move(item.FullPath, $"{path}\\{item.Name}");
                    break;
                case DirectoryItemType.Folder:
                    try
                    {
                        Directory.Move(item.FullPath, path);
                    }
                    catch
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }



        public static DirectoryItem GetDirectoryItem(string path)
        {
            if (path is null) return null;
            FileInfo fileInfo = new FileInfo(path);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            DirectoryItemType type = directoryInfo.FullName == path ? DirectoryItemType.Folder :
                                     DirectoryItemType.File;
            
            return new DirectoryItem { FullPath = path, Type = type };
        }
    }
}
