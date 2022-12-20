using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : CacheComponent
{
    public SpriteRenderer ImgCell;
    public Image CellSprite;
    public CellData CellData;
    public bool IsEmpty;
    public bool IsBarrier;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsEmpty) return;
        if (ConnectionChecker.Instance.CellCount == 2)
        {
            ConnectionChecker.Instance.CheckCell();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    private void OnMouseDown()
    {
        if (IsEmpty) return;
        ConnectionChecker.Instance.CellCount++;
        if (ConnectionChecker.Instance.CellCount == 1)
        {
            ConnectionChecker.Instance.SetStartCellData(CellData);
        }
        else
        {
            ConnectionChecker.Instance.SetEndCellData(CellData);
        }
    }

    public void SetData(Sprite sprite, CellData data)
    {
        ImgCell.sprite = sprite;
        //CellSprite.sprite = sprite;
        CellData = data;
    }
}
