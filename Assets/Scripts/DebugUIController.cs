using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void ShowPawnMovement()
    {
        MarkerController.RenderTiles(0);
    }

    public void ShowRookMovement()
    {
        MarkerController.RenderTiles(2);
    }

    public void ShowBishopMovement()
    {
        MarkerController.RenderTiles(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
