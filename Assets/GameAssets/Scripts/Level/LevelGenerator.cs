using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class LevelGenerator : CacheComponent
{
    [SerializeField] private Transform cellContainer;
    [SerializeField] private Transform starContainer;
    [SerializeField] private RectTransform CellPanel;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Cell emptyCellPrefab;
    [SerializeField] private LevelDataObject levelDataObject;
    [SerializeField] private BlockPackObject blockPackObject;
    [SerializeField] private LevelData levelData;
    [SerializeField] private List<Cell> cellList;
    [SerializeField] private int row;
    [SerializeField] private int column;
    [ShowInInspector] private Cell[,] cellMatrix = new Cell[4, 4];
    [ShowInInspector] private bool[,] cellSpot = new bool[4, 4];

    private float posX;
    private float posY;
    private float spacing = 1.5f;

    public Transform StarContainer { get => starContainer; set => starContainer = value; }

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
        SetCellPosition();
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

    private void SetCellPosition()
    {
        //cellContainer.LocalScale = new Vector3(column, column, cellContainer.LocalScale.y);
        float xPos = 0;
        float yPos = 0f;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                cellMatrix[i, j].WorldPosition = new Vector3(xPos, yPos, cellMatrix[i, j].WorldPosition.z);
                xPos += spacing;
                posX = xPos;
            }
            xPos = 0;
            yPos -= spacing;
        }
        posY = yPos;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                cellMatrix[i, j].Transform.SetParent(cellContainer);
            }
        }
        SetCameraPosition();
    }

    private void SetCameraPosition()
    {
        posX = (posX / 2f) - (spacing / 2f);
        posY = (posY / 2f) + (spacing / 2f);
        CameraController.Instance.SetCameraFocusPoint(new Vector3(posX, posY));
        CameraController.Instance.SetStartPosition();
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

    public Vector3 GetCellPositionByCellData(CellData cellData)
    {
        for (int i = 0; i < cellList.Count; i++)
        {
            if (cellList[i].CellData == cellData)
            {
                return new Vector3(cellList[i].WorldPosition.x, cellList[i].WorldPosition.y, -1);
            }
        }
        return Vector3.zero;
    }
}
