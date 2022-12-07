using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class Tile : UIElement, IPointerUpHandler, IPointerDownHandler
{
    public TileTheme Theme;

    public void OnPointerDown(PointerEventData eventData)
    {
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
        if (ConnectionController.Instance.TileCount == 2)
        {
            ConnectionController.Instance.CheckTileConnection();
        }
    }
}

public enum TileID { } 


