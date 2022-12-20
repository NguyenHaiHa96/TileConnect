using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelCreator LevelCreator;
    public LevelGenerator LevelGenerator;
    public ConnectionChecker ConnectionChecker;
    public int CurrentLevel;

    public void GenerateLevel()
    {
        LevelGenerator.GenerateLevel(1);
    }
}
