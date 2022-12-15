using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class LevelGenerator : CacheComponent
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Cell emptyCellPrefab;
    [SerializeField] private LevelDataObject levelDataObject;
    [SerializeField] private BlockPackObject blockPackObject;
    [SerializeField] private LevelData levelData;
    [SerializeField] private List<Cell> cellList;
    [SerializeField] private RectTransform CellPanel;
    [SerializeField] private int row;
    [SerializeField] private int column;
    [ShowInInspector] private Cell[,] cellMatrix = new Cell[4, 4];
    [ShowInInspector] private bool[,] cellSpot = new bool[4, 4];

    public void GenerateLevel(int level)
    {
        levelData = levelDataObject.GetLevel(level);
        blockPackObject = levelDataObject.BlockPackObject(levelData);
        row = levelData.shape.row;
        column = levelData.shape.col;
        CellPanel = UIManager.Instance.Canvas_Gameplay.CellPanel;
        cellMatrix = new Cell[row, column];
        UIManager.Instance.Canvas_Gameplay.SetConstraintCount(row);
        LayoutRebuilder.ForceRebuildLayoutImmediate(CellPanel);
        SetCellData();
        SetCellParent();
    }

    private void SetCellData()
    {
        foreach (CellData data in levelData.shape.datas)
        {
            Cell cell;
            if (data.spriteId == -1) 
            {
                cell = Instantiate(emptyCellPrefab);
            }
            else
            {
                cell = Instantiate(cellPrefab);
                cell.SetData(blockPackObject.GetSprite(data.spriteId), data);
            }
            cellList.Add(cell);
        }

        foreach (Cell cell in cellList)
        {
            cellMatrix[cell.CellData.rowIndex, cell.CellData.colIndex] = cell;
        }
    }

    private void SetCellParent()
    {
        
        foreach (Cell cell in cellList)
        {
            cell.Transform.SetParent(CellPanel);
        }
        //LayoutRebuilder.ForceRebuildLayoutImmediate(CellPanel);
    }

    public void GetCellIndex(Cell cell, out int x, out int y)
    {
        x = 0;
        y = 0;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (cellMatrix[i, j] == cell)
                {
                    x = i;
                    y = j;
                }
            }
        }
    }
}
