using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathNode 
{
    public PathNode CameFromNode;

    public GameGrid<PathNode> GameGrid;
    public int Row;
    public int Column;

    public int GCost;
    public int HCost;
    public int FCost;

    public bool IsWalkable;

    public PathNode(GameGrid<PathNode> grid, int row, int column)
    {
        GameGrid = grid;
        Row = row;
        Column = column;
        IsWalkable = true;
    }

    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }
}
