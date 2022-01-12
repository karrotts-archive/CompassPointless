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
        KEGrid.LoadPattern(0, player.EntityObject.transform.position);
    }

    public void ShowRookMovement()
    {
        KEGrid.LoadPattern(2, player.EntityObject.transform.position);
    }

    public void ShowBishopMovement()
    {
        KEGrid.LoadPattern(1, player.EntityObject.transform.position);
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
