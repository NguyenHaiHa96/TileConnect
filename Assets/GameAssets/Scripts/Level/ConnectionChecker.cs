using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionChecker : Singleton<ConnectionChecker>
{
    public LevelData LevelData;
    [Header("Check Connection")]
    public List<CellData> CellConnectList;
    public CellData Cell_1;
    public CellData Cell_2;
    public int CellCount;

    public void SetStartCellData(CellData cellData)
    {
        Cell_1 = cellData;
    }

    public void SetEndCellData(CellData cellData)
    {
        Cell_2 = cellData;
    }

    public void CheckCell()
    {
        if (Cell_1.spriteId == Cell_2.spriteId)
        {
            CheckConnection();
        }
        else
        {
            this.LogMsg("Not same tile");
        }
    }

    public void CheckConnection()
    {
        CellConnectList = LevelData.CellPathCanDraw(Cell_1, Cell_2);
    }
}
