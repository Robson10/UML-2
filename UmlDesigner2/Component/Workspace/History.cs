using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.Workspace
{
    class TestHistory
    {
        //łapać historie edycji bloku z poziomu properties po zmianie aktywnego bloku lub zamkniecie properties
        //lub dodac event przy textboxach aby nie zapisywać zmiany kazdej literki w TB
        private static Stack<List<HistoryItem>> cofnij = new Stack<List<HistoryItem>>();
        private static Stack<List<HistoryItem>>  doPrzodu = new Stack<List<HistoryItem>>();

        public static void Clear()
        {
            cofnij.Clear();
            doPrzodu.Clear();
        }
        public static void Push(List<HistoryItem> value)
        {
            for (int i = 0; i < value.Count; i++)
            {
                value[i].Block.Rect =
                    new System.Drawing.Rectangle(value[i].Block.Rect.Location, value[i].Block.Rect.Size);
            }
            cofnij.Push(value);
            doPrzodu.Clear();
        }
        public static List<HistoryItem> Cofnij()
        {
            if (cofnij.Count == 0) return null;
            var temp = cofnij.Pop();
            doPrzodu.Push(temp);
            return temp;
        }

        public static List<HistoryItem> DoPrzodu()
        {
            if (doPrzodu.Count == 0) return null;
            var temp=doPrzodu.Pop();
            cofnij.Push(temp);
            return temp;
        }

        public static List<HistoryItem> ConvertToHistoryItems(List<MyBlock> value,MyAction action)
        {
            List<HistoryItem> temp = new List<HistoryItem>();
            for (int i = 0; i < value.Count; i++)
            {
                temp.Add(new HistoryItem(action, new MyBlock(){Shape=value[i].Shape, Rect= value[i].Rect,ID=value[i].ID}));
            }
            return temp;
        }
        public static List<HistoryItem> ConvertToHistoryItems(List<MyLine> value, MyAction action)
        {
            List<HistoryItem> temp = new List<HistoryItem>();
            for (int i = 0; i < value.Count; i++)
            {
                temp.Add(new HistoryItem(action, value[i]));
            }
            return temp;
        }

        public static bool compareWithLastPush(List<HistoryItem> value)
        {
            if (cofnij.Count > 0 && value.Count > 0)
            {
                
                if (value[0].MyActionType == MyAction.EditSize)
                {
                    var temp = cofnij.ToList()[1];
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (value[i].Block.Rect != temp[i].Block.Rect) return false;
                    }
                }
            }
                return true;
        }

        public static void DeleteLast()
        {
            if (cofnij.Count > 0)
                cofnij.Pop();
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
        PasteCopy,
        PasteCut,
        DeleteBlock,
        DeleteLine,
        Edit,
        EditSize
    }
}
