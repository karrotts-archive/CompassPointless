using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject Marker;

    public Color AvailableLocationColor;
    public Color UnavailableLocationColor;
    public Color InteractableLocationColor;

    public bool ShowGridPlacementMarker = true;

    // Contains Grid based items.
    public List<GridItem> GridItems;

    private bool buildEnabled = true;
    
    // Start is called before the first frame update
    void Start()
    {
        GridSystem.InitGridSystem(Marker);
    }

    // Update is called once per frame
    void Update()
    {
        RenderMarker();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShowGridPlacementMarker = !ShowGridPlacementMarker;
            GridSystem.SetEnabled(!buildEnabled);
        }
    }

    /// <summary>
    /// Moves and renders marker on screen if enabled.
    /// </summary>
    private void RenderMarker()
    {
        if (ShowGridPlacementMarker)
        {
            Marker.SetActive(true);
            Marker.transform.position = GridSystem.GetGridPosFromMousePosition();

            if (GridSystem.CanPlaceBlock())
            {
                Marker.GetComponent<SpriteRenderer>().color = AvailableLocationColor;
            }
            else
            {
                Marker.GetComponent<SpriteRenderer>().color = UnavailableLocationColor;
            }
        } 
        else
        {
            Marker.SetActive(false);
        }
    }
}
