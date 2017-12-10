using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.Workspace
{
    class History
    {
        //textbox nie ma ctrlZ/Y wiec zaskakuje moja historia - nalezy jednak edycje tekstu takze dorzucic do mojej historii
        //todo move, oraz okno propertisów 

        //łapać historie edycji bloku z poziomu properties po zmianie aktywnego bloku lub zamkniecie properties
        //lub dodac event przy textboxach aby nie zapisywać zmiany kazdej literki w TB
        private static Stack<List<HistoryItem>> cofnij = new Stack<List<HistoryItem>>();
        private static Stack<List<HistoryItem>>  doPrzodu = new Stack<List<HistoryItem>>();
        public  static List<HistoryItem> temporary = new List<HistoryItem>();
        public static void Clear()
        {
            cofnij.Clear();
            doPrzodu.Clear();
        }
        public static void Push(List<HistoryItem> value)
        {
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

        public static bool compareWithLastPush(List<HistoryItem> value,MyAction action)
        {
            if (cofnij.Count > 0 && value.Count > 0)
            {
                if (value[0].MyActionType == MyAction.EditSize)
                {
                    var temp1 = cofnij.ToList()[1];
                    for (int i = 0; i < temp1.Count; i++)
                    {
                        try
                        {
                            if (value[i].Block.Rect != temp1[i].Block.Rect) return false;
                        }
                        catch
                        {
                        }
                    }
                }
                //if (value[0].MyActionType == MyAction.Move)
                //{
                //    var temp1 = cofnij.ToList()[1];
                //    for (int i = 0; i < temp1.Count; i++)
                //    {
                //        if (value[i].Block.Rect != temp1[i].Block.Rect) return false;
                //    }
                //}
            }
                return true;
        }

        public static void DeleteLast()
        {
            if (cofnij.Count > 0)
                cofnij.Pop();
        }
    }

    public class HistoryItem
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
        Delete,
        Edit,
        EditSize,
        Move
    }
}
