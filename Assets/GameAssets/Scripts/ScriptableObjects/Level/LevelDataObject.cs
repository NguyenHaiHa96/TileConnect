using System;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GameData/Create LevelDataObject", fileName = "LevelDataObject", order = 0)]
public class LevelDataObject : ScriptableObject
{
    [SerializeField] private TextAsset levelTxtAsset;
    [SerializeField] private TextAsset shapeTxtAsset;
    [SerializeField] private TextAsset atlasTxtAsset;
    [SerializeField] private List<BlockPackObject> blockPackObjects;
    private List<JSONNode> levelNodes;
    private JSONNode shapesNodes;
    private List<JSONNode> atlastNodes;
    // levelId = 1, 2, 3.... (level 1, level 2, level 3...)
    public LevelData GetLevel(int levelId)
    {
        return GetLevelsFromDataTextAssets(levelId);
    }

    // Get level list from 3 file data: atlas.txt, level.txt, shape.txt
    private LevelData GetLevelsFromDataTextAssets(int levelId)
    {
        if (levelNodes == null || levelNodes.Count < 1)
        {
            levelNodes = JSON.Parse(levelTxtAsset.text).Children.ToList();
            shapesNodes = JSON.Parse(shapeTxtAsset.text.Replace("var shapes =", "").Trim());
            atlastNodes = JSON.Parse(atlasTxtAsset.text).Children.ToList();
        }
        foreach (var node in levelNodes)
        {
            var level = new LevelData();
            level.id = node.GetValueOrDefault(LevelData.idKey, 0);
            level.blindFold = node.GetValueOrDefault(LevelData.blindFoldKey, 0);
            level.boom = node.GetValueOrDefault(LevelData.boomKey, 0);
            level.isDown = node.GetValueOrDefault(LevelData.downKey, false);
            level.isLeft = node.GetValueOrDefault(LevelData.leftKey, false);
            level.moveNum = node.GetValueOrDefault(LevelData.moveNumKey, 0);
            level.repeat = node.GetValueOrDefault(LevelData.repeatKey, 0);
            level.isRight = node.GetValueOrDefault(LevelData.rightKey, false);
            level.score = node.GetValueOrDefault(LevelData.scoreKey, 0);
            level.time = node.GetValueOrDefault(LevelData.timeKey, 0);
            level.isUp = node.GetValueOrDefault(LevelData.upKey, false);
            // get atlast of level
            var atlast = new Atlast();
            atlast.id =  node.GetValueOrDefault(LevelData.atlasIdKey, 0);
            var atLastOrg = atlastNodes[atlast.id-1];
            atlast.count = atLastOrg.GetValueOrDefault(Atlast.countKey, "[]").
                Children.Select(item => item.AsInt).ToList()[0];
            atlast.packId = atLastOrg.GetValueOrDefault(Atlast.packIdkey, "[]").
                Children.Select(item => item.AsInt).ToList()[0];
            level.atlast = atlast;
            // get shape of level
            var shape = new Shape();
            shape.id = node.GetValueOrDefault(LevelData.shapeIdKey, 0);
            var shapeNode = shapesNodes.GetValueOrDefault(shape.id.ToString(), "{}");
            shape.row = shapeNode.GetValueOrDefault(Shape.rowKey, 0);
            shape.col = shapeNode.GetValueOrDefault(Shape.colKey, 0);
            var newDatasList = new List<int>();
            foreach (var value in shapeNode.GetValueOrDefault(Shape.datasKey, "[]").Children.ToList())
            {
                if (value == 1) // empty block
                {
                    newDatasList.Add(-1);
                }
                else
                {
                    newDatasList.Add(0); // has block
                }
            }
            GenLevelMatrixDatasGames(newDatasList, level);
            var cellDatas = new List<CellData>();
            for (int i = 0; i < newDatasList.Count; i++)
            {
                int iRow = i / shape.col;
                int jCol = i % shape.col;
                cellDatas.Add(new CellData(iRow, jCol,newDatasList[i]));
            }
            shape.datas = cellDatas;
            level.shape = shape;
            if (level.id == levelId)
            {
                return level;
            }
        }
        return null;
    }
    // Gen sprite id vao block id list datas de choi game
    private void GenLevelMatrixDatasGames(List<int> datas, LevelData level)
    {
        var blockPack = BlockPackObject(level);
        // check not nnykk
        var spriteIds = blockPack.GetSpriteIds(level.atlast.count);
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i] == 0) // has block
            {
                var spriteId = FindLowestCountSpriteIdApplied(datas, level, spriteIds);
                datas[i] = spriteId;
                // find couple block
                var blocksNexts = new List<int>();
                for (int j = i + 1; j < datas.Count; j++)
                {
                    if (datas[j] == 0)
                    {
                        blocksNexts.Add(j);
                    }
                }
                if (blocksNexts.Count > 0)
                {
                    datas[blocksNexts[Random.Range(0, blocksNexts.Count)]] = spriteId;
                }
            }
        }
    }

    private int FindLowestCountSpriteIdApplied(List<int> datas, LevelData level, List<int> spriteIds)
    {
        int minCount = -1;
        int minCountSpriteId = spriteIds[0];
        foreach (var spriteId in spriteIds)
        {
            int count = 0;
            for (int i = 0; i < datas.Count; i++)
            {
                if (datas[i] == spriteId)
                {
                    count++;
                }
                if (minCount >= 0 && count > minCount)
                {
                    break;
                }
            }
            if (count == 0)
            {
                return spriteId;
            }
            if (minCount < 0 || minCount > count)
            {
                minCount = count;
                minCountSpriteId = spriteId;
            }
        }
        return minCountSpriteId;
    }
        
    public BlockPackObject BlockPackObject(LevelData level)
    {
        return BlockPackObject(level.atlast);
    }
    public BlockPackObject BlockPackObject(Atlast atlastLevel)
    {
        return BlockPackObject(atlastLevel.packId);
    }
    public BlockPackObject BlockPackObject(int packId)
    {
        return blockPackObjects[packId % blockPackObjects.Count];
    }
}
