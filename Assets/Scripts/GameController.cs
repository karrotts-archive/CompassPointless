using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject GridControllerObject;
    public GameObject DebugUI;
    public GameObject PlayUI;


    private GridController GridController;
    private DeckManager deckManager;
    private GenerateUICards uigen;
    // Start is called before the first frame update
    void Start()
    {
        GridController = GridControllerObject.GetComponent<GridController>();
        uigen = PlayUI.GetComponent<GenerateUICards>();
        deckManager = new DeckManager();
        uigen.Generate(deckManager.DealHand());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DebugUI.SetActive(!DebugUI.activeSelf);
        }
    }
}
