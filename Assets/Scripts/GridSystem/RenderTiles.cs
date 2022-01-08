using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTiles : MonoBehaviour
{
    public GameObject Marker;

    public Color MoveColor;
    public Color AttackColor;

    private List<GridTile> tiles;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        tiles = LoadDataFiles.GetLoadedTiles();

        //Debug.Log(tiles.Count);
        foreach (GridTile tile in tiles) 
        {
            Debug.Log(tile.Type);
            if (tile.Type == TileType.NONE || tile.Type == TileType.PLAYER) continue;

            Vector2 placePos = (Vector2)player.transform.position + tile.Position;
            //for now I am not going to worry about storage of these tokens.
            //probably will need to since I will need to check if it is a valid spot

            GameObject placedItem;
            if (tile.Type == TileType.MOVEMENT)
            {
                placedItem = Instantiate(Marker, placePos, Quaternion.identity);
                placedItem.GetComponent<SpriteRenderer>().color = MoveColor;
                continue;
            }

            if (tile.Type == TileType.ATTACK)
            {
                placedItem = Instantiate(Marker, placePos, Quaternion.identity);
                placedItem.GetComponent<SpriteRenderer>().color = AttackColor;
                continue;
            }
        }

    }
}
