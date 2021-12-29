using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace NTWordUse
{
    public class classGlobal
    {
        /**********************************************************************************************
         *                                                                                            *
         *                                       classGlobal                                          *
         *                                       ===========                                          *
         *                                                                                            *
         *   Variables that will need to be accessed in several other classes or forms.               *
         *                                                                                            *
         *   Since Registry values will also need to be available globally, Registry management has   *
         *   been included in the global class.                                                       *
         *                                                                                            *
         **********************************************************************************************/

        bool doesIncludeLXX = true;

        /*============================================================================================*
         *                                                                                            *
         *  Codes for how references are displayed in frmReferences:                                  *
         *                                                                                            *
         *    ntOnlyCode:                                                                             *
         *       1  Display text as Red                                                               *
         *       2  Display text as normal bold                                                       *
         *       3  Display as Red and Bold                                                           *
         *                                                                                            *
         *    lxxOnlyCode:                                                                            *
         *       1  Display text as Grey                                                              *
         *       2  Display text as Italic                                                            *
         *       3  Display text as both Grey and Italic                                              *
         *                                                                                            *
         *    ntAndLxxCode                                                                            *
         *       1  Display text as Orange                                                            *
         *       2  Display text as normal (black)                                                    *
         *                                                                                            *
         *============================================================================================*/
        int ntOnlyCode = 1, lxxOnlyCode = 1, ntAndLxxCode = 1;
        String RegistryKeyString = @"software\LFCConsulting\NTWordUse";
        String[] registryKeys = { "Include LXX", "Code for only NT", "Code for only LXX", "Code for both" };
        SortedList<int, classBooks> bookList = new SortedList<int, classBooks>();
        object regValue;
        RegistryKey baseKey;

        /*============================================================================================*
         *                                                                                            *
         *  File location                                                                             *
         *  -------------                                                                             *
         *                                                                                            *
         *  baseDirectory:    The root directory for files, referenced by the registry                *
         *  altBaseDirectory: The root directory for files when in development mode                   *
         *       3  Display as Red and Bold                                                           *
         *                                                                                            *
         *    lxxOnlyCode:                                                                            *
         *       1  Display text as Grey                                                              *
         *       2  Display text as Italic                                                            *
         *       3  Display text as both Grey and Italic                                              *
         *                                                                                            *
         *    ntAndLxxCode                                                                            *
         *       1  Display text as Orange                                                            *
         *       2  Display text as normal (black)                                                    *
         *                                                                                            *
         *============================================================================================*/

        String baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LFCConsulting\NTWordUse\Source\";
        String altBaseDirectory = Path.GetFullPath(@"..\Source");
        String helpDirectory = "Help", lxxTextDirectory = "LXX_Text", lxxTitles = "LXX_Titles.txt", ntTextFile = "NTText.txt", ntTitles = "Titles.txt";

        public bool DoesIncludeLXX { get => doesIncludeLXX; set => doesIncludeLXX = value; }
        public int NtOnlyCode { get => ntOnlyCode; set => ntOnlyCode = value; }
        public int LxxOnlyCode { get => lxxOnlyCode; set => lxxOnlyCode = value; }
        public int NtAndLxxCode { get => ntAndLxxCode; set => ntAndLxxCode = value; }
        public SortedList<int, classBooks> BookList { get => bookList; set => bookList = value; }
        public string BaseDirectory { get => baseDirectory; set => baseDirectory = value; }
        public string HelpDirectory { get => helpDirectory; set => helpDirectory = value; }
        public string LxxTextDirectory { get => lxxTextDirectory; set => lxxTextDirectory = value; }
        public string LxxTitles { get => lxxTitles; set => lxxTitles = value; }
        public string NtTextFile { get => ntTextFile; set => ntTextFile = value; }
        public string NtTitles { get => ntTitles; set => ntTitles = value; }

        public void initialiseRegistry()
        {
            int tempVal;

            openRegistry();
            regValue = baseKey.GetValue(registryKeys[0]);
            if (regValue != null)
            {
                tempVal = (int)regValue;
                if (tempVal == 0) doesIncludeLXX = false;
                else doesIncludeLXX = true;
            }
            regValue = baseKey.GetValue(registryKeys[1]);
            if (regValue != null) ntOnlyCode = (int)regValue;
            regValue = baseKey.GetValue(registryKeys[2]);
            if (regValue != null) lxxOnlyCode = (int)regValue;
            regValue = baseKey.GetValue(registryKeys[3]);
            if (regValue != null) ntAndLxxCode = (int)regValue;
            regValue = baseKey.GetValue("Base Directory");
            if (regValue == null) manageSourceFiles();
            else
            {
                baseDirectory = regValue.ToString();
                if (Directory.Exists(baseDirectory)) baseDirectory += @"\";
                else manageSourceFiles();
            }
        }

        private void manageSourceFiles()
        {
            DirectoryInfo diSource, diTarget;

            Directory.CreateDirectory(baseDirectory);
            // Make sure the target directory is empty
            diTarget = new DirectoryInfo(baseDirectory);
            try
            {
                foreach (DirectoryInfo diName in diTarget.GetDirectories()) diName.Delete(true);
            }
            catch
            {
                // Do nothing
            }
            foreach (FileInfo fiFile in diTarget.GetFiles()) fiFile.Delete();
            // Now copy data from the current location
            diSource = new DirectoryInfo(altBaseDirectory);
            foreach (DirectoryInfo diName in diSource.GetDirectories()) cloneSingleDirectory(diTarget, diName);
            // Now get any files in the current directory
            foreach (FileInfo fiFile in diSource.GetFiles()) fiFile.CopyTo(diTarget.FullName + @"\" + fiFile.Name);
            openRegistry();
            if (baseKey != null) baseKey.SetValue("Base Directory", baseDirectory, RegistryValueKind.String);
            closeRegistryKey();
            baseDirectory += @"\";
        }

        public void cloneSingleDirectory(DirectoryInfo diTargetDirectory, DirectoryInfo diSourceDirectory)
        {
            DirectoryInfo diTarget;

            // Create the new directry
            if (!Directory.Exists(diTargetDirectory.FullName + @"\" + diSourceDirectory.Name)) Directory.CreateDirectory(diTargetDirectory.FullName + @"\" + diSourceDirectory.Name);
            // Get info for the new directory
            diTarget = new DirectoryInfo(diTargetDirectory.FullName + @"\" + diSourceDirectory.Name);
            // Now go down a level and do the same
            foreach (DirectoryInfo diName in diSourceDirectory.GetDirectories()) cloneSingleDirectory(diTarget, diName);
            // Now get any files in the current directoy
            foreach (FileInfo fiFile in diSourceDirectory.GetFiles()) fiFile.CopyTo(diTargetDirectory.FullName + @"\" + diSourceDirectory.Name + @"\" + fiFile.Name);
        }

        public void openRegistry()
        {
            baseKey = Registry.CurrentUser.OpenSubKey(RegistryKeyString, true);
            if (baseKey == null) baseKey = Registry.CurrentUser.CreateSubKey(RegistryKeyString, true);
        }

        public void closeRegistryKey()
        {
            if (baseKey != null) baseKey.Close();
        }

        public void updateRegistrySetting( int entryValue, int keyIndex)
        {
            initialiseRegistry();
            baseKey.SetValue(registryKeys[keyIndex], entryValue, RegistryValueKind.DWord);
            closeRegistryKey();
        }
    }
}
