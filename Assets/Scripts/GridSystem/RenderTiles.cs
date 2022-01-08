using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTiles : MonoBehaviour
{
    public GameObject Marker;
    public GameObject AttackMarker;

    public Color MoveColor;
    //public Color AttackColor;

    private List<GridTile> tiles;

    public void Render(int tileIndex) 
    {
        ClearTiles();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        tiles = LoadPatterns.Load(tileIndex);

        foreach (GridTile tile in tiles) 
        {
            if (tile.Type == TileType.NONE || tile.Type == TileType.PLAYER) continue;

            Vector2 placePos = (Vector2)player.transform.position + tile.Position;
            tile.Position = placePos;

            if (GridSystem.GetGameObjectAtPosition(tile.Position) != null)
            {
                continue;
            }

            GameObject placedItem;
            if (tile.Type == TileType.MOVEMENT)
            {
                placedItem = Instantiate(Marker, placePos, Quaternion.identity);
                placedItem.GetComponent<SpriteRenderer>().color = MoveColor;
                tile.Marker = placedItem;
                continue;
            }

            if (tile.Type == TileType.ATTACK)
            {
                placedItem = Instantiate(AttackMarker, placePos, Quaternion.identity);
                tile.Marker = placedItem;
                continue;
            }
        }
    }

    public void ClearTiles()
    {
        if (tiles != null && tiles.Count > 0)
        {
            foreach(GridTile tile in tiles) 
            {
                if (tile.Marker != null) 
                {
                    Destroy(tile.Marker);
                }
            }
            tiles = new List<GridTile>();
        }
    }

    public GridTile GetTileAtLocation(Vector2 pos)
    {
        if (tiles != null && tiles.Count > 0) 
        {
            foreach(GridTile tile in tiles)
            {
                //Debug.Log("");
                if (tile.Position.Equals(pos))
                {
                    return tile;
                }
            }
        }
        return null;
    }
}
