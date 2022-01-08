using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    public RenderTiles tileRender;
    // Start is called before the first frame update
    void Start()
    {
        tileRender = GameObject.FindGameObjectWithTag("GridController").GetComponent<RenderTiles>();
    }

    // Update is called once per frame
    void Update()
    {
        GridTile tile = MarkerController.GetGridTile(GridSystem.GetGridPosFromMousePosition() - (Vector2)transform.position);
        if (Input.GetMouseButtonDown(0) && tile != null && tile.Type == TileType.MOVEMENT) 
        {
            transform.position = tile.Position + (Vector2)transform.position;
            MarkerController.ClearTiles();
        }
    }
}
