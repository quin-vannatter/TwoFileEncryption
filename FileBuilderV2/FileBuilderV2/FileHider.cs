using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace FileBuilderV2
{
    public partial class FileHider : Form
    {
        int state = 0;
        string hideFile = "";
        string keyFile = "";
        string mapFile = "";

        int totalSize = 0;
        int completedSize = 0;

        public FileHider()
        {
            InitializeComponent();
            butStart.Enabled = false;
            butCancel.Enabled = false;
            this.AllowDrop = true;
            this.DragDrop += FileHider_DragDrop;
            this.DragEnter += FileHider_DragEnter;
            labInfo.Text  = "Click and drag key or\nfile to be hidden on to the plus.";
        }

        void FileHider_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void FileHider_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string [])e.Data.GetData(DataFormats.FileDrop))[0];
            if (state == 0)
            {
                butCancel.Enabled = true;
                if (file.Split('.').Length != 2 || file.Split('.')[1] != "fkey")
                {
                    hideFile = file;
                    labInfo.Text  = "File to be hidden has been added.\nPlease add map file.";
                }
                else
                {
                    keyFile = file;
                    labInfo.Text  = "Key has been added.\nPlease add map file.";
                }
                state = 1;
            }
            else if(state==1)
            {
                labInfo.Text = "Map file added.\n";
                if (keyFile != "") labInfo.Text += "Press start to create file.";
                else labInfo.Text += "Press start to create key.";
                mapFile = file;
                state = 2;
                butStart.Enabled = true;
            }
        }

        void HideFile(string hideFileLoc, string mapFileLoc, FileHider caller)
        {
                Stream mapStream = File.Open(mapFileLoc, FileMode.Open);
                byte[] mapBytes = new byte[mapStream.Length];
                mapStream.Read(mapBytes, 0, mapBytes.Length);
                mapStream.Close();

                Stream fileStream = File.Open(hideFileLoc, FileMode.Open);
                byte[] fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, fileBytes.Length);
                fileStream.Close();

                List<List<int>> byteLocations = new List<List<int>>();
                for (int i = 0; i < 256; i++)
                {
                    byteLocations.Add(new List<int>());
                    for (int h = 0; h < mapBytes.Length; h++)
                    {
                        if (mapBytes[h] == i)
                        {
                            byteLocations[i].Add(h);
                        }
                    }
                }
                List<int> missingBytes = new List<int>();
                for (int i = 0; i < byteLocations.Count; i++)
                {
                    if (byteLocations[i].Count == 0)
                    {
                        missingBytes.Add(i);
                    }
                }
                bool validMap = true;
                if (missingBytes.Count != 0)
                {
                    for (int i = 0; i < missingBytes.Count && validMap; i++)
                    {
                        for (int h = 0; h < fileBytes.Length && validMap; h++)
                        {
                            if (missingBytes[i] == fileBytes[h])
                            {
                                validMap = false;
                            }
                        }
                    }
                }
                if (validMap)
                {
                    List<int> locations = new List<int>();
                    List<int> rawData = new List<int>();
                    completedSize = 0;
                    totalSize = fileBytes.Length;
                    foreach (byte b in fileBytes)
                    {
                        completedSize++;
                        List<int> newBytes = ConvertDec(FindLocation(byteLocations, b));
                        locations.Add(newBytes.Count);
                        foreach (int i in newBytes) rawData.Add(i);
                    }
                    List<int> locSize = ConvertDec(locations.Count);
                    foreach (int i in locSize) rawData.Add(i);
                    rawData.Add(locSize.Count);
                    string fileName = "";
                    string extention = "";
                    if (hideFileLoc.Split('.').Length > 1)
                    {
                        fileName = hideFileLoc.Split('.')[0] + ".fkey";
                        extention = hideFileLoc.Split('.')[1];
                        byte[] extBytes = ASCIIEncoding.ASCII.GetBytes(extention);
                        foreach (byte b in extBytes) rawData.Add(b);
                        rawData.Add(extBytes.Length);
                    }
                    else
                    {
                        fileName = hideFileLoc + ".fkey";
                        rawData.Add(0);
                    }
                    foreach (int i in rawData) locations.Add(i);
                    byte[] final = new byte[locations.Count];
                    for (int i = 0; i < locations.Count; i++) final[i] = (byte)locations[i];
                    File.WriteAllBytes(fileName, final);
                    MessageBox.Show("Key Created.");
                }
                else
                {
                    MessageBox.Show("Map is not sufficant.");
                }
            state = 0;
        }

        List<int> ConvertDec (int number)
        {
            List<int> numbers = new List<int>();
            for(int i = number;i>0;i = (int)Math.Floor((decimal)i/256))
            {
                numbers.Add(i % 256);
            }
            numbers.Reverse();
            return numbers;
        }

        int ConvertByte(List<int> digits)
        {
            int result = 0;
            for (int i = 0; i < digits.Count; i++) result += digits[i] * (int)Math.Pow(256, digits.Count - 1 - i);
            return result;
        }

        int FindLocation (List<List<int>> locations, int byteNeeded)
        {
            Random ran = new Random();
            return locations[byteNeeded][ran.Next(locations[byteNeeded].Count)];
        }

        void BuildFile(string keyFileLoc, string mapFileLoc, FileHider caller)
        {
            try
            {
                Stream mapStream = File.Open(mapFileLoc, FileMode.Open);
                byte[] mapBytes = new byte[mapStream.Length];
                mapStream.Read(mapBytes, 0, mapBytes.Length);
                mapStream.Close();

                Stream keyStream = File.Open(keyFileLoc, FileMode.Open);
                byte[] keyBytes = new byte[keyStream.Length];
                keyStream.Read(keyBytes, 0, keyBytes.Length);
                keyStream.Close();

                int extSize = keyBytes[keyBytes.Length - 1];
                List<byte> extNums = new List<byte>();
                for (int i = keyBytes.Length - extSize - 1; i < keyBytes.Length - 1; i++) extNums.Add(keyBytes[i]);
                string extention = String.Join("",Encoding.ASCII.GetChars(extNums.ToArray()));
                int locNumSize = keyBytes[keyBytes.Length - extSize - 2];
                List<int> locNums = new List<int>();
                for (int i = keyBytes.Length - locNumSize - extSize - 2; i < keyBytes.Length - extSize - 2; i++) locNums.Add(keyBytes[i]);
                int startNum = ConvertByte(locNums);
                int startCount = startNum;
                List<int> locations = new List<int>();
                for (int i = 0; i < startNum; i++)
                {
                    List<int> newInt = new List<int>();
                    for (int h = startCount; h < keyBytes[i] + startCount; h++)
                    {
                        newInt.Add(keyBytes[h]);
                    }
                    startCount += keyBytes[i];
                    locations.Add(ConvertByte(newInt));
                }
                byte[] final = new byte[locations.Count];
                totalSize = final.Length;
                completedSize = 0;
                for (int i = 0; i < final.Length; i++)
                {
                    final[i] = mapBytes[locations[i]];
                    completedSize = i;
                }
                string fileName = keyFileLoc.Split('.')[0] + "." + extention;
                File.WriteAllBytes(fileName, final);
                MessageBox.Show("File Created.");
            }
            catch
            {
                MessageBox.Show("File failed to be created.");
            }
            state = 0;
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            if(state==2)
            {
                Thread thread;
                if (hideFile != "") 
                {
                    thread = new Thread(delegate() { HideFile(hideFile, mapFile, this); });
                    thread.Start();
                }
                else
                {
                    thread = new Thread(delegate() { BuildFile(keyFile, mapFile, this); });
                    thread.Start();
                }
                butCancel.Enabled = false;
                butStart.Enabled = false;
                while(thread.IsAlive)
                {
                    if(totalSize>0)
                    {
                        pBpercent.Value = (int)((float)completedSize / (float)totalSize  * 100);
                    }
                    Thread.Sleep(1);
                }
                pBpercent.Value = 0;
                hideFile = "";
                mapFile = "";
                keyFile = "";
                labInfo.Text = "Click and drag key or\nfile to be hidden on to the plus.";
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            if(state!=0)
            {
                butStart.Enabled = false;
                butCancel.Enabled = false;
                state = 0;
                hideFile = "";
                mapFile = "";
                keyFile = "";
                labInfo.Text = "Click and drag key or\nfile to be hidden on to the plus.";
            }
        }
    }
}
