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
    [ShowInInspector] private Tile[,] TileArray = new Tile[8, 8];
    [ShowInInspector] private List<TileSpot> TileSpots = new List<TileSpot>();
    private string path;
    private int prefabIndex;

    public override void OnInit()
    {
        PanelTiles = UIManager.Instance.Canvas_Gameplay.PanelTiles;
        UIManager.Instance.Canvas_Gameplay.SetColumnCount(NumberOfColumns);
    }

    public void GenerateLevel(TileTheme theme)
    {
        TileArray = new Tile[NumberOfRows, NumberOfColumns];
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
        int totalTiles = NumberOfColumns * NumberOfRows;
        for (int i = 0; i < NumberOfColumns; i++)
        {
            for (int j = 0; j < NumberOfRows; j++)
            {
                TileSpot tileSpot = new TileSpot(i, j);
                TileSpots.Add(tileSpot);
            }
        }

        while (TileSpots.Count > 0)
        {
            prefabIndex = Random.Range(0, TilePrefabList.Count);
            for (int i = 0; i < 2; i++)
            {
                int index = Random.Range(0, TileSpots.Count);
                this.LogMsg(index);
                TileSpot tileSpot = TileSpots[index];
                Tile tile = Instantiate(TilePrefabList[prefabIndex]);
                TileArray[tileSpot.RowIndex, tileSpot.ColumnIndex] = tile;
                TileSpots.RemoveAt(index);
            }
        }

        SetTileParent();
    }

    private void SetTileParent()
    {
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                TileArray[i, j].Transform.SetParent(PanelTiles);
            }
        }
    }

    private List<int> GetListIndex(int number)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < number; i++)
        {
            list.Add(i);
        }
        return list;
    } 
}

[System.Serializable]
public class TileSpot
{
    public int ColumnIndex;
    public int RowIndex;

    public TileSpot(int columnIndex, int rowIndex)
    {
        ColumnIndex = columnIndex;
        RowIndex = rowIndex;
    }
}




