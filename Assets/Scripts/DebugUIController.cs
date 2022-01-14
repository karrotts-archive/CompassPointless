using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KarrottEngine.GridSystem;
using KarrottEngine.EntitySystem;

public class DebugUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public Entity player;
    void Start() 
    {
        player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
    }

    // Update is called once per frame
    void Update() { }

    public void ShowPawnMovement()
    {
        EntityGridExtensions.RenderPlayerTiles(0);
    }

    public void ShowRookMovement()
    {
        EntityGridExtensions.RenderPlayerTiles(2);
    }

    public void ShowBishopMovement()
    {
        EntityGridExtensions.RenderPlayerTiles(13);
    }

    public void Clear()
    {
        KEGrid.DeleteEntitesWithType(EntityType.TILE);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
