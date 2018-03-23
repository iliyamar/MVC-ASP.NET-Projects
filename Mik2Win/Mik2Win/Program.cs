using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mik2Win
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding encoding = Encoding.GetEncoding(1251);
            StreamReader streamReader = new StreamReader(args[0], encoding);
            string line = string.Empty;

            using (streamReader)
            {
                // string path = args[0];
                string fileName = Path.GetFileNameWithoutExtension(args[0]) + ' ' + "_WinCyr";
                string extension = Path.GetExtension(args[0]);
                string getDirectory = Path.GetDirectoryName(args[0]);
                string pathRenamed = String.Concat(getDirectory, '\\', fileName, extension);


                if (!File.Exists(pathRenamed))
                {
                    WriteToFile(encoding, streamReader, pathRenamed);
                }

                else if (File.Exists(pathRenamed))
                {
                    Console.WriteLine($"File {pathRenamed} already exist. Press Y/y to override it");
                    var key = Console.ReadKey();
                    if (key.KeyChar == 'y' || key.KeyChar == 'Y')
                    {
                        WriteToFile(encoding, streamReader, pathRenamed);
                    }
                }

            };
        }

        public static void WriteToFile(Encoding encoding, StreamReader streamReader, string pathRenamed)
        {
            string line;
            var writer = File.CreateText(pathRenamed);
            using (writer)
            {

                while ((line = streamReader.ReadLine()) != null) //read by line till end of file
                {
                    writer.WriteLine(W2Mik(line, encoding));
                }
            }


        }

        public static string W2Mik(string line, Encoding enc)
        {

            var inputInBytes = enc.GetBytes(line);

            for (int i = 0; i < inputInBytes.Length; i++)
            {
                if (inputInBytes[i] >= 128 && inputInBytes[i] <= 191)
                {
                    inputInBytes[i] += 64;
                }
            }

            return enc.GetString(inputInBytes);
        }

    }
}
