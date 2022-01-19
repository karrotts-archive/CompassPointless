using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using KarrottEngine.GridSystem;
using KarrottEngine.EntitySystem;

public class BlobAI : MonoBehaviour
{
    public int MaxDecisions = 3;
    public GameObject projectile;

    private Pathfinder pathfinder;
    private int currentDecisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<Pathfinder>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayTurn()
    {
        Entity player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
        /**
         * 0. Load in tiles
         * 1. Check if blob is currently able to attack the player.
         *  - Attack the player and move to next decision if available.
         * 2. If unable to attack the player, find where the blob would be able to attack the player and move one movement cost to that position
         * 3. Repeat until we run out of decisions.
         */
        List<Tile> tiles = PatternLoader.LoadTiles(90, transform.position);
        Tile attack = CanAttackPlayer(tiles, player.EntityObject.transform.position);
        if (attack != null)
        {
            Debug.Log("Blob Attacking!!");
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
        else
        {
            List<Node> path = pathfinder.FindPath(transform.position, ClosestAttackPosition(tiles, player.EntityObject.transform.position));
            if (path != null && path.Count > 0)
            {
                //transform.position = path[0].Position;
                MoveEnemy(path[0].Position);
            }
            else
            {
                Debug.Log("No valid path to target.");
            }
        }
    }

    public IEnumerator CheckMoving(EntityMover mover) 
    {
        yield return new WaitUntil(() => !mover.Moving);
        PlayTurn();
    }

    public void MoveEnemy(Vector2 pos)
    {
        EntityMover mover = GetComponent<EntityMover>();
        mover.MovePosition = pos;
        mover.Moving = true;
    }

    public Vector2 ClosestAttackPosition(List<Tile> tiles, Vector2 playerPos)
    {
        Vector2 closest = new Vector2(-1000f, -1000f);
        foreach (Tile tile in tiles)
        {
            if (tile.Type == TileType.ATTACK)
            {
                Vector2 pos = tile.Position - (Vector2)transform.position;
                pos *= -1;
                pos += playerPos;

                if (Vector2.Distance(pos, (Vector2)transform.position) < Vector2.Distance(closest, (Vector2)transform.position))
                {
                    if (!KEGrid.GetEntitiesAtPosition(pos).Any(n => n.Type == EntityType.ENEMY || n.Type == EntityType.ENVIRONMENT))
                        closest = pos;
                }
            }
        }
        return closest;
    }

    public Tile CanAttackPlayer(List<Tile> tiles, Vector2 playerPos)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.Type == TileType.ATTACK && tile.Position == playerPos)
            {
                return tile;
            }
        }
        return null;
    }
}

public class Move
{
    public Vector2 MovePosition;
    public AIAction Action;
    public bool StrongAttack;
}

public enum AIAction
{
    NONE,
    MOVE,
    ATTACK
}