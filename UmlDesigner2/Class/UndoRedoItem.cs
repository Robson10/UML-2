using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SbWinNew.Components.Workspace;

namespace SbWinNew.Class
{
    public class UndoRedoItem
    {
        public UndoRedoItem(MyAction myAction, MyBlock block, MyLine line)
        {
            MyActionType = myAction;
            Block = block;
            Line = line;
        }
        public MyAction MyActionType;
        public MyBlock Block = null;
        public MyLine Line = null;
    }

}
