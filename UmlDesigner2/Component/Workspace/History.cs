using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.Workspace
{
    class History:List<List<HistoryItem>>
    {
        private int ID = -1;
        public void Add(MyAction myAction, List<MyBlock> block, List<MyLine> line)
        {
            ID++;
            if (!(ID < Count))
                base.Add(new List<HistoryItem>());
            else
                this[ID].Clear();
            for (int i = 0; i < block.Count; i++)
                base[0].Add(new HistoryItem(myAction, block[i]));
            for (int i = 0; i < line.Count; i++)
                base[0].Add(new HistoryItem(myAction, line[i]));
            for (int i = ID+1; i < Count; i++)//usuwanie krokow nastepnych (rozpoczecie nowej historii)
                this.RemoveAt(i);
        }
        public void Undo()
        {
            if (ID - 1 < 0) return;
            ID--;
            List<HistoryItem> temp = this[ID];
            switch (temp[0].MyActionType)
            {
                case MyAction.Add: break;
                case MyAction.Cut: break;
                case MyAction.Paste: break;
                case MyAction.DeleteBlock: break;
                case MyAction.DeleteLine: break;
                case MyAction.Move: break;
                case MyAction.Edit: break;
            }
        }

        public void UndoPaste()
        {
            
        }

        public void Redo()
        {
            if (ID + 1 >= this.Count) return;
            ID++;
            List<HistoryItem> temp = this[ID];
        }
    }

    class HistoryItem
    {
        public HistoryItem(MyAction myAction,MyLine line)
        {
            MyActionType = myAction;
            Line = line;
        }
        public HistoryItem(MyAction myAction, MyBlock block)
        {
            MyActionType = myAction;
            Block = block;
        }
        public MyAction MyActionType;
        public MyBlock Block=null;
        public MyLine Line=null;
    }

    public enum MyAction
    {
        Add,
        Cut,
        Paste,
        DeleteBlock,
        DeleteLine,
        Move,
        Edit
    }
}
