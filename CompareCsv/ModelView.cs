using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;


namespace CompareCsv
{
    class ModelView : INotifyPropertyChanged
    {

        #region properties

        #region File1

        public string File1
        {
            get
            {
                return file1;
            }

            set
            {
                if (value != File1)
                {
                    file1 = value;
                    OnPropertyChanged("File1");
                }
            }
        }

        private string file1 = string.Empty;

        #endregion

        #region File2

        public string File2
        {
            get
            {
                return file2;
            }

            set
            {
                if (value != File2)
                {
                    file2 = value;
                    OnPropertyChanged("File2");
                }
            }
        }

        private string file2 = string.Empty;

        #endregion

        #region Status

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                if (value != Status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private string status = "Hello";

        #endregion

        #region Sort Options

        public string SortOptions
        {
            get
            {
                return sortOptions;
            }

            set
            {
                if (value != Status)
                {
                    sortOptions = value;
                    OnPropertyChanged("textBoxSortOn");
                }
            }
        }

        private string sortOptions = string.Empty;

        #endregion

        #endregion

        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region methods

        public bool SortFiles()
        {
            string[] columns = SortOptions.Split(',');

            if (columns.Length > 0 && columns.Count(x => string.IsNullOrEmpty(x)) == 0)
            {
                if (!SortFile(File1, columns))
                {
                    Status = "Parsing file1 failed";
                    return false;
                }

                if (!SortFile(File2, columns))
                {
                    Status = "Parsing file2 failed";
                    return false;
                }
            }

            return true;
        }

        private bool SortFile(string name, string [] columns)
        {
            List<string[]> data = null;
            string[] headers = null;

            using (var filestream = new System.IO.FileStream(name,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite))
            {
                var file = new System.IO.StreamReader(filestream);

                //read first line
                string lineOfText = file.ReadLine();

                if (lineOfText != null)
                {
                    headers = lineOfText.Split(',');
                    if (columns.Except(headers).Any())
                    {
                        return false;
                    }

                    data = new List<string[]>();

                    while ((lineOfText = file.ReadLine()) != null)
                    {
                        data.Add(lineOfText.Split(','));
                    }

                    if (columns.Length == 1)
                    {
                        int idx = Array.IndexOf(headers, columns[0]);

                        data = data.OrderBy(o => o[idx]).ToList();
                    }
                    else if (columns.Length == 2)
                    {
                        int idx1 = Array.IndexOf(headers, columns[0]);
                        int idx2 = Array.IndexOf(headers, columns[1]);

                        data = data.OrderBy(o => o[idx1]).ThenBy(o => o[idx2]).ToList();
                    }
                    else if (columns.Length == 3)
                    {
                        int idx1 = Array.IndexOf(headers, columns[0]);
                        int idx2 = Array.IndexOf(headers, columns[1]);
                        int idx3 = Array.IndexOf(headers, columns[2]);

                        data = data.OrderBy(o => o[idx1]).ThenBy(o => o[idx2]).ThenBy(o => o[idx3]).ToList();
                    }
                }
                else
                {
                    return false;
                }
               
            }

            if (data != null && data.Count > 0 && headers != null && headers.Length > 0)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(name))
                {
                    foreach (var h in headers)
                    {
                            file.Write(h + ",");
                    }

                    file.Write("\n");

                    foreach (var ar in data)
                    {
                        foreach (var s in ar)
                            file.Write(s + ",");

                        file.Write("\n");
                    }
                }
            }

            return true;
        }

        public bool Merge()
        {
            //read first file
            using (var filestream1 = new System.IO.FileStream(File1,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite))
            {
                using (var filestream2 = new System.IO.FileStream(File2,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite))
                {
                    var file1 = new System.IO.StreamReader(filestream1);
                    var file2 = new System.IO.StreamReader(filestream2);

                    //read first line
                    string lineOfText1 = file1.ReadLine();
                    string lineOfText2 = file2.ReadLine();

                    if (lineOfText1 != null && lineOfText2 != null)
                    {
                        string[] headers1 = lineOfText1.Split(',');
                        string[] headers2 = lineOfText2.Split(',');

                        if (!headers1.SequenceEqual(headers2))
                        {
                            Status = "Headers not the same";
                            return false;
                        }

                        string newFileName = Path.GetDirectoryName(File1) + "\\" + Path.GetFileNameWithoutExtension(File1) + "_compared.csv";

                        using (System.IO.StreamWriter fileOut = new System.IO.StreamWriter(newFileName))
                        {
                            fileOut.Write(lineOfText1);
                            fileOut.Write(",,,,,,");
                            fileOut.Write(lineOfText2 + "\n");

                            lineOfText1 = file1.ReadLine();
                            lineOfText2 = file2.ReadLine();

                            while (lineOfText1 != null || lineOfText2 != null)
                            {
                                if (lineOfText1 != null && lineOfText2 != null)
                                {
                                    fileOut.Write(lineOfText1);
                                    fileOut.Write(",,,,");
                                    fileOut.Write(lineOfText2);
                                    fileOut.Write("\n");

                                    lineOfText1 = file1.ReadLine();
                                    lineOfText2 = file2.ReadLine();
                                }
                                else if (lineOfText1 != null)
                                {
                                    fileOut.Write(lineOfText1 + "\n");
                                    lineOfText1 = file1.ReadLine();
                                }
                                else if (lineOfText2 != null)
                                {
                                    foreach (var h in headers1)
                                    {
                                        fileOut.Write(",");
                                    }

                                    fileOut.Write(",,,");
                                    fileOut.Write(lineOfText2 + "\n");
                                    lineOfText2 = file2.ReadLine();
                                }


                            }
                        }


                    }
                }

                return true;
            }
        }

        #endregion

    }
}
