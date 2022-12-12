using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Pathfinding 
{
    private const int MOVE_STRAIGHT_COST = 10;

    [ShowInInspector] public GameGrid<PathNode> NodeGrid;
    public List<PathNode> OpenList;
    public List<PathNode> CloseList;

    public int[,] Mattrix;
    public int Row;
    public int Column;

    public GameGrid<PathNode> GetGrid()
    {
        return NodeGrid;
    }

    public Pathfinding(int row, int column)
    {
        Row = row;
        Column = column;
        Mattrix = new int[row, column];
        NodeGrid = new GameGrid<PathNode>(row, column, (GameGrid<PathNode> grid, int x, int y) => new PathNode(grid, x, y));
    }

    private PathNode GetNode(int x, int y)
    {
        PathNode node = NodeGrid.GetGridObject(x, y);
        return node;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = NodeGrid.GetGridObject(startX, startY);
        PathNode endNode = NodeGrid.GetGridObject(endX, endY);
        OpenList = new List<PathNode> { startNode };
        CloseList = new List<PathNode>();
        for (int x = 0; x < NodeGrid.Row; x++)
        {
            for (int y = 0; y < NodeGrid.Column; y++)
            {
                PathNode node = NodeGrid.GetGridObject(x, y);
                node.GCost = int.MaxValue;
                node.CalculateFCost();
                node.CameFromNode = null;
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (OpenList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(OpenList);
            if (currentNode == endNode)
            {
                return CalculatedPath(endNode);
            }

            OpenList.Remove(currentNode);
            CloseList.Remove(currentNode);
            foreach (PathNode neighbourNode in GetNeighbourNodes(currentNode))
            {
                if (CloseList.Contains(neighbourNode)) continue;
                if (!neighbourNode.IsWalkable)
                {
                    CloseList.Add(neighbourNode);
                    continue;
                }
                int tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost > neighbourNode.GCost)
                {
                    neighbourNode.CameFromNode = currentNode;
                    neighbourNode.GCost = tentativeGCost;
                    neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();
                    if (!OpenList.Contains(neighbourNode))
                    {
                        OpenList.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    private List<PathNode> CalculatedPath(PathNode endNode)
    {
        List<PathNode> pathNodeList = new List<PathNode>();
        pathNodeList.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.CameFromNode != null)
        {
            pathNodeList.Add(currentNode.CameFromNode);
            currentNode = currentNode.CameFromNode;
        }
        pathNodeList.Reverse();
        return pathNodeList;
    }

    private List<PathNode> GetNeighbourNodes(PathNode currentNode)
    {
        List<PathNode> neighbourNodes = new List<PathNode>();
        if (currentNode != null)
        {
            if (currentNode.Row - 1 >= 0)
            {
                neighbourNodes.Add(GetNode(currentNode.Row - 1, currentNode.Column));
            }
            if (currentNode.Row +1 < NodeGrid.Row)
            {
                neighbourNodes.Add(GetNode(currentNode.Row + 1, currentNode.Column));
            }

            if (currentNode.Column - 1 >= 0)
            {
                neighbourNodes.Add(GetNode(currentNode.Row, currentNode.Column - 1));
            }

            if (currentNode.Column < NodeGrid.Column)
            {
                neighbourNodes.Add(GetNode(currentNode.Row, currentNode.Column + 1));
            }
        }
        return neighbourNodes;
    }

    private int CalculateDistanceCost(PathNode node1, PathNode node2)
    {
        int xDistance = Mathf.Abs(node1.Row - node2.Row);
        int yDistance = Mathf.Abs(node1.Column - node2.Column);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
    {
        PathNode lowestFCostNode = pathNodes[0];
        for (int i = 0; i < pathNodes.Count; i++)
        {
            if (pathNodes[i].FCost < lowestFCostNode.FCost)
            {
                lowestFCostNode = pathNodes[i];
            }
        }
        return lowestFCostNode;
    }
}
