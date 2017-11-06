using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace UmlDesigner2
{
    partial class Helper
    {
        public static void SaveShortcuts()
        {
            List<Keys> keys = new List<Keys>();
            keys.Add(KeyRun);
            keys.Add(KeyCopy);
            keys.Add(KeyCut);
            keys.Add(KeyPaste);
            keys.Add(KeyUndo);
            keys.Add(KeyRedo);
            keys.Add(KeySaveFile);
            keys.Add(KeyNewFile);
            keys.Add(KeyOpenFile);
            keys.Add(KeyDebug);
            keys.Add(KeySaveFileAs);
            keys.Add(KeyOpenFileFromServer);
            SaveXML(ShortcutsPath, keys);
        }
        public static void LoadShortcuts()
        {
            List<Keys> x=new List<Keys>();
            x=(List <Keys> )LoadXML(ShortcutsPath, x);
            if (x != null && x.Count==12)
            {
                KeyRun = x[0];
                KeyCopy = x[1];
                KeyCut = x[2];
                KeyPaste = x[3];
                KeyUndo = x[4];
                KeyRedo = x[5];
                KeySaveFile = x[6];
                KeyNewFile = x[7];
                KeyOpenFile = x[8];
                KeyDebug = x[9];
                KeySaveFileAs = x[10];
                KeyOpenFileFromServer = x[11];
            }
        }

        private static object LoadXML(string FilePath,object TypeOfSavedData)
        {
            if (File.Exists(FilePath))
            {
                using (var sr = new StreamReader(FilePath))
                {
                    XmlSerializer xs = new XmlSerializer(TypeOfSavedData.GetType());
                    TypeOfSavedData = xs.Deserialize(sr);
                    sr.Close();
                }
                return TypeOfSavedData;
            }
            return null;
        }

        private static void SaveXML(string FilePath,object TypeOfSavedData)
        {
            if (!Directory.Exists(ConfigFolderPath))
                Directory.CreateDirectory(ConfigFolderPath);
            XmlSerializer xs = new XmlSerializer(TypeOfSavedData.GetType());
            TextWriter tw = new StreamWriter(FilePath);
            xs.Serialize(tw, TypeOfSavedData);
            tw.Close();
        }

    }
}
