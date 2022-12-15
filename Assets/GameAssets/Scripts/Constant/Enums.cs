using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums 
{
   
}
public enum UIID
{
    UI_BlockRaycast,
    UI_MainMenu,
    UI_Settings,
    UI_Gameplay,
    UI_Store,
    UI_Map,

    UI_LoadingScene,
    UI_Victory,
    UI_Lose
}

public enum TileTheme
{
    Fish,
    Insect,
    Fruit,
    Bird,
    Dinosaur
}

public enum GameState 
{ 
    Shop, 
    MainMenu, 
    Pause, 
    Gameplay, 
    Quit 
}
