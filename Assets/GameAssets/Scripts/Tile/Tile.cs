using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : UIElement, IPointerUpHandler, IPointerDownHandler
{
    public SpriteRenderer SpriteRenderer;
    public Image ImgBG;
    public Image ImgIcon;
    public TileTheme Theme;
    public int ID;
    public int RowIndex;
    public int ColumnIndex;
    public bool IsBarrier;
    public bool IsEmpty;

    public float SpriteRectWidth { get => SpriteRenderer.sprite.rect.width; }
    public float SpriteRectHeight { get => SpriteRenderer.sprite.rect.height; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsEmpty) return;
        Vector2Int tileIndex;
        PathfindingController.Instance.TileCount++;
        if (PathfindingController.Instance.TileCount == 1)
        {
            tileIndex = LevelManager.Instance.LevelCreator.GetTileIndex(this);
            PathfindingController.Instance.GetFirstTile(this, tileIndex);
        }
        else
        {
            tileIndex = LevelManager.Instance.LevelCreator.GetTileIndex(this);
            PathfindingController.Instance.GetSecondTile(this, tileIndex);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsEmpty) return;
        if (PathfindingController.Instance.TileCount == 2)
        {
        if (PathfindingController.Instance.CheckTile())
            {
                PathfindingController.Instance.CheckTileConnection();
            }
        }    
    }

    public void IsEmptyTile()
    {
        IsEmpty = true; 
        ImgBG.gameObject.SetActive(false);
        ImgIcon.gameObject.SetActive(false);
    }

    public void SetEmptyTile()
    {
        IsEmpty = true;
        if (ImgBG != null && ImgIcon != null)
        {
            ImgBG.gameObject.SetActive(false);
            ImgIcon.gameObject.SetActive(false);
        }
    }
}

public enum TileID { } 


