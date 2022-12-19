using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellContainer : CacheComponent
{
    public SpriteRenderer SpriteRenderer;

    public float SpriteRectWidth => SpriteRenderer.sprite.rect.width; 
    public float SpriteRectHeight => SpriteRenderer.sprite.rect.height; 
}
