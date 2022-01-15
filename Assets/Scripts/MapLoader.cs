using System.Collections;
using System.Collections.Generic;
using KarrottEngine.EntitySystem;
using KarrottEngine.GridSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapLoader : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Environment;

    public TileBase GrassTile;
    public TileBase StoneTile;

    public void LoadMap(int mapId) 
    {
        Sprite loaded = Resources.LoadAll<Sprite>($"Maps/Map_0")[0];
        RenderEntityTiles(loaded);
        RenderTilemap(loaded);
    }

    public void RenderEntityTiles(Sprite entityTiles)
    {
        for (int y = 0; y < entityTiles.rect.height; y++)
        {
            for (int x = 0; x < entityTiles.rect.width; x++)
            {
                EntityType type = EntityType.UNKNOWN;
                GameObject toRender = null;

                Vector2 pos = new Vector2(x, y);
                Color pixelColor = entityTiles.texture.GetPixel(x + 32, y);
                if (pixelColor.Equals(Color.black))
                {
                    type = EntityType.ENVIRONMENT;
                    toRender = Environment;
                }

                if (pixelColor.Equals(Color.green))
                {
                    type = EntityType.PLAYER;
                    toRender = Player;
                }

                if (pixelColor.Equals(Color.red))
                {
                    type = EntityType.ENEMY;
                    toRender = Enemy;
                }
                

                if (toRender != null)
                    KEGrid.CreateEntity(toRender, type, pos);
            }
        }
    }

    public void RenderTilemap(Sprite tiles)
    {
        var grid = new GameObject("TileGrid").AddComponent<Grid>();
        var tilemapObj = new GameObject("Tilemap");
        var tilemap = tilemapObj.AddComponent<Tilemap>();
        var renderer = tilemapObj.AddComponent<TilemapRenderer>();

        //tilemap settings
        grid.transform.position = new Vector3(-.5f, -.5f, 0);
        tilemapObj.transform.SetParent(grid.gameObject.transform);
        tilemap.tileAnchor = new Vector3(0, 0, 0);
        renderer.sortingOrder = -1;

        for (int y = 0; y < tiles.rect.height; y++)
        {
            for (int x = 0; x < tiles.rect.width; x++)
            {
                Vector2 pos = new Vector2(x, y);
                Color pixelColor = tiles.texture.GetPixel(x, y);
                Color green = new Color(0, 0.494f, 0, 1);
                Color grey = new Color(0.494f, 0.494f, 0.494f, 1);

                if (CompareColor(pixelColor, green))
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), GrassTile);
                    continue;
                }

                if (CompareColor(pixelColor, grey))
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), StoneTile);
                    continue;
                }
            }
        }
    }

    private bool CompareColor(Color c1, Color c2)
    {
        return (int)Mathf.Round(c1.r * 255) == (int)Mathf.Round(c2.r * 255)
            && (int)Mathf.Round(c1.g * 255) == (int)Mathf.Round(c2.g * 255)
            && (int)Mathf.Round(c1.b * 255) == (int)Mathf.Round(c2.b * 255);
    }
}
