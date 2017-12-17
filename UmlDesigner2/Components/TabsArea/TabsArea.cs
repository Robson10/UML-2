using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.TabBlocks;

namespace UmlDesigner2.Component.TabsArea
{
    class TabsArea:TabControl
    {
        private BlocksTab _blocksTab = new BlocksTab();

        /// <summary>
        /// Konstruktor ustawiający wygląd kontrolki oraz dodający zakładkę z blokami oraz schematami
        /// </summary>
        public TabsArea()
        {
            Alignment = TabAlignment.Left;
            TabPages.Add(_blocksTab);
            Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top|AnchorStyles.Right);
        }


        /// <summary>
        /// Eventhandler do pojedynczego kliknięcia na element listy bloków
        /// </summary>
        public event EventHandler BlocksListItemClick
        {
            add { _blocksTab.ListItemClick += value; }
            remove { _blocksTab.ListItemClick += value; }
        }

        /// <summary>
        /// Eventhandler do podwójnego kliknięcia na element listy bloków
        /// </summary>
        public event EventHandler BlocksListItemDoubleClick
        {
            add { _blocksTab.ListItemDoubleClick += value; }
            remove { _blocksTab.ListItemDoubleClick += value; }
        }

        /// <summary>
        /// Zdarzenie ustawiające połozenie i rozmiar kontrolki parajej dodaniu
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (Parent != null)
            {
                Location = new System.Drawing.Point(0, 0);
                Size = new System.Drawing.Size(Parent.Width, Parent.Height);
            }
        }

        /// <summary>
        /// Usuniecie domyślnie tworzonych zakładek przez Form1.designer.cs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control.Text.Contains("tabPage"))
            {
                e.Control.Dispose();
            }
        }
    }
}
