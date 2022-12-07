using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Sirenix.OdinInspector;

public class LevelGenerator : CacheComponent
{
    private readonly string THEME_FISH_PATH = "Tile/Theme/Fish/";

    public RectTransform PanelTiles;
    public List<Tile> TilePrefabList;
    [FormerlySerializedAs("NumberOfColumns")] public int NumberOfColumns;
    [FormerlySerializedAs("NumberOfRows")] public int NumberOfRows;
    [ShowInInspector] public Tile[,] TileArray = new Tile[4, 4];
    [ShowInInspector] public bool[,] TileSpot = new bool[4, 4];
    [ShowInInspector] private List<TileIndex> TileIndexList = new List<TileIndex>();
    private string path;
    private int prefabIndex;

    public override void OnInit()
    {
        PanelTiles = UIManager.Instance.Canvas_Gameplay.PanelTiles;
        UIManager.Instance.Canvas_Gameplay.SetConstraintCount(NumberOfColumns);
    }

    public void GenerateLevel(TileTheme theme)
    {
        TileArray = new Tile[NumberOfColumns, NumberOfRows];
        TileSpot = new bool[NumberOfColumns + 2, NumberOfRows + 2];
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
        for (int i = 0; i < NumberOfColumns; i++)
        {
            for (int j = 0; j < NumberOfRows; j++)
            {
                TileIndex tileSpot = new TileIndex(i, j);
                TileIndexList.Add(tileSpot);
            }
        }

        while (TileIndexList.Count > 0)
        {
            prefabIndex = Random.Range(0, TilePrefabList.Count);
            for (int i = 0; i < 2; i++)
            {
                int index = Random.Range(0, TileIndexList.Count);
                TileIndex tileSpot = TileIndexList[index];
                Tile tile = Instantiate(TilePrefabList[prefabIndex]);
                TileArray[tileSpot.ColumnIndex, tileSpot.RowIndex] = tile;
                TileSpot[tileSpot.ColumnIndex + 1, tileSpot.RowIndex + 1] = true;
                TileIndexList.RemoveAt(index);
            }
        }

        SetTileParent();
    }

    private void SetTileParent()
    {
        for (int i = 0; i < NumberOfColumns; i++)
        {
            for (int j = 0; j < NumberOfRows; j++)
            {
                this.LogMsg(i);
                this.LogMsg(j);
                TileArray[i, j].Transform.SetParent(PanelTiles);
            }
        }
    }

    public Vector2Int GetTileIndex(Tile tile)
    {
        for (int i = 0; i < NumberOfColumns; i++)
        {
            for (int j = 0; j < NumberOfRows; j++)
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
    public int ColumnIndex;
    public int RowIndex;

    public TileIndex(int columnIndex, int rowIndex)
    {
        ColumnIndex = columnIndex;
        RowIndex = rowIndex;
    }
}




