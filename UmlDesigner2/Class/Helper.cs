﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SbWinNew.Class
{


    public partial class Helper
    {
        public static string ConnectionString = "data source=DESKTOP-M4NPT9R;initial catalog=SbWinNEW;Integrated Security=SSPI";
        public static string CompilePath = AppDomain.CurrentDomain.BaseDirectory;
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

        #endregion

        #region Shortcuts
        private static string ConfigFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"/Configuration";
        private static string ShortcutsPath = AppDomain.CurrentDomain.BaseDirectory + @"/Configuration/Shortcuts.xml";

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
        public static Keys SelectAll = Keys.Control | Keys.A;
        //shift
        public static Keys KeyOpenFileFromServer = Keys.Shift | Keys.O;
        public static Keys KeySaveFileOnServer = Keys.Shift | Keys.S;
        public static Keys KeyDebug = Keys.Shift | Keys.F5;
        //ctrl shift
        public static Keys KeySaveFileAs = Keys.Control | Keys.Shift | Keys.S;
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
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "B. Start",
                    "SbWinNew.Icons.Start.jpg")
            },
            {
                Shape.End,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "B. Koniec",
                    "SbWinNew.Icons.End.jpg")
            },
            {
                Shape.Input,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "B. Wejścia/wyjścia",
                    "SbWinNew.Icons.Input.jpg")
            },
            {
                Shape.Execution,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "B. Wykonawczy",
                    "SbWinNew.Icons.Execution.jpg")
            },
            {
                Shape.Decision,
                new DictionaryBlock(Color.FromArgb(255, 130, 130, 130), Color.Wheat, 10, "B. Warunkowy",
                    "SbWinNew.Icons.Decision.jpg")
            },
            {
                Shape.ConnectionLine,
                new DictionaryBlock(Color.FromArgb(255, 0, 0, 0), Color.Wheat, 10, "Linia łącząca",
                    "SbWinNew.Icons.ConnectionLine.jpg")
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
        public static Color ClockBackColor { get; set; } = Control.DefaultBackColor;

        public enum ClockType
        {
            Analog = 1,
            DigitalCountingDown = 2,
            DigitalCountingUp = 3
        };

        public static ClockType ClockChoosenType = ClockType.Analog;
        public static int ClockSize = 47;
        public static TimeSpan ClockTimeForExam = new TimeSpan(1, 45, 0);
        public static Color ClockColorHand = Color.Black;
        public static Color ClockColorScale = Color.Black;
        public static string ClockMessageWhenTimeIsOver = "Skończył ci się czas";
        public static Brush ClockPartOfTimeColor = Brushes.LightSalmon;

        #endregion
    }
}
