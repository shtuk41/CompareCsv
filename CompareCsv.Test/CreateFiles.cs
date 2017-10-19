using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompareCsv.Test
{
    [TestClass]
    public class CreateFiles
    {
        [TestMethod]
        public void CreateFile1()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Aleksander\Desktop\Projects\CompareCsv\CompareCsv\File1.csv"))
            {
                file.Write("Header1,Header2,\n");
                file.Write("3,4,\n");
                file.Write("1,1,\n");
                file.Write("1,3,\n");
                file.Write("2,1,\n");
                file.Write("2,2,\n");
                file.Write("3,1,\n");
                file.Write("3,2,\n");
                file.Write("3,3,\n");
                file.Write("2,3,\n");
                file.Write("1,2,\n");
            }
        }

        [TestMethod]
        public void CreateFile2()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Aleksander\Desktop\Projects\CompareCsv\CompareCsv\File2.csv"))
            {
                file.Write("Header1,Header2,\n");
                file.Write("3,4,\n");
                file.Write("1,1,\n");
                file.Write("1,3,\n");
                file.Write("2,1,\n");
                file.Write("2,2,\n");
                file.Write("3,1,\n");
                file.Write("3,2,\n");
                file.Write("3,3,\n");
                file.Write("2,3,\n");
                file.Write("1,2,\n");
            }
        }

        
        [TestMethod]
        public void CreateRandomFile1()
        {
            Random rnd = new Random();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Aleksander\Desktop\Projects\CompareCsv\CompareCsv\File1.csv"))
            {
                CreateRandomFile(file, rnd);
            }
        }

        [TestMethod]
        public void CreateRandomFile2()
        {
            Random rnd = new Random();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Aleksander\Desktop\Projects\CompareCsv\CompareCsv\File2.csv"))
            {
                CreateRandomFile(file, rnd);
            }
        }

        private void CreateRandomFile(System.IO.StreamWriter file, Random rnd, int numberOfRows = 50)
        {

            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            int wLength = 5;

            file.Write("Header1,Header2,Header3\n");
            for (int ii = 0; ii < numberOfRows; ii++)
            {

                for (int jj = 0; jj < 3; jj++)
                {
                    var builder = new StringBuilder();

                    for (var i = 0; i < wLength; i++)
                    {
                        var c = pool[rnd.Next(0, pool.Length)];
                        builder.Append(c);
                    }

                    file.Write(string.Format("{0},", builder));
                }

                file.Write(",\n");

                
            }
        }

        [TestMethod]
        public void CreateRandomFiles_FirstFileLonger()
        {
            Random rnd = new Random();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Aleksander\Desktop\Projects\CompareCsv\CompareCsv\File1.csv"))
            {
                CreateRandomFile(file, rnd, 3);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Aleksander\Desktop\Projects\CompareCsv\CompareCsv\File2.csv"))
            {
                CreateRandomFile(file, rnd, 2);
            }
        }

        [TestMethod]
        public void CreateRandomFiles_SecondFileLonger()
        {
            Random rnd = new Random();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Aleksander\Desktop\Projects\CompareCsv\CompareCsv\File1.csv"))
            {
                CreateRandomFile(file, rnd, 20);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Aleksander\Desktop\Projects\CompareCsv\CompareCsv\File2.csv"))
            {
                CreateRandomFile(file, rnd, 30);
            }
        }


    }
}
