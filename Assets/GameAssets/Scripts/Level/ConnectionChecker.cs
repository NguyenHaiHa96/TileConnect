using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionChecker : Singleton<ConnectionChecker>
{
    [SerializeField] private Transform starContainer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Star starPrefab;
    [SerializeField] private List<Star> stars;
    [SerializeField] private LevelData levelData;
    [Header("Check Connection")]
    [SerializeField] private List<CellData> cellDatas;
    [SerializeField] private List<Vector3> cellPositions;
    [SerializeField] private Cell cell_1;
    [SerializeField] private Cell cell_2;
    [SerializeField] private CellData cellData_1;
    [SerializeField] private CellData cellData_2;
    [SerializeField] private int cellCount;

    private float timeDelayResetLine = 0.5f;

    public int CellCount { get => cellCount; set => cellCount = value; }

    public override void OnInit()
    {
        base.OnInit();
        starContainer = LevelManager.Instance.LevelGenerator.StarContainer; 
    }

    public void SetStartCell(Cell cell, CellData cellData)
    {
        cell_1 = cell;
        cellData_1 = cellData;
    }

    public void SetEndCell(Cell cell, CellData cellData)
    {
        cell_2 = cell;
        cellData_2 = cellData;
    }

    public void CheckCell()
    {
        if (cellData_1.spriteId == cellData_2.spriteId)
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
        CellCount = 0;
        cellDatas = levelData.CellPathCanDraw(cellData_1, cellData_2);
        if (cellDatas.Count > 0)
        {
            DrawLine();
        }
    }

    private void DrawLine()
    {
        foreach (CellData cellData in cellDatas)
        {
            Vector3 cellPosition = LevelManager.Instance.LevelGenerator.GetCellPositionByCellData(cellData);
            cellPositions.Add(cellPosition);
        }
        lineRenderer.positionCount = cellPositions.Count;

        for (int i = 0; i < cellPositions.Count; i++)
        {
            lineRenderer.SetPosition(i, cellPositions[i]);
        }

        ShowStar();
        StartCoroutine(ResetLine());
    }

    private void ShowStar()
    {
        
        foreach (Vector3 position in cellPositions)
        {
            Star star = Instantiate(starPrefab, position, Quaternion.identity, starContainer);
            stars.Add(star);
        }
    }

    private IEnumerator ResetLine()
    {
        yield return Helper.GetWaitForSeconds(timeDelayResetLine);
        lineRenderer.positionCount = 0;
        cell_1.SetIsNotBarrier();
        cell_2.SetIsNotBarrier();

        foreach (Star star in stars)
        {
            Destroy(star.gameObject);
        }
    }
}
