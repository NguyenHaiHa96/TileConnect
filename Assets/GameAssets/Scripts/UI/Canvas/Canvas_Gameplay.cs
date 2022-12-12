using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Canvas_Gameplay : UICanvas
{
    public RectTransform PanelTiles;
    public RectTransform UILineRendererRectTransform;
    public GridLayoutGroup GridLayoutGroup;
    public UILineRenderer UILineRenderer;
    public float Spacing;

    public override void OnOpen()
    {
        base.OnOpen();
        Spacing = GridLayoutGroup.spacing.x;
    }

    public void SetConstraintCount(int constraint)
    {
        GridLayoutGroup.constraintCount = constraint;
    }     
}
