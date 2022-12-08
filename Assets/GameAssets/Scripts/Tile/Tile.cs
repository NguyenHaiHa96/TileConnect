using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class Tile : UIElement, IPointerUpHandler, IPointerDownHandler
{
    public Image ImgBG;
    public Image ImgIcon;
    public TileTheme Theme;
    public int ColumnIndex;
    public int RowIndex;
    public bool IsBarrier;
    public bool IsEmpty;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsEmpty) return;
        Vector2Int tileIndex = new Vector2Int();
        ConnectionController.Instance.TileCount++;
        if (ConnectionController.Instance.TileCount == 1)
        {
            tileIndex = LevelManager.Instance.LevelGenerator.GetTileIndex(this);
            ConnectionController.Instance.GetFirstTileIndex(tileIndex);
        }
        else
        {
            tileIndex = LevelManager.Instance.LevelGenerator.GetTileIndex(this);
            ConnectionController.Instance.GetSecondTileIndex(tileIndex);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsEmpty) return;
        if (ConnectionController.Instance.TileCount == 2)
        {
            ConnectionController.Instance.CheckTileConnection();
        }
    }

    public void IsEmptyTile()
    {
        IsEmpty = true; 
        ImgBG.gameObject.SetActive(false);
        ImgIcon.gameObject.SetActive(false);
    }
}

public enum TileID { } 


