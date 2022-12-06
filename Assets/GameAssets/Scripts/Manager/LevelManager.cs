using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelGenerator LevelGenerator;

    public override void OnInit()
    {
        base.OnInit();
        LevelGenerator.OnInit();
    }
}
