using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MarkerController
{
    public static List<GridTile> MarkerRepository = new List<GridTile>();
    public static MarkerRender Render = GameObject.FindGameObjectWithTag("GridController").GetComponent<MarkerRender>();

    public static GridTile GetGridTile(Vector2 position)
    {
        foreach(GridTile tile in MarkerRepository) 
        {
            if (tile.Position.Equals(position))
            {
                return tile;
            }
        }
        return null;
    }

    public static void RenderTiles(int patternIndex)
    {
        ClearTiles();
        MarkerRepository = LoadPatterns.Load(patternIndex);
        Render.Render(MarkerRepository);
    }

    public static void ClearTiles()
    {
        MarkerRepository.Clear();
        Render.DestroyAllTiles();
    }
}
