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

    private DeckManager deckManager;
    private GenerateUICards cardGen;
    private GameObject player;
    private PlayerRoundController playerRoundController;
    //private List<GameObject> EnemiesInPlay;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MapLoader>().LoadMap(0);
        GetComponent<RoundController>().StartRounds();
        player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0].EntityObject;
        playerRoundController = player.GetComponent<PlayerRoundController>();
        deckManager = player.GetComponent<DeckManager>();

        deckManager.GenerateDemoDeck();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug controls
        if (Input.GetKeyDown(KeyCode.Q)) DebugUI.SetActive(!DebugUI.activeSelf);
        if (Input.GetMouseButtonDown(0)) playerRoundController.HandleClick();
    }
}
