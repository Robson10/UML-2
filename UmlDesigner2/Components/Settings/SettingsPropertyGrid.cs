using System.ComponentModel;
using System.Drawing;
using UmlDesigner2.Class;

namespace UmlDesigner2.Components.Settings
{
    class SettingsPropertyGrid
    {

        #region Bloki
        [Category("B. Startu")]
        [Description("Domyślny kolor tła bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor tła")]
        public Color StartBackColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Start].BackColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.Start].BackColor = value; }
        }
        [Category("B. Startu")]
        [Description("Domyślny kolor czcionki bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor czcionki")]
        public Color StartFontColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Start].FontColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.Start].FontColor = value; }
        }

        [Category("B. Końca")]
        [Description("Domyślny kolor tła bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor tła")]
        public Color EndBackColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.End].BackColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.End].BackColor = value; }
        }
        [Category("B. Końca")]
        [Description("Domyślny kolor czcionki bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor czcionki")]
        public Color EndFontColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.End].FontColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.End].FontColor = value; }
        }

        [Category("B. Wprowadzania")]
        [Description("Domyślny kolor tła bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor tła")]
        public Color InputBackColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Input].BackColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.Input].BackColor = value; }
        }
        [Category("B. Wprowadzania")]
        [Description("Domyślny kolor czcionki bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor czcionki")]
        public Color InputFontColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Input].FontColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.Input].FontColor = value; }
        }

        [Category("B. Wykonawczy")]
        [Description("Domyślny kolor tła bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor tła")]
        public Color ExecutionBackColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Execution].BackColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.Execution].BackColor = value; }
        }
        [Category("B. Wykonawczy")]
        [Description("Domyślny kolor czcionki bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor czcionki")]
        public Color ExecutionFontColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Execution].FontColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.Execution].FontColor = value; }
        }

        [Category("B. Warunkowy")]
        [Description("Domyślny kolor tła bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor tła")]
        public Color DecisionBackColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Decision].BackColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.Decision].BackColor = value; }
        }
        [Category("B. Warunkowy")]
        [Description("Domyślny kolor czcionki bloku.Działa tylko na nowe bloki")]
        [DisplayName("Kolor czcionki")]
        public Color DecisionFontColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Decision].FontColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.Decision].FontColor = value; }
        }

        [Category("Bloki")]
        [Description("Domyślny rozmiar czcionki dla wszystkich bloków. Działa tylko na nowe bloki")]
        [DisplayName("Rozmiar czcionki")]
        public int BlockFontSize
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Start].FontSize; }
            set
            {
                Helper.DefaultBlocksSettings[Helper.Shape.Start].FontSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.End].FontSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.Decision].FontSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.Input].FontSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.Execution].FontSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.ConnectionLine].FontSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.Nothing].FontSize = value;
            }
        }

        [Category("Bloki")]
        [Description("Domyślne wymiary początkowe wszystkich bloków. Działa tylko na nowe bloki")]
        [DisplayName("Wymiary")]
        public Size BlockSize
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.Start].BlockSize; }
            set
            {
                Helper.DefaultBlocksSettings[Helper.Shape.Start].BlockSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.End].BlockSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.Decision].BlockSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.Input].BlockSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.Execution].BlockSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.ConnectionLine].BlockSize = value;
                Helper.DefaultBlocksSettings[Helper.Shape.Nothing].BlockSize = value;
            }
        }

        [Category("Bloki")]
        [Description("Kolor zaznaczonego bloku")]
        [DisplayName("Kolor zaznaczenia")]
        public Color DefaultSelectionColor
        {
            get { return Helper.DefaultSelectionColor; }
            set { Helper.DefaultSelectionColor = value; }
        }
        #endregion

        #region Linie
        [Category("Linie")]
        [Description("Kolor linii prawdy. Nie działa na istniejące obiekty")]
        [DisplayName("Kolor linii prawdy")]
        public Color TrueLineBackColor
        {
            get { return Helper.TrueLineBackColor; }
            set { Helper.TrueLineBackColor = value; }
        }
        [Category("Linie")]
        [Description("Kolor linii fałszu. Nie działa na istniejące obiekty")]
        [DisplayName("Kolor linii fałszu")]
        public Color FalseLineBackColor
        {
            get { return Helper.FalseLineBackColor; }
            set { Helper.FalseLineBackColor = value; }
        }
        [Category("Linie")]
        [Description("Kolor linii. Nie działa na istniejące obiekty")]
        [DisplayName("Kolor linii")]
        public Color LineBackColor
        {
            get { return Helper.DefaultBlocksSettings[Helper.Shape.ConnectionLine].BackColor; }
            set { Helper.DefaultBlocksSettings[Helper.Shape.ConnectionLine].BackColor = value; }
        }
        #endregion

        #region Zegar
        [Category("Zegar")]
        [Description("Kolor wskazówek zegara")]
        [DisplayName("Kolor wskazówek")]
        public Color ClockColorHand
        {
            get { return Helper.ClockColorHand; }
            set { Helper.ClockColorHand = value; }
        }
        [Category("Zegar")]
        [Description("Kolor podziałek zegara")]
        [DisplayName("Kolor podziałki")]
        public Color ClockColorScale
        {
            get { return Helper.ClockColorScale; }
            set { Helper.ClockColorScale = value; }
        }
        [Category("Zegar")]
        [Description("Kolor wycinka koła na zegarze jaki jest przeznaczony na egzamin")]
        [DisplayName("Kolor egzaminu")]
        public Color ClockPartOfTimeColor
        {
            get { return (Helper.ClockPartOfTimeColor as SolidBrush).Color; }
            set { Helper.ClockPartOfTimeColor =new SolidBrush(value); }
        }
        #endregion

        #region Gumki
        [Category("Gumki")]
        [Description("Kolor gumek zmieniających rozmiar bloków")]
        [DisplayName("Kolor gumek")]
        public Color RubbersColor
        {
            get { return Helper.RubberColor; }
            set { Helper.RubberColor = value; }
        }
        [Category("Gumki")]
        [Description("Rozmiar gumek zmieniających rozmiar bloków")]
        [DisplayName("Rozmiar gumek")]
        public Size RubberSize
        {
            get { return Helper.RubberSize; }
            set { Helper.RubberSize = value; }
        }
        #endregion
    }
}
