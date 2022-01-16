using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KarrottEngine.GridSystem;
using KarrottEngine.EntitySystem;

public class BlobAI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayTurn();
    }

    public void PlayTurn()
    {
        Entity player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
        List<List<Move>> PossibleMoves = DetermineMovement(player.EntityObject.transform.position);

        List<Move> Ideal = new List<Move>();
        float smallest = 100f;
        foreach (List<Move> moves in PossibleMoves)
        {
            float dis = Vector2.Distance(moves[moves.Count - 1].MovePosition, player.EntityObject.transform.position);
            if (dis < smallest)
            {
                smallest = dis;
                Ideal = moves;
            }
        }

        transform.position = Ideal[9].MovePosition;
    }

    /// <summary>
    /// Removes anything that would be blocking the tiles
    /// </summary>
    /// <param name="tiles"></param>
    public List<Tile> FilterTiles(List<Tile> tiles)
    {

        return tiles;
    }

    public bool LegalSpace(Vector2 space)
    {
        Entity[] atSpace = KEGrid.GetEntitiesAtPosition(space);
        return !atSpace.Any(n => n.Type == EntityType.PLAYER || n.Type == EntityType.ENEMY || n.Type == EntityType.ENVIRONMENT);
    }

    public List<List<Move>> DetermineMovement(Vector2 playerPos)
    {
        List<List<Move>> AllMovement = new List<List<Move>>();
        Queue<Node> q = new Queue<Node>();
        Node node = new Node(transform.position, new List<Move>());

        q.Enqueue(node);

        while(q.Count != 0)
        {
            Node current = q.Peek();
            q.Dequeue();

            if (current.Moves.Count == 3)
            {
                AllMovement.Add(current.Moves);
                continue;
            }

            List<Tile> movementTiles = PatternLoader.LoadTiles(90, current.Position);
            EntityGridExtensions.RenderTiles = movementTiles;
            movementTiles = FilterTiles(movementTiles);

            foreach (Tile tile in movementTiles)
            {
                if (tile.Type == TileType.MOVE || tile.Type == TileType.ATTACK)
                {
                    if (tile.Type == TileType.MOVE)
                    {
                        if (!current.Moves.Any(n => n.MovePosition == tile.Position) && LegalSpace(tile.Position))
                        {
                            Move move = new Move();
                            move.MovePosition = tile.Position;
                            move.Action = AIAction.MOVE;
                            List<Move> newMoves = new List<Move>(current.Moves);
                            newMoves.Add(move);
                            q.Enqueue(new Node(tile.Position, newMoves));
                        }
                    }
                }
            }
        }
        return AllMovement;
    }

}

public class Node
{
    public Vector2 Position;
    public List<Move> Moves;

    public Node(Vector2 pos, List<Move> items)
    {
        Position = pos;
        Moves = items;
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