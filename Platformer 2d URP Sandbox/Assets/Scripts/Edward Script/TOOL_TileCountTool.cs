using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TOOL_TileCountTool : MonoBehaviour
{
    Tilemap tilemap;
    public Sprite CountingSprite;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        GetTileAmountSprite(CountingSprite);
    }
    public int GetTileAmountSprite(Sprite targetSprite)
    {
        int amount = 0;

        // loop through all of the tiles        
        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            Tile tile = tilemap.GetTile<Tile>(pos);
            if (tile != null)
            {
                if (tile.sprite == targetSprite)
                {
                    amount += 1;
                }
            }
        }

        //Debug.Log("Total Lv3 Playable Tiles = " + amount);
        return amount;
    }
}
