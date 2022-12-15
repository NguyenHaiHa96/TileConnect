using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Create BlockPackObject", fileName = "BlockPackObject", order = 0)]
public class BlockPackObject :ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private new string name;
    [SerializeField] private List<Sprite> sprites;

    public int Id => id;
    public string Name => name;


    // Get random list id sprite block by count couple block
    public List<int> GetSpriteIds(int count)
    {
        var list = MyUtils.GetRandomItemsFromList(sprites, count % sprites.Count);
        var result = list.Select(item => sprites.IndexOf(item)).ToList();
        return result;
    }

    public Sprite GetSprite(int indexSprite)
    {
        return sprites[indexSprite % sprites.Count];
    }
}
