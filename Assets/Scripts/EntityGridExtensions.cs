using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KarrottEngine.EntitySystem;
using KarrottEngine.GridSystem;
using UnityEngine;

public static class EntityGridExtensions
{
    public static void RenderPlayerTiles(int patternId)
    {
        Entity player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
        KEGrid.LoadPattern(patternId, player.EntityObject.transform.position);
        List<Entity> controlTiles = FilterTileTypes(KEGrid.EntityRepository, new TileType[]{ TileType.EMPTY, TileType.ENEMY }); 
        RemoveBlockedTiles(controlTiles);
        Debug.Log(controlTiles.Count);
    }

    //sudo code, line renderer..
    // start at player tile
    // move to connecting tiles
    // check if connecting tile of the same type and check if the tile is blocked.
    // if the tile is blocked, mark it for removal and move to next block
    // set it for removal as well until we hit the end.
    // Once we hit 1 or zero connecting tiles than we hit the end and we can return back.

    /// <summary>
    /// Returns all connecting entities given a specific entity in the Entity Repository
    /// </summary>
    /// <param name="main"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
    public static List<Entity> GetConnectingEntities(Entity entity)
    {
        List<Entity> connecting = new List<Entity>();
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                if (x == 0 && y == 0)  continue;
                float offX = x + entity.EntityObject.transform.position.x;
                float offY = y + entity.EntityObject.transform.position.y;

                foreach(Entity e in KEGrid.EntityRepository) 
                {
                    if (e.EntityObject.transform.position.Equals(new Vector2(offX, offY)))
                    {
                        connecting.Add(e);
                    }
                }
            }
        }
        return connecting;
    }

    /// <summary>
    /// Removes all blocked tiles. Blocked means that there is an overlapping environment piece with the tile.
    /// </summary>
    /// <param name="tilesToCheck"></param>
    public static void RemoveBlockedTiles(List<Entity> tilesToCheck)
    {
        foreach (Entity tile in tilesToCheck)
        {
            Entity[] atPos = KEGrid.GetEntitiesAtPosition(tile.EntityObject.transform.position);
            if (atPos.Length >= 2 && atPos.ToList<Entity>().Any(n => n.Type == EntityType.ENVIRONMENT))
            {
                KEGrid.DeleteEntity(tile);
            }
        }
    }

    /// <summary>
    /// Given a list of entities filter out specific tile types. This method is destructive,
    /// if given entities that are not of type tile then they will be removed from the result.
    /// </summary>
    /// <param name="tiles"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    public static List<Entity> FilterTileTypes (List<Entity> tiles, TileType[] types)
    {
        List<Entity> copy = new List<Entity>(tiles);
        foreach (Entity tile in tiles)
        {
            if(tile.Type != EntityType.TILE || types.Contains((TileType)tile.SpecialType))
            {
                copy.Remove(tile);
            }
        }
        return copy;
    }
}
