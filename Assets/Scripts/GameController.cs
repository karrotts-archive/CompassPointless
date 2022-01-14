using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KarrottEngine.GridSystem;
using KarrottEngine.EntitySystem;

public class GameController : MonoBehaviour
{
    public GameObject Marker;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Wall;
    public GameObject DebugUI;
    public GameObject PlayUI;

    private DeckManager deckManager;
    private GenerateUICards cardGen;
    private GameObject player;

    private List<GameObject> EnemiesInPlay;

    // Start is called before the first frame update
    void Start()
    {
        // cardGen = PlayUI.GetComponent<GenerateUICards>();
        // deckManager = new DeckManager();

        // cardGen.Generate(deckManager.DealHand());
        KEGrid.CreateEntity(Player, EntityType.PLAYER, new Vector2(0,0));
        KEGrid.CreateEntity(Wall, EntityType.ENVIRONMENT, new Vector2(1,2));
        KEGrid.CreateEntity(Wall, EntityType.ENVIRONMENT, new Vector2(-2,-2));
        KEGrid.CreateEntity(Wall, EntityType.ENVIRONMENT, new Vector2(3,2));
        KEGrid.CreateEntity(Wall, EntityType.ENVIRONMENT, new Vector2(3,-1));
        KEGrid.CreateEntity(Wall, EntityType.ENVIRONMENT, new Vector2(1,-4));
        KEGrid.CreateEntity(Wall, EntityType.ENVIRONMENT, new Vector2(-4,2));

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TargetEntity>().Target = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Debug controls
        if (Input.GetKeyDown(KeyCode.Q)) DebugUI.SetActive(!DebugUI.activeSelf);
        if (Input.GetMouseButtonDown(0)) HandleClick();
    }

    private void HandleClick()
    {
        Vector2 mousePos = KEGrid.GetMouseGridPosition();
        Entity entity = KEGrid.GetEntityAtPosition(mousePos, EntityType.TILE);
        if (entity != null) {
            Debug.Log((TileType)entity.SpecialType);
            switch((TileType)entity.SpecialType)
            {
                case TileType.MOVE:
                    Debug.Log("Moving...");
                    Entity player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
                    player.EntityObject.transform.position = mousePos;
                    KEGrid.DeleteEntitesWithType(EntityType.TILE);
                break;
                case TileType.ATTACK:
                    Debug.Log("Attacking...");
                break;
            }
        }
    }


}
