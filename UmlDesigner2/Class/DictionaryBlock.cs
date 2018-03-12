using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SbWinNew.Class
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
        public DictionaryBlock(Color backColor, Color fontColor, int fontSize, string label, string imgPath)
        {
            BackColor = backColor;
            FontColor = fontColor;
            FontSize = fontSize;
            Label = label;
            ImgPath = imgPath;
        }
    }
}
