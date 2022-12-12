using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using CodeMonkey.Utils;

[System.Serializable]
public class GameGrid<TGridObject> 
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int ValueX;
        public int ValueY;  
    }

    public Transform Parent;
    [ShowInInspector] public TGridObject[,] Mattrix;
    public float CellSize;
    public int Row;
    public int Column;

    public GameGrid(int row, int column, float cellSize, Transform parent, Func<TGridObject> createObject)
    {
        Row = row;
        Column = column;
        CellSize = cellSize;
        Mattrix = new TGridObject[row, column];

        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < column; y++)
            {
                TGridObject gridObject = createObject();
                Mattrix[x, y] = gridObject;
                //UtilsClass.CreateWorldText(Mattrix[x, y].ToString(), parent, GetWordPosition(x,y), 20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
            }
        }
    }

    public GameGrid(int row, int column, Func<GameGrid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        Row = row;
        Column = column;
        Mattrix = new TGridObject[row, column];

        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < column; y++)
            {
                Mattrix[x, y] = createGridObject(this, x, y);
                //UtilsClass.CreateWorldText(Mattrix[x, y].ToString(), parent, GetWordPosition(x,y), 20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
            }
        }
    }

    private Vector3 GetWordPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize;
    }

    private void GetXYValue(Vector3 wordPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(wordPosition.x / CellSize);
        y = Mathf.FloorToInt(wordPosition.y / CellSize);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y <= 0 && x < Row && y < Column)
        {
            return Mattrix[x, y];
        }
        else return default(TGridObject);
    }

    public void SetGridObjectValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && y <= 0 && x < Row && y < Column)
        {
            Mattrix[x, y] = value;
        }
    }

    public void SetGridObjectValue(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXYValue(worldPosition, out x, out y);
        SetGridObjectValue(x, y, value);  
    }

}
