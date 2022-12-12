using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    public Tile TilePrefab;
    public GameGrid<Tile> TileGrid;
    public float CellSize;

    private void Start()
    {
        CellSize = TilePrefab.SpriteRectHeight;
        TileGrid = new GameGrid<Tile>(6, 4, CellSize, null, InstantiateTile);
    }

    public Tile InstantiateTile()
    {
        Tile tile = Instantiate(TilePrefab);
        return tile;
    }
}
