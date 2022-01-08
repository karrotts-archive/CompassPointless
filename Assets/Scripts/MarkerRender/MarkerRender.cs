using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerRender : MonoBehaviour
{
    public GameObject Marker;
    public GameObject AttackMarker;
    public Color MoveColor;

    public void Render(List<GridTile> tiles)
    {
        GameObject renderedTilesObject = new GameObject("tiles");
        renderedTilesObject.tag = "Tiles";
        foreach (GridTile tile in tiles) 
        {
            switch(tile.Type)
            {
                case TileType.ATTACK:
                    GameObject AttackItem = Instantiate(AttackMarker, tile.Position, Quaternion.identity);
                    AttackItem.transform.parent = renderedTilesObject.transform;
                    break;
                case TileType.MOVEMENT:
                    GameObject MovementItem = Instantiate(Marker, tile.Position, Quaternion.identity);
                    MovementItem.GetComponent<SpriteRenderer>().color = MoveColor;
                    MovementItem.transform.parent = renderedTilesObject.transform;
                    break;
            }
        }
        renderedTilesObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void DestroyAllTiles()
    {
        GameObject tiles = GameObject.FindGameObjectWithTag("Tiles");
        if(tiles != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Tiles"));
        }
    }
}
