using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelGenerator LevelGenerator;
    public LevelCreator LevelCreator;
    public int CurrentLevel;

    public void GenerateLevel()
    {
        LevelGenerator.GenerateLevel(1);
    }
}
