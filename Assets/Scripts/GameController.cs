using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject GridControllerObject;
    public GameObject DebugUI;
    public GameObject Wall;

    private GridController GridController;
    // Start is called before the first frame update
    void Start()
    {
        GridController = GridControllerObject.GetComponent<GridController>();
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
