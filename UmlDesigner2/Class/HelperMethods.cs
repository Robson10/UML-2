using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace UmlDesigner2.Class
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
        public static DataSet DataBaseSelect(string query)
        {
            DataSet dataSet = new DataSet(); ;
            SqlDataAdapter adapter = new SqlDataAdapter();
            using (SqlConnection con = new SqlConnection() { ConnectionString= ConnectionString })
            {
                con.Open();
                adapter = new SqlDataAdapter(query, ConnectionString);
                con.Close();
            }
            adapter.Fill(dataSet, "tab");
            return dataSet;
        }
        public static void DatabaseExecuteQuery(string querry)
        {
            using (SqlConnection con = new SqlConnection() { ConnectionString = ConnectionString })
            {
                con.Open();
                SqlCommand command = new SqlCommand(querry, con);
                command.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
