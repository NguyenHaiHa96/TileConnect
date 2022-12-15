using System.Collections.Generic;
using System.Text;
using UnityEngine;

enum OrientationPath
{
    Up, Down, Left, Right, None
}

[System.Serializable]
public class LevelData 
{
    // key data parse field start
    public const string idKey = "id";
    public const string blindFoldKey = "blindfold";
    public const string boomKey = "boom";
    public const string downKey = "down";
    public const string leftKey = "left";
    public const string moveNumKey = "movenum";
    public const string repeatKey = "repeat";
    public const string rightKey = "right";
    public const string scoreKey = "score";
    public const string shapeIdKey = "shapeid";
    public const string atlasIdKey = "atlasid";
    public const string timeKey = "time";
    public const string upKey = "up";
    // key data parse field end
        
    public int id;
    public int blindFold;
    public int boom;
    public bool isDown;
    public bool isLeft;
    public int moveNum;
    public int repeat;
    public bool isRight;
    public int score;
    public int time;
    public bool isUp;
    public Atlast atlast;
    public Shape shape;
        
    public override string ToString()
    {
        return $"< id:{id}, blindFold:{blindFold}, boom:{boom}, isDown:{isDown}, " +
                $"isLeft:{isLeft}, moveNum:{moveNum}, repeat:{repeat}, " +
                $" isRight:{isRight}, score:{score}, time:{time}, isUp:{isUp}," +
                $"atlast: {atlast}, shape:{shape} >";
    }

    // return list cell data of path 2 cell start, end,
    // if return empty: cannot draw with 2 cell
    // if return not null, clear all cell data return (set spriteId to -1)
    public List<CellData> CellPathCanDraw(CellData cellStart, CellData cellEnd)
    {
        var result = new List<CellData>();
        if (cellStart.IsSameCell(cellEnd))
        {
            return result;
        }
        if (cellStart.IsSameRowLine(cellEnd))
        {
            return CellPathCanDrawSameLineRow(cellStart, cellEnd);
        }
        if (cellStart.IsSameColLine(cellEnd))
        {
            return CellPathCanDrawSameLineCol(cellStart, cellEnd);
        }
        var queue = new List<CellData>();
        queue.Insert(0, cellStart);
        int countOrientationPath = 0;
        var lastOrientationPath = OrientationPath.None;
        while (queue.Count > 0) {
            var p = queue[0];
            queue.RemoveAt(0);
            // Destination found;
            if (p.IsSameCell(cellEnd))
            {
                break;
            }
            // moving up
            var upCell = shape.findCellData(p.rowIndex - 1, p.colIndex);
            if (upCell != null && (upCell.IsEmptyCell || upCell.spriteId == p.spriteId))
            {
                queue.Insert(0, upCell);
                result.Add(upCell);
                if (lastOrientationPath != OrientationPath.Up)
                {
                    countOrientationPath++;
                    lastOrientationPath = OrientationPath.Up;
                }
            }
            // moving down
            var downCell = shape.findCellData(p.rowIndex + 1, p.colIndex);
            if (downCell != null && (downCell.IsEmptyCell || downCell.spriteId == p.spriteId))
            {
                queue.Insert(0, downCell);
                result.Add(downCell);
                if (lastOrientationPath != OrientationPath.Down)
                {
                    countOrientationPath++;
                    lastOrientationPath = OrientationPath.Down;
                }
            }
            // moving left
            var leftCell = shape.findCellData(p.rowIndex, p.colIndex - 1);
            if (leftCell != null && (leftCell.IsEmptyCell || leftCell.spriteId == p.spriteId))
            {
                queue.Insert(0, leftCell);
                result.Add(leftCell);
                if (lastOrientationPath != OrientationPath.Left)
                {
                    countOrientationPath++;
                    lastOrientationPath = OrientationPath.Left;
                }
            }
            // moving right
            var rightCell = shape.findCellData(p.rowIndex, p.colIndex + 1);
            if (rightCell != null && (rightCell.IsEmptyCell || rightCell.spriteId == p.spriteId))
            {
                queue.Insert(0, rightCell);
                result.Add(rightCell);
                if (lastOrientationPath != OrientationPath.Right)
                {
                    countOrientationPath++;
                    lastOrientationPath = OrientationPath.Right;
                }
            }
            // max 3 path can draw
            if (countOrientationPath > 3)
            {
                result.Clear();
                break;
            }
        }

        if (result.Count > 0 && countOrientationPath <= 3)
        {
            result.Insert(0, cellStart);
            result.Add(cellEnd);
        }
        return result;
    }

    // 2 cell same row lines
    private List<CellData> CellPathCanDrawSameLineRow(CellData cellStart, CellData cellEnd)
    {
        var result = new List<CellData>();
        if (cellStart.rowIndex == 0 || cellStart.rowIndex == shape.row - 1)
        {
            result.Add(cellStart);
            result.Add(cellEnd);
            return result;
        }
        else
        {
            bool isCanDraw = true;
            for (int i = Mathf.Min(cellStart.rowIndex, cellEnd.rowIndex) + 1; i < Mathf.Max(cellStart.rowIndex, cellEnd.rowIndex); i++)
            {
                var centerCell = shape.findCellData(i, cellStart.colIndex);
                if (centerCell != null && centerCell.spriteId != cellStart.spriteId)
                {
                    isCanDraw = false;
                    break;
                }
            }

            if (isCanDraw)
            {
                result.Add(cellStart);
                result.Add(cellEnd);
                return result;
            }
            else
            {
                result.Clear();
                return result;
            }
        }
    }
    // 2 cell same col lines
    private List<CellData> CellPathCanDrawSameLineCol(CellData cellStart, CellData cellEnd)
    {
        var result = new List<CellData>();
        if (cellStart.colIndex == 0 || cellStart.colIndex == shape.col - 1)
        {
            result.Add(cellStart);
            result.Add(cellEnd);
            return result;
        } 
        else
        {
            bool isCanDraw = true;
            for (int j = Mathf.Min(cellStart.colIndex, cellEnd.colIndex) + 1; j < Mathf.Max(cellStart.colIndex, cellEnd.colIndex); j++)
            {
                var centerCell = shape.findCellData(cellStart.rowIndex, j);
                if (centerCell != null && centerCell.spriteId != cellStart.spriteId)
                {
                    isCanDraw = false;
                    break;
                }
            }
            if (isCanDraw)
            {
                result.Add(cellStart);
                result.Add(cellEnd);
                return result;
            }
            else
            {
                result.Clear();
                return result;
            }
        }
    }
}
    
[System.Serializable]
public class Shape
{
    // key data parse field start
    public const string rowKey = "row";
    public const string colKey = "col";
    public const string datasKey = "datas";
    // key data parse field end
        
    public int id;
    // row mang 2 chieu
    public int row;
    //col mang 2 chieu
    public int col;
    // mang 2 chieu sprite ids, index row, col : >=0  is has sprite cell, -1 is empty cell
    public List<CellData> datas;
        
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append("[ ");
        foreach (var cell in datas)
        {
            builder.Append(cell);
        }
        builder.Append(" ],");
        return $"<id:{id}, row:{row}, col:{col}, datas: {builder}>";
    }

    public CellData findCellData(int rowIndex, int colIndex)
    {
        if (rowIndex < 0 || rowIndex >= row || colIndex < 0 || colIndex >= col)
        {
            return null;
        }
        foreach (var cell in datas)
        {
            if (cell.rowIndex == rowIndex && cell.colIndex == colIndex)
            {
                return cell;
            }
        }
        return null;
    }
}
    
[System.Serializable]
public class Atlast
{
    // key data parse field start
    public const string countKey = "count";
    public const string packIdkey = "packid";
    // key data parse field end
        
    public int id;
    // số cặp cell
    public int count;
    // packID để get ra bộ ảnh:
    // ví dụ packId = 1 -> bộ cá, packId = 2 -> bộ khủng long, packId = 3 -> bộ ô tô....
    public int packId;
    public override string ToString()
    {
        return $"<id:{id}, count:{count}, packId:{packId}>";
    }
}
    
// save index row, index col, and sprite Id of cell
[System.Serializable]
public class CellData
{
    public int rowIndex;
    public int colIndex;
    // sprite id of cell: -1 is empty, >= 0 is has sprite cell
    public int spriteId = -1;
            
    public CellData(int aRowIndex, int aColIndex, int aSpriteId)
    {
        rowIndex = aRowIndex;
        colIndex = aColIndex;
        spriteId = aSpriteId;
    }
        
    public override string ToString()
    {
        return $"<rowIndex:{rowIndex}, colIndex:{colIndex}, spriteId: {spriteId}>, ";
    }

    public bool IsSameCell(CellData other)
    {
        return rowIndex == other.rowIndex && colIndex == other.colIndex;
    }

    public bool IsSameRowLine(CellData other)
    {
        if (rowIndex == other.rowIndex && colIndex != other.colIndex)
        {
            return true;
        }
        return false;
    }
    public bool IsSameColLine(CellData other)
    {
        if (rowIndex != other.rowIndex && colIndex == other.colIndex)
        {
            return true;
        }
        return false;
    }
    public bool IsEmptyCell => spriteId == -1; // empty cell
}
