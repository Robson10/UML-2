using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        #region TabsArea

        #region Blocks

        public static string BlockTabText = "Bloki";

        #endregion

        #region Schemats

        public static string SchematsTabText = "Schematy";

        public static string SchematsPath = "Z:\\Programowanie\\UmlDesigner2\\UmlDesigner2\\bin\\Debug\\Schemats"
            ; // System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Schemats";

        public static string SchematsExtension = ".txt";

        #endregion

        #endregion

        #region Shortcuts

        public static Keys MultiselectKey = Keys.ControlKey;

        #endregion

        #region Clipboard

        public static string BlockClipboardFormat = "CopyOfBlocks";
        public static string LineClipboardFormat = "CopyOfLines";

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

        public static Dictionary<Shape, DictionaryBlock> DefaultBlocksSettings = new Dictionary<Shape, DictionaryBlock>()
        {
            {
                Shape.Start,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 21, "Blok Startu",
                    "UmlDesigner2.Icons.Start.jpg")
            },
            {
                Shape.End,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 21, "Blok Końca",
                    "UmlDesigner2.Icons.End.jpg")
            },
            {
                Shape.Input,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 21, "Blok Wprowadzania",
                    "UmlDesigner2.Icons.Input.jpg")
            },
            {
                Shape.Execution,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 21, "Blok Wykonawczy",
                    "UmlDesigner2.Icons.Execution.jpg")
            },
            {
                Shape.Decision,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 21, "Blok Decyzyjny",
                    "UmlDesigner2.Icons.Decision.jpg")
            },
            {
                Shape.ConnectionLine,
                new DictionaryBlock(Color.FromArgb(255, 0, 0, 0), Color.Wheat, 21, "Linia łącząca",
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
