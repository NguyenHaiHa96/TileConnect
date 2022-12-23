using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map Data", menuName = "Game Data/Map Data")]
public class MapData : ScriptableObject
{
    public string Name;
    public List<int> Levels;
}
