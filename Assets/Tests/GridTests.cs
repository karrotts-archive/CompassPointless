using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTests : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Wall;
    void Update() 
    {
        // //temp disable player
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // player.SetActive(false);

        // //Start grid testing
        // GridController grid = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        // Debug.Log("--- STARTING GRID TESTS ---");

        // // Test #1
        // Debug.Log("Test #1 - Placing Game Object");
        // grid.PlaceGameObjectInGrid(Wall);
        // if (grid.GridItems.Count > 0) 
        // {
        //     Debug.Log("Success: Game Object was placed in game world...");
        // }
        // else
        // {
        //     Debug.LogWarning($"Failure: Game Object was unable to be placed... \n" + 
        //     $"Can Place Status: {grid.CanPlaceBlock()} \n" +
        //     $"Grid Items Count: {grid.GridItems.Count}");
        // }

        // //Test #2
        // Debug.Log("Test #2 - Can Destroy Game Object");
        // grid.RemoveGameObjectAtPosition(grid.GetGridPosFromMousePosition());
        // if (grid.GridItems.Count == 0) 
        // {
        //     Debug.Log("Success: Game Object was removed from world...");
        // }
        // else
        // {
        //     Debug.LogWarning($"Failure: Game Object was unable to be removed... \n" + 
        //     $"Can Place Status: {grid.CanPlaceBlock()} \n" +
        //     $"Grid Items Count: {grid.GridItems.Count}");
        // }

        // Clean up game world
        // /player.SetActive(true);

        Debug.Log("--- END OF GRID TESTS ---");
        Destroy(this);
    }
}
