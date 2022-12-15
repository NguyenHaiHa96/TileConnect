using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : Singleton<PathfindingController>   
{
    public Pathfinding Pathfinding;
    public Tile Tile1;
    public Tile Tile2;
    public PathNode Node1;
    public PathNode Node2;
    public Vector2Int FirstTileIndex;
    public Vector2Int SecondTileIndex;
    public int TileCount;

    public override void OnInit()
    {
        base.OnInit();
        int row = LevelManager.Instance.LevelCreator.GetNumberOfRows();
        int column = LevelManager.Instance.LevelCreator.GetNumberOfColumns();
        Pathfinding = new Pathfinding(row, column);
    }

    public void GetFirstTile(Tile tile1, Vector2Int index)
    {
        Tile1 = tile1;
        FirstTileIndex = index;
    }
    
    public void GetSecondTile(Tile tile1, Vector2Int index)
    {
        Tile1 = tile1;
        FirstTileIndex = index;
    }

    public bool CheckTile()
    {
        if (Tile1.Theme == Tile2.Theme && Tile1.ID == Tile2.ID)
        {
            return true;
        }
        else return false;
    }

    public void CheckTileConnection()
    {
        List<PathNode> path = Pathfinding.FindPath(FirstTileIndex.x, FirstTileIndex.y, SecondTileIndex.x, SecondTileIndex.y);
    }
}
