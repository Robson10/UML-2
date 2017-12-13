﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    partial class Canvas
    {
        //zlapanie usuniecia linii
        public void Undo()
        {
                    bool showProperties = false;
                    var temp = History.Cofnij();
                    temp = (temp?[0].MyActionType == MyAction.EditSize ||
                        temp?[0].MyActionType == MyAction.Move ||
                        temp?[0].MyActionType == MyAction.Edit) ? History.Cofnij() : temp;//podwojny pop na EditSize.NWM czemu
                    if (temp == null) return;
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Block != null)//blok
                {
                    if (temp[i].MyActionType == MyAction.Add)//został dodany wiec usuń blok
                    {
                        CanvObj.MyDeleteByID(temp[i].Block.ID);
                    }
                    else if (temp[i].MyActionType == MyAction.EditSize)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Rect = temp[i].Block.Rect;
                    }
                    else if (temp[i].MyActionType == MyAction.Cut)//został dodany wiec usuń blok
                    {
                        CanvObj.Add(temp[i].Block);//wykorzystujemy metode domyslną ponieważ CanvObj.MyAdd zmieniłaby ID bloku
                    }
                    else if (temp[i].MyActionType == MyAction.Delete)
                    {
                        CanvObj.Add(temp[i].Block);
                    }
                    else if (temp[i].MyActionType == MyAction.Move)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Rect = temp[i].Block.Rect;
                    }
                    else if (temp[i].MyActionType == MyAction.Edit)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Label = temp[i].Block.Label;
                        CanvObj[index].AutoResize = temp[i].Block.AutoResize;
                        CanvObj[index].BackColor = temp[i].Block.BackColor;
                        CanvObj[index].BackColorStorage = temp[i].Block.BackColorStorage;
                        CanvObj[index].Code = temp[i].Block.Code;
                        CanvObj[index].FontColor = temp[i].Block.FontColor;
                        CanvObj[index].FontSize = temp[i].Block.FontSize;
                        CanvObj[index].IsLocked = temp[i].Block.IsLocked;
                        CanvObj[index].IsSelected = temp[i].Block.IsSelected;
                        CanvObj[index].Shape = temp[i].Block.Shape;
                        CanvObj[index].Rect = temp[i].Block.Rect;
                        showProperties = true;
                    }
                }
                else if (temp[i].Line != null)
                {
                    if (temp[i].MyActionType == MyAction.Add)//został dodany wiec usuń blok
                    {
                        CanvLines.MyRemove(temp[i].Line.BeginId);
                    }
                    //else if (temp[i].MyActionType == MyAction.EditSize)
                    //{
                    //    //var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                    //    //CanvObj[index].Rect = temp[i].Block.Rect;
                    //}
                    else if (temp[i].MyActionType == MyAction.Cut)//został dodany wiec usuń blok
                    {
                        //CanvObj.Add(temp[i].Block);//wykorzystujemy metode domyslną ponieważ CanvObj.MyAdd zmieniłaby ID bloku
                    }
                    else if (temp[i].MyActionType == MyAction.Delete)
                    {
                        CanvLines.Add(temp[i].Line);
                    }
                    //else if (temp[i].MyActionType == MyAction.Move)
                    //{
                    //    //var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                    //    //CanvObj[index].Rect = temp[i].Block.Rect;
                    //}
                    //else if (temp[i].MyActionType == MyAction.Edit)
                    //{
                    //    //var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                    //    //CanvObj[index].Label = temp[i].Block.Label;
                    //    //CanvObj[index].AutoResize = temp[i].Block.AutoResize;
                    //    //CanvObj[index].BackColor = temp[i].Block.BackColor;
                    //    //CanvObj[index].BackColorStorage = temp[i].Block.BackColorStorage;
                    //    //CanvObj[index].Code = temp[i].Block.Code;
                    //    //CanvObj[index].FontColor = temp[i].Block.FontColor;
                    //    //CanvObj[index].FontSize = temp[i].Block.FontSize;
                    //    //CanvObj[index].IsLocked = temp[i].Block.IsLocked;
                    //    //CanvObj[index].IsSelected = temp[i].Block.IsSelected;
                    //    //CanvObj[index].Shape = temp[i].Block.Shape;
                    //    //CanvObj[index].Rect = temp[i].Block.Rect;
                    //    //showProperties = true;
                    //}
                }
            }
            CanvLines.MyUpdate(ref CanvObj);
            _rubbers.MyHideRubbers();
            if (!showProperties)
            {
                OnHideBlockProperties();
            }
            else
            { ShowProperties(); }
            Invalidate();
        }
        //zmiana rozmiaru nwm czemu musi zczytać 2 razy stos
        public void Redo()
        {
            bool showProperties = false;
            var temp = History.DoPrzodu();
            temp = (
                temp?[0].MyActionType == MyAction.EditSize ||
                temp?[0].MyActionType == MyAction.Move ||
                temp?[0].MyActionType == MyAction.Edit) ? History.DoPrzodu() : temp;
            if (temp == null) return;
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Block != null)//blok
                {
                    if (temp[i].MyActionType == MyAction.Add)//został dodany wiec usuń blok
                    {
                        CanvObj.Add(temp[i].Block);//wykorzystujemy metode domyslną ponieważ CanvObj.MyAdd zmieniłaby ID bloku
                    }
                    else if (temp[i].MyActionType == MyAction.EditSize)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Rect = temp[i].Block.Rect;
                    }
                    else if (temp[i].MyActionType == MyAction.Cut)
                    {
                        CanvObj.MyDeleteByID(temp[i].Block.ID);
                    }
                    else if (temp[i].MyActionType == MyAction.Delete)
                    {
                        CanvObj.MyDeleteByID(temp[i].Block.ID);
                    }
                    else if (temp[i].MyActionType == MyAction.Move)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Rect = temp[i].Block.Rect;
                    }
                    else if (temp[i].MyActionType == MyAction.Edit)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Label = temp[i].Block.Label;
                        CanvObj[index].AutoResize = temp[i].Block.AutoResize;
                        CanvObj[index].BackColor = temp[i].Block.BackColor;
                        CanvObj[index].BackColorStorage = temp[i].Block.BackColorStorage;
                        CanvObj[index].Code = temp[i].Block.Code;
                        CanvObj[index].FontColor = temp[i].Block.FontColor;
                        CanvObj[index].FontSize = temp[i].Block.FontSize;
                        CanvObj[index].IsLocked = temp[i].Block.IsLocked;
                        CanvObj[index].IsSelected = temp[i].Block.IsSelected;
                        CanvObj[index].Shape = temp[i].Block.Shape;
                        CanvObj[index].Rect = temp[i].Block.Rect;
                        showProperties = true;
                    }
                }
                else if (temp[i].Line != null)
                {
                    if (temp[i].MyActionType == MyAction.Add)//został dodany wiec usuń blok
                    {
                        CanvLines.Add(temp[i].Line);
                    }
                    //else if (temp[i].MyActionType == MyAction.EditSize)
                    //{
                    //    //var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                    //    //CanvObj[index].Rect = temp[i].Block.Rect;
                    //}
                    else if (temp[i].MyActionType == MyAction.Cut)//został dodany wiec usuń blok
                    {
                        //CanvObj.Add(temp[i].Block);//wykorzystujemy metode domyslną ponieważ CanvObj.MyAdd zmieniłaby ID bloku
                    }
                    else if (temp[i].MyActionType == MyAction.Delete)
                    {
                        CanvLines.MyRemove(temp[i].Line.BeginId, temp[i].Line.EndId);
                    }
                    //else if (temp[i].MyActionType == MyAction.Move)
                    //{
                    //    //var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                    //    //CanvObj[index].Rect = temp[i].Block.Rect;
                    //}
                    //else if (temp[i].MyActionType == MyAction.Edit)
                    //{
                    //    //var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                    //    //CanvObj[index].Label = temp[i].Block.Label;
                    //    //CanvObj[index].AutoResize = temp[i].Block.AutoResize;
                    //    //CanvObj[index].BackColor = temp[i].Block.BackColor;
                    //    //CanvObj[index].BackColorStorage = temp[i].Block.BackColorStorage;
                    //    //CanvObj[index].Code = temp[i].Block.Code;
                    //    //CanvObj[index].FontColor = temp[i].Block.FontColor;
                    //    //CanvObj[index].FontSize = temp[i].Block.FontSize;
                    //    //CanvObj[index].IsLocked = temp[i].Block.IsLocked;
                    //    //CanvObj[index].IsSelected = temp[i].Block.IsSelected;
                    //    //CanvObj[index].Shape = temp[i].Block.Shape;
                    //    //CanvObj[index].Rect = temp[i].Block.Rect;
                    //    //showProperties = true;
                    //}
                }
            }
            CanvLines.MyUpdate(ref CanvObj);
            if (!showProperties)
            {
                OnHideBlockProperties();
            }
            else
            { ShowProperties(); }
            Invalidate();
        }

        private void AddLinesToHistoryList(ref List<HistoryItem> ListHistoryItem)
        {
            for (int i = 0; i < CanvObj.Count; i++)
            {
                if (CanvObj[i].IsSelected)
                {
                    var temp = CanvLines.GetLineByID(CanvObj[i].ID);
                    if (temp != null)
                    {
                        for (int j = 0; j < temp.Count; j++)
                        {
                            ListHistoryItem.Add(new HistoryItem(MyAction.Delete, null, temp[j]));
                        }

                    }
                }
            }
        }
    }
}
