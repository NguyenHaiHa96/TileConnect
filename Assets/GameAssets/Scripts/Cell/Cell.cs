using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : CacheComponent, IPointerUpHandler, IPointerDownHandler
{
    public Image CellSprite;
    public CellData CellData;

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void SetData(Sprite sprite, CellData data)
    {
        CellSprite.sprite = sprite;
        CellData = data;
    }
}
