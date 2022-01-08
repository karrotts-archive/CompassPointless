using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadDataFiles
{
    private static string[] text;

    public static List<GridTile> GetLoadedTiles() 
    {
        text = System.IO.File.ReadAllLines(@"./Assets/Data/Pawn.txt");
        List<GridTile> Tiles = new List<GridTile>();
        for (int i = 0; i < text.Length; i++) 
        {
            for (int j = 0; j < text[i].Length; j++) 
            {
                GridTile tile = new GridTile();
                tile.Position.y = 5 - i;
                tile.Position.x = j - 5;
                switch(text[i][j])
                {
                    case '#':
                        tile.Type = TileType.NONE;
                        break;
                    case '@':
                        tile.Type = TileType.MOVEMENT;
                        break;
                    case 'X':
                        tile.Type = TileType.ATTACK;
                        break;
                    case 'O':
                        tile.Type = TileType.PLAYER;
                        break;
                }
                Tiles.Add(tile);
            }
        }
        return Tiles;
    }
}
