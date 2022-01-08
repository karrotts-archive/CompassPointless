using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadPatterns
{
    public static List<GridTile> Load(int index) 
    {
        int y = index > 9 ? (index / 10) * 11 : 0;
        int x = index > 9 ? (index % 10) * 11 : index * 11;

        //Load a Texture2D (Assets/Resources/Data/Move_Patterns.png)
        Texture2D patterns = Resources.Load<Texture2D>("Data/Move_Patterns");
        List<GridTile> Tiles = new List<GridTile>();
        for (int i = 0; i < 11; i++) 
        {
            for (int j = 0; j < 11; j++) 
            {
                GridTile tile = new GridTile();
                tile.Position.y = i - 5;
                tile.Position.x = 5 - j;

                Color posColor = patterns.GetPixel(x + j, y + i);
                if (posColor.Equals(Color.black))
                {
                    tile.Type = TileType.NONE;
                    Tiles.Add(tile);
                    continue;
                }
                if (posColor.Equals(Color.green))
                {
                    tile.Type = TileType.PLAYER;
                    Tiles.Add(tile);
                    continue;
                }
                if (posColor.Equals(Color.red))
                {
                    tile.Type = TileType.ATTACK;
                    Tiles.Add(tile);
                    continue;
                }
                if (posColor.Equals(Color.blue))
                {
                    tile.Type = TileType.MOVEMENT;
                    Tiles.Add(tile);
                    continue;
                }
            }
        }
        return Tiles;
    }
}
