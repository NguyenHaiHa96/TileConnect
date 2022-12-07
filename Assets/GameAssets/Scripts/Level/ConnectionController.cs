using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController : Singleton<ConnectionController>
{
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
