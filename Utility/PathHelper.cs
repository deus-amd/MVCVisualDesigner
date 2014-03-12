using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner.Utility
{
    public class PathHelper
    {
        // utility methods
        public static string GetAbsolutePath(string path)
        {
            var curAssembly = System.Reflection.Assembly.GetCallingAssembly();
            UriBuilder uri = new UriBuilder(curAssembly.CodeBase);

            string curPath = Uri.UnescapeDataString(uri.Path);
            return GetAbsolutePath(path, curPath);
        }

        public static string GetRelativePath(string path)
        {
            var curAssembly = System.Reflection.Assembly.GetCallingAssembly();
            return MakeRelativePath(curAssembly.CodeBase, path);
        }

        public static string GetAbsolutePath(string path, string refFilePath)
        {
            if (Path.IsPathRooted(path)) return path;

            refFilePath = Path.GetDirectoryName(refFilePath);
            path = Path.Combine(refFilePath, path);
            path = Path.GetFullPath(path);
            return path;
        }

        public static string GetRelativePath(string path, string refFilePath)
        {
            return MakeRelativePath(refFilePath, path);
        }

        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private static String MakeRelativePath(String fromPath, String toPath)
        {
            if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
            if (String.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.ToUpperInvariant() == "FILE")
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }
    }
}
