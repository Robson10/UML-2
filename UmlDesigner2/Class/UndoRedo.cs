using System.Collections.Generic;
using System.Linq;
using SbWinNew.Components.Workspace;

namespace SbWinNew.Class
{
    class UndoRedo
    {
        //textbox nie ma ctrlZ/Y wiec zaskakuje moja historia - nalezy jednak edycje tekstu takze dorzucic do mojej historii
        //todo move, oraz okno propertisów 

        //łapać historie edycji bloku z poziomu properties po zmianie aktywnego bloku lub zamkniecie properties
        //lub dodac event przy textboxach aby nie zapisywać zmiany kazdej literki w TB
        private static Stack<List<UndoRedoItem>> cofnij = new Stack<List<UndoRedoItem>>();
        private static Stack<List<UndoRedoItem>>  doPrzodu = new Stack<List<UndoRedoItem>>();
        public static void Clear()
        {
            cofnij.Clear();
            doPrzodu.Clear();
        }
        public static void Push(List<UndoRedoItem> value)
        {
            cofnij.Push(value);
            doPrzodu.Clear();
        }
        public static List<UndoRedoItem> Undo()
        {
            if (cofnij.Count == 0) return null;
            var temp = cofnij.Pop();
            doPrzodu.Push(temp);
            return temp;
        }

        public static List<UndoRedoItem> Redo()
        {
            if (doPrzodu.Count == 0) return null;
            var temp=doPrzodu.Pop();
            cofnij.Push(temp);
            return temp;
        }

        public static bool compareWithLastPush(List<UndoRedoItem> value,MyAction action)
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
            }
                return true;
        }

        public static void DeleteLast()
        {
            if (cofnij.Count > 0)
                cofnij.Pop();
        }
    }


}
