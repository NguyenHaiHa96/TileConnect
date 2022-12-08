using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using Sirenix.OdinInspector;

public class LevelGenerator : CacheComponent
{
    private readonly string THEME_FISH_PATH = "Tile/Theme/Fish/";

    public Tile EmptyTilePrefab;
    public RectTransform PanelTiles;
    public List<Tile> TilePrefabList;
    [FormerlySerializedAs("NumberOfRows")] public int NumberOfRows;
    [FormerlySerializedAs("NumberOfColumns")] public int NumberOfColumns;
    [ShowInInspector] public Tile[,] TileArray = new Tile[4, 4];
    [ShowInInspector] public bool[,] TileSpot = new bool[4, 4];
    [ShowInInspector] private List<TileIndex> TileIndexList = new List<TileIndex>();
    private string path;
    private float timeDelay = 0.5f;
    private int prefabIndex;
    private int rows;
    private int columns;
    public override void OnInit()
    {
        columns = NumberOfColumns + 2;
        rows = NumberOfRows + 2;
        PanelTiles = UIManager.Instance.Canvas_Gameplay.PanelTiles;
        UIManager.Instance.Canvas_Gameplay.SetConstraintCount(rows);
    }

    public void GenerateLevel(TileTheme theme)
    {
        TileArray = new Tile[rows, columns];
        TileSpot = new bool[rows, columns];
        switch (theme)
        {
            case TileTheme.Fish:
                path = THEME_FISH_PATH;      
                break;
            case TileTheme.Insect:
                break;
            case TileTheme.Fruit:
                break;
            case TileTheme.Bird:
                break;
            case TileTheme.Dinosaur:
                break;
            default:
                break;
        }
        
        GenerateLevelByTheme();
    }

    private void GenerateLevelByTheme()
    {
        Tile[] tiles = Resources.LoadAll<Tile>(path);
        TilePrefabList = tiles.ToList();
        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < columns - 1; j++)
            {
                TileIndex tileSpot = new TileIndex(i, j);
                TileIndexList.Add(tileSpot);
            }
        }

        InstantiateTile();
        InstantiateEmptyTile();
        SetTileParent();
    }

    private void InstantiateTile()
    {
        while (TileIndexList.Count > 0)
        {
            prefabIndex = Random.Range(0, TilePrefabList.Count);
            for (int i = 0; i < 2; i++)
            {
                int index = Random.Range(0, TileIndexList.Count);
                TileIndex tileSpot = TileIndexList[index];
                Tile tile = Instantiate(TilePrefabList[prefabIndex]);
                TileArray[tileSpot.RowIndex, tileSpot.ColumnIndex] = tile;
                tile.RowIndex = tileSpot.RowIndex;
                tile.ColumnIndex = tileSpot.ColumnIndex;
                TileSpot[tileSpot.RowIndex, tileSpot.ColumnIndex] = true;
                TileIndexList.RemoveAt(index);
            }
        }
    }

    private void InstantiateEmptyTile()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (TileArray[i, j] == null)
                {
                    Tile emptyTile = Instantiate(EmptyTilePrefab);
                    TileArray[i, j] = emptyTile;
                    emptyTile.RowIndex = i;
                    emptyTile.ColumnIndex = j;
                }
            }
        }
    }

    private void SetTileParent()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (TileArray[i, j] != null)
                {
                    this.LogMsg(i);
                    this.LogMsg(j);
                    TileArray[i, j].Transform.SetParent(PanelTiles);
                }
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(PanelTiles);
    }

    public Vector2Int GetTileIndex(Tile tile)
    {
        for (int i = 1; i < NumberOfColumns; i++)
        {
            for (int j = 1; j < NumberOfRows; j++)
            {
                if (TileArray[i, j] == tile)
                {
                    this.LogMsg(j);
                    return new Vector2Int(i, j);
                }
            }
        }
        return new Vector2Int();
    }
}

[System.Serializable]
public class TileIndex
{
    public int RowIndex;
    public int ColumnIndex;

    public TileIndex(int rowIndex, int columnIndex)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
    }
}




