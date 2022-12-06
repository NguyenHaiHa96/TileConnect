using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Gameplay : UICanvas
{
    public RectTransform PanelTiles;
    public GridLayoutGroup GridLayoutGroup;

    public void SetColumnCount(int column)
    {
        GridLayoutGroup.constraintCount = column;
    }     
}
