using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KarrottEngine.EntitySystem;
using KarrottEngine.GridSystem;
using UnityEngine;

public static class EntityGridExtensions
{
    private static List<Tile> RenderTiles = new List<Tile>();
    private static GridRenderer Renderer = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridRenderer>();

    public static void RenderPlayerTiles(int patternId)
    {
        Entity player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
        RenderTiles = PatternLoader.LoadTiles(patternId, player.EntityObject.transform.position);
        RenderTiles = FilterOut(new List<TileType>{ TileType.PLAYER, TileType.EMPTY, TileType.ENEMY});

        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                Tile tile = GetTileByLocation(new Vector2(x , y) + (Vector2)player.EntityObject.transform.position);
                if (tile != null)
                {
                    RenderPath(new Vector2(x, y), new Tile(TileType.PLAYER, player.EntityObject.transform.position), tile);
                }
            }
        }

        foreach (Tile tile in RenderTiles)
        {
            Renderer.RenderEntityFromTile(tile);
        }
    }

    public static void RenderPath(Vector2 direction, Tile previous, Tile current)
    {
        Tile next = GetTileByLocation(current.Position + direction);
        if (previous == null && next != null)
        {
            RenderTiles.Remove(current);
            RenderPath(direction, null, next);
        }
        else
        {
            if (previous == null)
            {
                RenderTiles.Remove(current);
                return;
            }

            Entity[] ePos = KEGrid.GetEntitiesAtPosition(current.Position);
            if (ePos.ToList().Any(n=> n.Type == EntityType.ENVIRONMENT))
            {
                RenderTiles.Remove(current);
                if (next == null) return;
                RenderPath(direction, null, next);   
            } 
            else
            {
                if (next == null) return;
                RenderPath(direction, current, next);
            }
        }
    }

    public static Tile GetTileByLocation(Vector2 position)
    {
        foreach(Tile tile in RenderTiles) 
        {
            if (tile.Position.Equals(position)) return tile;
        }
        return null;
    }

    private static List<Tile> FilterOut(List<TileType> types)
    {
        List<Tile> copy = new List<Tile>(RenderTiles);
        foreach(Tile tile in RenderTiles)
        {
            if (types.Contains(tile.Type)) copy.Remove(tile);
        }
        return copy;
    }
}
