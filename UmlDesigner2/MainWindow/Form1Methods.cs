using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using UmlDesigner2.Class;
using UmlDesigner2.Components.ResultComponent;
using UmlDesigner2.Components.Settings;
using UmlDesigner2.Components.ToolStripArea.Login;
using UmlDesigner2.Components.Workspace;

namespace UmlDesigner2.MainWindow
{
    partial class Form1
    {
        private string ActualFilePath= string.Empty;
        private void View()
        {
            WindowState = FormWindowState.Maximized;
            BackColor = Helper.BackColor;
            splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
            splitContainer3.Panel2.BackColor = System.Drawing.Color.Gray;
        }

        private void AddEvents()
        {
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;

            clock1.EgzamStarted += Clock1_EgzamStarted;
            clock1.EgzamEnded += Clock1_EgzamEnded;

            canvas1.HideBlockPoperites += Canvas1_HideBlockPoperites;
            canvas1.ShowBlockPoperites += Canvas1_ShowBlockPoperites;

            ToolStripAddEvents();
            TabsAddEvents();
        }
        
        private void NewFile()
        {
            ActualFilePath = string.Empty;
            canvas1.ClearCanvas();
        }

        //po załadowaniu bloków jezeli wczesniej jakiś zaznaczylismy załadowane bloki czasami są zaznaczone (czesto)
        private void OpenFile()
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.FileName = ".xml";
            openfile.Filter= "XML (*.xml)|*.xml";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                ActualFilePath = openfile.FileName;
                TextReader reader = null;
                try
                {
                    openfile.FileName = openfile.FileName.Substring(0, openfile.FileName.Count() - 3) + "hab";
                    if (openfile.CheckFileExists)
                    {
                        reader?.Close();
                        var serializer = new XmlSerializer(typeof(ListCanvasBlocks));
                        reader = new StreamReader(openfile.FileName);
                        Canvas.CanvObj = (ListCanvasBlocks) serializer.Deserialize(reader);
                        canvas1?.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Plik przechowujący informacje o blokach nie został znaleziony");
                    }
                    openfile.FileName = openfile.FileName.Substring(0, openfile.FileName.Count() - 3) + "hal";
                    if (openfile.CheckFileExists)
                    {
                        reader?.Close();
                        var serializer = new XmlSerializer(typeof(ListCanvasLines));
                        reader = new StreamReader(openfile.FileName);
                        Canvas.CanvLines = (ListCanvasLines) serializer.Deserialize(reader);
                        canvas1?.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Plik przechowujący połączenia bloków nie został znaleziony");
                    }
                }
                finally
                {
                        reader?.Close();
                }
                
            }
        }
        
        private void SaveFile()
        {
            if (ActualFilePath== string.Empty)
            {
                SaveFileAs();
                return;
            }
            Canvas.CanvObj.My_IsSelectedSetForAll(false);

            ActualFilePath = ActualFilePath.Substring(0, ActualFilePath.Count() - 3) + "xml";
            SaveObjToXmlFile(ActualFilePath, 0);
            ActualFilePath = ActualFilePath.Substring(0, ActualFilePath.Count() - 3) + "hab";
            SaveObjToXmlFile(ActualFilePath, Canvas.CanvObj);
            ActualFilePath = ActualFilePath.Substring(0, ActualFilePath.Count() - 3) + "hal";
            SaveObjToXmlFile(ActualFilePath, Canvas.CanvLines);
        }

        private void SaveObjToXmlFile(string path,object obj)
        {
            var xmlWriterSettings = new XmlWriterSettings() { Indent = true, NewLineHandling=NewLineHandling.Entitize};
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings))
            {
                serializer.Serialize(xmlWriter, obj);
                xmlWriter.Close();
            }
        }
        private void SaveFileAs()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = ".xml";
            savefile.Filter = "XML (*.xml)|*.xml";
            Canvas.CanvObj.My_IsSelectedSetForAll(false);   
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                ActualFilePath = savefile.FileName;
                ActualFilePath = ActualFilePath.Substring(0, ActualFilePath.Count() - 3) + "xml";
                SaveObjToXmlFile(ActualFilePath, 0);
                ActualFilePath = ActualFilePath.Substring(0, ActualFilePath.Count() - 3) + "hab";
                SaveObjToXmlFile(ActualFilePath, Canvas.CanvObj);
                ActualFilePath = ActualFilePath.Substring(0, ActualFilePath.Count() - 3) + "hal";
                SaveObjToXmlFile(ActualFilePath, Canvas.CanvLines);
            }
        }

        private void Debug()
        {
            Compile.Debug(Canvas.CanvObj, Canvas.CanvLines);
        }

        private void Run()
        {
            Compile.Run(Canvas.CanvObj, Canvas.CanvLines);
        }
        private void Options()
        {
            SettingWindow settings = new SettingWindow();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                Helper.SaveShortcuts();
            }
        }
        #region metody dla skrótów i przycisków z paska narzedzi

        private void SaveFileOnServer()
        {
            Components.ToolStripArea.SaveOnServer.SaveOnServerForm saveOnServerForm =
                new Components.ToolStripArea.SaveOnServer.SaveOnServerForm();
            if (saveOnServerForm.ShowDialog() == DialogResult.OK)
            {
            }
        }
        private void OpenFileFromServer()
        {
            Components.ToolStripArea.OpenFromServer.OpenFromServerForm openFromServerForm =
                new Components.ToolStripArea.OpenFromServer.OpenFromServerForm();
            if (openFromServerForm.ShowDialog() == DialogResult.OK)
            {
                canvas1.Invalidate();
                
                ActualFilePath = string.Empty;
            }
        }

        private void LogIn()
        {
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                myToolStrip1.Login();
                
            }
        }


        #endregion
    }
}
