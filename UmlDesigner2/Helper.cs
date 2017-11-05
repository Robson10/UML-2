using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2
{
    public class DictionaryBlock
    {
        public Size BlockSize { get; set; } = new Size(100, 50);
        public Size MinSize { get; set; } = new Size(10, 10);
        public Color BackColor { get; set; } 
        public Color FontColor { get; set; }
        public int FontSize { get; set; }
        public string Label { get; set; }
        public string ImgPath { get; set; }
        public DictionaryBlock(Color backColor,Color fontColor,int fontSize,string label, string imgPath)
        {
            BackColor = backColor;
            FontColor = fontColor;
            FontSize= fontSize;
            Label = label;
            ImgPath = imgPath;
        }
    }

    public class Helper
    {
        #region View
        public static Color BackColor = Color.FromArgb(47, 47, 47);
        public static Color ButtonColor = Color.Gray;
        public static Color ButtonSelectedColor = Color.FromArgb(229, 130, 0);
        public static Color TextColor = Color.White;
        #endregion

        #region TabsArea

        #region Blocks

        public static string BlockTabText = "Bloki";

        #endregion

        #region Schemats

        public static string SchematsTabText = "Schematy";

        public static string SchematsPath =  System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Schemats";//@"E:\Programming\GitRepo\UmlDesigner2\bin\Debug\Schemats";

        public static string SchematsExtension = ".txt";

        #endregion

        #endregion

        #region Shortcuts
        private static string ConfigFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"/Configuration";
        private static string ShortcutsPath = AppDomain.CurrentDomain.BaseDirectory + @"/Configuration/Shortcuts.xml";

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

            if (!Directory.Exists(ConfigFolderPath))
                Directory.CreateDirectory(ConfigFolderPath);
            XmlSerializer xs = new XmlSerializer(typeof(List<Keys>));
            TextWriter tw = new StreamWriter(ShortcutsPath);
            xs.Serialize(tw, keys);
            tw.Close();
        }
        public static void LoadShortcuts()
        {
            List<Keys> x=new List<Keys>();
            if (Directory.Exists(ConfigFolderPath) && File.Exists(ShortcutsPath))
            {
                using (var sr = new StreamReader(ShortcutsPath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<Keys>));
                    x = (List<Keys>) xs.Deserialize(sr);
                    sr.Close();
                }
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

        public static Keys KeyMultiselect = Keys.Control|Keys.ControlKey;
        public static Keys KeyRun = Keys.F5;
        // ctrl
        public static Keys KeyCopy = Keys.Control|Keys.C;
        public static Keys KeyCut = Keys.Control | Keys.X;
        public static Keys KeyPaste = Keys.Control | Keys.V;
        public static Keys KeyUndo = Keys.Control | Keys.Z;
        public static Keys KeyRedo = Keys.Control | Keys.Y;
        public static Keys KeySaveFile = Keys.Control | Keys.S;
        public static Keys KeyNewFile = Keys.Control | Keys.N;
        public static Keys KeyOpenFile = Keys.Control | Keys.O;
        //shift
        public static Keys KeyDebug = Keys.Shift | Keys.F5;
        //ctrl shift
        public static Keys KeySaveFileAs = Keys.Control | Keys.Shift | Keys.S;
        public static Keys KeyOpenFileFromServer = Keys.Control | Keys.Shift | Keys.O;
        #endregion

        #region Clipboard

        public static string BlockClipboardFormat = "CopyOfBlocks";
        public static string LineClipboardFormat = "CopyOfLines";

        #endregion

        #region Canvas
        public static Color CanvasBgColor { get; set; } = Color.White;
        public static SolidBrush CanvasSelectionRectBrush { get; set; } = new SolidBrush(Color.FromArgb(70, Color.Blue));

        #endregion
        #region Rubbers

        public static Size RubberSize = new Size(10, 10);
        public static Color RubberColor = Color.Silver;

        #endregion

        #region Block & Line
        
        public enum Shape
        {
            Nothing = 0,
            Start = 1,
            Input = 2,
            Execution = 3,
            Decision = 4,
            End = 5,
            ConnectionLine = 6
        };

        public static Color DefaultSelectionColor = Color.DarkOrange;
        public static Color TrueLineBackColor = Color.Green;
        public static Color FalseLineBackColor = Color.Red;
        public static bool DefaultBlockAutoresize = false;

        public static Dictionary<Shape, DictionaryBlock> DefaultBlocksSettings = new Dictionary<Shape, DictionaryBlock>()
        {
            {
                Shape.Start,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "Start",
                    "UmlDesigner2.Icons.Start.jpg")
            },
            {
                Shape.End,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "End",
                    "UmlDesigner2.Icons.End.jpg")
            },
            {
                Shape.Input,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "B. Wprowadzania",
                    "UmlDesigner2.Icons.Input.jpg")
            },
            {
                Shape.Execution,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "B. Wykonawczy",
                    "UmlDesigner2.Icons.Execution.jpg")
            },
            {
                Shape.Decision,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "B. Warunkowy",
                    "UmlDesigner2.Icons.Decision.jpg")
            },
            {
                Shape.ConnectionLine,
                new DictionaryBlock(Color.FromArgb(255, 0, 0, 0), Color.Wheat, 10, "Linia łącząca",
                    "UmlDesigner2.Icons.ConnectionLine.jpg")
            },
            {
                Shape.Nothing,
                new DictionaryBlock(Color.Empty, Color.Empty, 21, "Nic", "")
            }
        };


        #endregion

        #region Clock

        public static bool ClockIsRunnable { get; set; } = true;
        public static bool ClockIsRunning { get; set; } = false;
        public static Color ClockBackColor { get; set; } = Color.White;

        public enum ClockType
        {
            Analog = 1,
            DigitalCountingDown = 2,
            DigitalCountingUp = 3
        };

        public static ClockType ClockChoosenType = ClockType.Analog;
        public static int ClockSize = 70;
        public static TimeSpan ClockTimeForExam = new TimeSpan(4, 0, 0);
        public static Color ClockColorHand = Color.Black;
        public static Color ClockColorScale = Color.Black;
        public static string ClockMessageWhenTimeIsOver = "Skończył ci się czas";
        public static Brush ClockPartOfTimeColor = Brushes.LightSalmon;

        #endregion
    }
}
