using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class ConnectionController : Singleton<ConnectionController>
{
    public RectTransform UILineRendererRectTransform;
    public UILineRenderer UILineRenderer;
    public List<LineConnect> LineConnectList;
    public List<Vector3> TilePositions;
    public Tile Tile1;
    public Tile Tile2;
    public Vector3 FirstTilePosition;
    public Vector3 SecondTilePosition;
    public Vector2Int FirstTileIndex;
    public Vector2Int SecondTileIndex;
    public float PanelSpacing;
    public int TileCount;

    private float timeDelay = 5f;

    public override void OnInit()
    {
        base.OnInit();
        UILineRenderer = UIManager.Instance.Canvas_Gameplay.UILineRenderer;
        PanelSpacing = UIManager.Instance.Canvas_Gameplay.Spacing;
    }

    public void GetFirstTile(Tile tile1, Vector3 position, Vector2Int index)
    {
        Tile1 = tile1;
        FirstTilePosition = position;
        FirstTileIndex = index;
    }

    public void GetSecondTileIndex(Tile tile2, Vector3 position, Vector2Int index)
    {
        Tile2 = tile2;
        SecondTilePosition = position;
        SecondTileIndex = index;
    }

    public bool CheckTile()
    {
        if (Tile1.Theme == Tile2.Theme && Tile1.ID == Tile2.ID)
        {
            return true;
        }
        else return false;
    }

    private void Swap(ref int indexMin, ref int indexMax, int index1, int index2)
    {
        indexMin = index1;
        indexMax = index2;
        if (indexMin > indexMax)
        {
            indexMin = index2;
            indexMax = index1;
        }
    }

    //Same row
    private bool CheckLineX(int y1, int y2, int x)
    {
        int min = Mathf.Min(y1, y2);
        int max = Mathf.Max(y1, y2);

        if (y1 == y2 - 1 || y1 == y2 + 1)
        {
            return true;
        }
        else
        {
            for (int y = min; y < max; y++)
            {
                if (LevelManager.Instance.LevelCreator.TileSpot[x, y] == false)
                {
                    return true;
                }
            }
        }   
        return false;
    }

    //Same column
    private bool CheckLineY(int x1, int x2, int y)
    {
        int min = Mathf.Min(x1, x2);
        int max = Mathf.Max(x1, x2);
        for (int x = min; x <= max; x++)
        {
            if (LevelManager.Instance.LevelCreator.TileSpot[x, y] == false)
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

        for (int y = indexMinY.y; y <= indexMaxY.y; y++)
        {
            if (CheckLineX(indexMinY.y, y, indexMinY.x)
                && CheckLineX(indexMinY.x, indexMaxY.x, y)
                && CheckLineX(y, indexMaxY.y, indexMaxY.x))
            {
                return y;
            }
        }
        return -1;
    }

    private int CheckRectY(Vector2Int index1, Vector2Int index2)
    {
        Vector2Int indexMinX = index1;
        Vector2Int indexMaxX = index2;

        if (indexMinX.y > indexMaxX.y)
        {
            indexMinX = index2;
            indexMaxX = index1;
        }

        for (int x = indexMinX.x; x <= indexMaxX.x; x++)
        {
            if (CheckLineY(indexMinX.x, x, indexMinX.y)
                && CheckLineX(indexMinX.y, indexMaxX.y, x)
                && CheckLineY(x, indexMaxX.x, indexMaxX.y))
            {
                return x;
            }
        }
        return -1;
    }

    public void CheckTileConnection()
    {
        TileCount = 0;
        if (FirstTileIndex.x == SecondTileIndex.x)
        {
            if (CheckLineX(FirstTileIndex.y, SecondTileIndex.y, FirstTileIndex.x))
            {
                LineConnect line = new LineConnect(FirstTileIndex, SecondTileIndex);
                LineConnectList.Add(line);

                int indexMin = 0;
                int indexMax = 0;
                Swap(ref indexMin, ref indexMax, FirstTileIndex.y, SecondTileIndex.y);

                for (int i = indexMin; i <= indexMax; i++)
                {
                    Vector3 tilePosition = LevelManager.Instance.LevelCreator.TileArray[FirstTileIndex.x, i].WorldPosition;
                    TilePositions.Add(tilePosition);
                    DrawLine();
                }
            }
            return;
        }

        if (FirstTileIndex.y == SecondTileIndex.y)
        {
            if (CheckLineY(FirstTileIndex.x, SecondTileIndex.x, FirstTileIndex.y))
            {
                LineConnect line = new LineConnect(FirstTileIndex, SecondTileIndex);
                LineConnectList.Add(line);

                int indexMin = 0;
                int indexMax = 0;
                Swap(ref indexMin, ref indexMax, FirstTileIndex.x, SecondTileIndex.x);

                for (int i = indexMin; i <= indexMax; i++)
                {
                    Vector3 tilePosition = LevelManager.Instance.LevelCreator.TileArray[i, FirstTileIndex.y].WorldPosition;
                    TilePositions.Add(tilePosition);
                    DrawLine();
                }
            }
            return;
        }

        int index = -1;

        if ((index = CheckRectX(FirstTileIndex, SecondTileIndex)) != -1) 
        {
            this.LogMsg("Check rect x");
            Vector2Int start = new Vector2Int(FirstTileIndex.x, index);
            Vector2Int end = new Vector2Int(SecondTileIndex.x, index);
            LineConnect line = new LineConnect(start, end);
            GetTilePosition(line);
            DrawLine();
            return;
        }
        
        if ((index = CheckRectY(FirstTileIndex, SecondTileIndex)) != -1)
        {
            this.LogMsg("Check rect y");
            Vector2Int start = new Vector2Int(index, FirstTileIndex.y);
            Vector2Int end = new Vector2Int(index, SecondTileIndex.y);
            LineConnect line = new LineConnect(start, end);
            GetTilePosition(line);
            DrawLine();
            return;
        }

        
    }

    private void GetTilePosition(LineConnect line)
    {
        Vector3 position1 = LevelManager.Instance.LevelCreator.GetTilePosition(line.Point1);
        Vector3 position2 = LevelManager.Instance.LevelCreator.GetTilePosition(line.Point2);
        TilePositions.AddElements(position1, position2);
    }

    private void DrawLine()
    {
        UILineRenderer.Points = new Vector2[TilePositions.Count];
        for (int i = 0; i < UILineRenderer.Points.Length; i++)
        {
            UILineRenderer.Points[i] = TilePositions[i];
        }
        StartCoroutine(ResetTile());
    }

    private IEnumerator ResetTile()
    {
        yield return Helper.GetWaitForSeconds(timeDelay);
        UILineRenderer.gameObject.SetActive(false);
        LevelManager.Instance.LevelCreator.SetEmptyTile(FirstTileIndex);
        LevelManager.Instance.LevelCreator.SetEmptyTile(SecondTileIndex);
        UILineRenderer.Points = new Vector2[0];
        LineConnectList.Clear();
        TilePositions.Clear();
        Tile1 = null; 
        Tile2 = null;
        FirstTilePosition = Vector3.zero;
        SecondTilePosition = Vector3.zero;
        FirstTileIndex = new Vector2Int();
        SecondTileIndex = new Vector2Int();
        UILineRenderer.gameObject.SetActive(true);
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
