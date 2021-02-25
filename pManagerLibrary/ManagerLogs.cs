using System;
using System.IO;
using pManagerLibrary.Enum;
using pManagerLibrary.Interface;

namespace pManagerLibrary
{
    public sealed class ManagerLogs : IManagerLog
    {
        private string Folders { get; set; }
        private string Namearchive { get; set; }
        private string ExtensaoFile { get; set; }

        private ManagerLogs(string conn) => this.Namearchive = conn;

        public static ManagerLogs Connect(string conn) => new ManagerLogs(conn);

        public void Recording(string MsgLog, TManagerEnum model = TManagerEnum.dsError)
        {
            using (StreamWriter sw = File.AppendText(GetSequenceFile(model)))
            {
                sw.WriteLine("----------------------------------------------");
                sw.WriteLine(String.Format("{0} : {1}", Convert.ToString(DateTime.Now), MsgLog));
            }
        }
        public string[] Reading(TManagerEnum model = TManagerEnum.dsError)
        {
            return File.ReadAllLines(@Directory.GetCurrentDirectory() + "\\" + GetNamePaste(model) + "\\" + Namearchive + ExtensaoFile);
        }

        public string GetSequenceFile(TManagerEnum model)
        {
            Folders = GetNamePaste(model);

            if (@Directory.Exists(string.Format("{0}\\{1}", @Directory.GetCurrentDirectory(), Folders)) == false)
                Directory.CreateDirectory(string.Format("{0}\\{1}", @Directory.GetCurrentDirectory(), Folders));

            return GetFileName(Namearchive);
        }
        private string GetNamePaste(TManagerEnum model)
        {
            string GetReturn = "";
            switch (model)
            {
                case TManagerEnum.dsError:
                    GetReturn = "Logs";
                    ExtensaoFile = ".logs";
                    break;
                case TManagerEnum.dsRegister:
                    GetReturn = "Bases";
                    ExtensaoFile = ".txt";
                    break;
            }
            return GetReturn;
        }
        public void OpenFolder(string pathFile, TManagerEnum model = TManagerEnum.dsError)
        {
            string dirFIle = string.Format("{0}\\{1}\\{2}{3}",
                                Directory.GetCurrentDirectory(),
                                GetNamePaste(model),
                                Namearchive,
                                ExtensaoFile);

            string nameFolder = "";
            if (File.Exists(dirFIle) == true)
            {
                if (File.Exists(@"C:\\tmp") == false)
                {
                    Directory.CreateDirectory(@"C:\\tmp");
                }

                nameFolder = string.Format("C:\\tmp\\{0}", Path.GetRandomFileName());
                while (File.Exists(@nameFolder))
                {
                    nameFolder = string.Format("C:\\tmp\\{0}", Path.GetRandomFileName());
                }
            }
            try
            {
                File.Move(pathFile, nameFolder);
            }
            catch (Exception e)
            {
                ManagerLogs.Connect("FileLogs").Recording(e.ToString());
            }

            System.Diagnostics.Process.Start("explorer.exe", string.Format(@"{0}\\{1}", nameFolder, Path.GetFileName(pathFile)));
        }

        private bool GetLengthArchive(string source, string archive)
        {
            bool vController = false;
            foreach(FileInfo file in new DirectoryInfo(source).GetFiles())
            {
                if (file.Name == string.Format("{0}{1}", archive, ExtensaoFile))
                    vController = Biblio.BytesInMB(file.Length) >= BiblioConstante.Megabytemax;                 
            }
            return vController;
        }

        private string GetFileName(string filename)
        {
            int sequence = 1;
            string lfile = string.Format("{0}\\{1}\\{2}", Directory.GetCurrentDirectory(), Folders, filename);
            
            if (File.Exists(@lfile + ExtensaoFile))
            {
                if (GetLengthArchive(Folders, filename))
                {
                    bool vControl = true;
                    while (vControl)
                    {
                        if(GetLengthArchive(Folders, string.Format("{0}({1})",filename, sequence)))
                            sequence += 1;
                        else
                            vControl = false;
                    }
                    return lfile + "(" + sequence + ")" + ExtensaoFile;
                }
                else
                    return lfile + ExtensaoFile;
            }
            else
                return lfile + ExtensaoFile;
        }
    }
}
