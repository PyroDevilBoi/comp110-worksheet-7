using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
    public static class DirectoryUtils
    {
        // Return the size, in bytes, of the given file
        public static long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }

        // Return true if the given path points to a directory, false if it points to a file
        public static bool IsDirectory(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }

        // Method added by me: returns a string array of files in the specified directory by reference.
        public static void GetFileArray(out string[] file_array, string dir)
        {
            file_array = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);

        }

        // Return the total size, in bytes, of all the files below the given directory
        //Add files to 'file_array'using the GetFileArray method, then indent through the array and update 'file_size' by adding the sizes of each file in the said array.
        public static long GetTotalSize(string directory)
        {
            string[] file_array;
            long file_size = 0;


            GetFileArray(out file_array, directory);
            for (int i = 0; i < GetFileSize(directory); i++)
                file_size = file_size + GetFileSize(file_array[i]);


            return file_size;
        }



        // Return the number of files (not counting directories) below the given directory
        public static int CountFiles(string directory)
        {
            string[] file_array;
            //int counter = 0;
            GetFileArray(out file_array, directory);
            //  for (int i = 0; i < GetFileSize(directory); i++)
            //    counter++;

            return file_array.Length;


        }

        // Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
        public static int GetDepth(string directory)
        {
            int counter = 0;
            string[] depth_array = Directory.GetDirectories(directory);

            foreach (string i in depth_array)
                counter++;
            return counter;


        }

        // Get the path and size (in bytes) of the smallest file below the given directory
        /* We load the arrays with the files (names and size) and a minimum tuple, then proceed with the usual min-finding algorithm.
        We will perform the method recursively for every subdirectory.
        */

        public static Tuple<string, long> GetSmallestFile(string directory)

        {


            string[] file_array = Directory.GetFiles(directory);
            string[] subdir_array = Directory.GetDirectories(directory);
            Tuple<string, long> min_file_size = new Tuple<string, long>(" ", long.MaxValue);

            // GetFileArray(out file_array, directory);

            foreach (string file in file_array)
                if (GetFileSize(file) < min_file_size.Item2)
                    min_file_size = new Tuple<string, long>(file, GetFileSize(file));

            //Keep updating the min recursively
            foreach (string subdir in subdir_array)
                GetSmallestFile(subdir);

            return min_file_size;
        }





        // Get the path and size (in bytes) of the largest file below the given directory
        /* We load the arrays with the files (names and size) and a maximum tuple, then proceed with the usual max-finding algorithm.
            We will perform the method recursively for every subdirectory.
            */
        public static Tuple<string, long> GetLargestFile(string directory)
        {
            string[] file_array = Directory.GetFiles(directory);
            string[] subdir_array = Directory.GetDirectories(directory);
            Tuple<string, long> max_file_size = new Tuple<string, long>(" ", long.MinValue);

            foreach (string file in file_array)
                if (GetFileSize(file) > max_file_size.Item2)
                    max_file_size = new Tuple<string, long>(file, GetFileSize(file));

            //Keep updating the max recursively
            foreach (string subdir in subdir_array)
                GetSmallestFile(subdir);

            return max_file_size;
        }

        // Get all files whose size is equal to the given value (in bytes) below the given directory
        public static IEnumerable<string> GetFilesOfSize(string directory, long size)
        {
            string[] file_array;
            long file_size;
            GetFileArray(out file_array, directory);
            List<string> equalFiles = new List<string>();

            foreach (string file in file_array)
            {
                //get the size of all the files in the given directory/stored in the array and compare them to the parameter value.
                file_size = GetFileSize(file);
                if (file_size == size)
                    equalFiles.Add(file);
            }
            return equalFiles;
        }
    }
}
