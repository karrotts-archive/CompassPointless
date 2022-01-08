using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIController : MonoBehaviour
{
    public GameObject PawnButton;
    public GameObject RookButton;
    public GameObject BishopButton;
    public GameObject DebugText;

    private RenderTiles tileRender;
    // Start is called before the first frame update
    void Start()
    {
        Button p_btn = PawnButton.GetComponent<Button>();
        Button b_btn = BishopButton.GetComponent<Button>();
        Button r_btn = RookButton.GetComponent<Button>();
		p_btn.onClick.AddListener(ShowPawnMovement);
        r_btn.onClick.AddListener(ShowRookMovement);
        b_btn.onClick.AddListener(ShowBishopMovement);

        tileRender = GameObject.FindGameObjectWithTag("GridController").GetComponent<RenderTiles>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowPawnMovement()
    {
        tileRender.Render(0);
    }

    public void ShowRookMovement()
    {
        tileRender.Render(2);
    }

    public void ShowBishopMovement()
    {
        tileRender.Render(1);
    }
}
