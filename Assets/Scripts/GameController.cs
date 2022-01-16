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
        GetComponent<MapLoader>().LoadMap(0);
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
                    Entity[] entities = KEGrid.GetEntitiesAtPosition(mousePos);
                    if (entities.Length >= 1) 
                    {
                        foreach (Entity enemey in entities)
                        {
                            if(enemey.Type == EntityType.ENEMY && enemey.EntityObject.GetComponent<EntityManager>().TakeDamage(5))
                            {
                                KEGrid.DeleteEntity(enemey);
                                break;
                            }
                        }
                    }
                break;
            }
        }
    }


}
