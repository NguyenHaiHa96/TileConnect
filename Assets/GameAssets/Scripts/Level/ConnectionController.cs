using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController : Singleton<ConnectionController>
{
    public LineRenderer LineRenderer;
    public LineConnect LineConnect;
    public Vector2Int FirstTilePosition;
    public Vector2Int SecondTilePosition;
    public int TileCount;

    public void GetFirstTileIndex(Vector2Int index)
    {
        FirstTilePosition = index;
    }

    public void GetSecondTileIndex(Vector2Int index)
    {
        SecondTilePosition = index;
    }

    private bool CheckLineX(int y1, int y2, int x)
    {
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);
        for (int y = min; y < max; y++)
        {
            if (LevelManager.Instance.LevelGenerator.TileSpot[x, y] == false) 
            {
                return true;
            } 
        }
        return false;
    }

    private bool CheckLineY(int x1, int x2, int y)
    {
        int min = Mathf.Min(x1, x2);
        int max = Mathf.Max(x1, x2);
        for (int x = min; x <= max; x++)
        {
           
            if (LevelManager.Instance.LevelGenerator.TileSpot[x, y] == false)
            {
                return true;
            }            
        }
        return true;
    }

    private int CheckRectX(Vector2Int index1, Vector2Int index2)
    {
        Vector2Int indexMinY = index1;
        Vector2Int indexMaxY = index2;

        if (indexMinY.y > indexMaxY.y)
        {
            indexMinY = index2;
            indexMaxY = index1;
        }

        for (int y = indexMinY.y; y < indexMaxY.y; y++)
        {
            if (CheckLineX(indexMinY.y, y, indexMinY.x)
                && CheckLineY(indexMinY.x, indexMinY.x, y)
                && CheckLineX(y, indexMaxY.y, indexMaxY.x))
            {
                return y;
            }
        }
        return -1;
    }

    public LineConnect CheckTileConnection()
    {
        TileCount = 0;
        if (FirstTilePosition.x == SecondTilePosition.x)
        {
            if (!CheckLineX(FirstTilePosition.y, SecondTilePosition.y, FirstTilePosition.x))
            {
                return new LineConnect(FirstTilePosition, SecondTilePosition);
            }
            else return null;
        }
        return null;
    }
}

[System.Serializable]
public class LineConnect
{
    public Vector2Int Point1;
    public Vector2Int Point2;

    public LineConnect(Vector2Int point1, Vector2Int point2)
    {
        Point1 = point1;
        Point2 = point2;
    }
}
