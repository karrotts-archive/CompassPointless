using KarrottEngine.EntitySystem;
using KarrottEngine.GridSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindPath(transform.position, KEGrid.GetEntitiesByType(EntityType.PLAYER)[0].EntityObject.transform.position);
        }
    }

    Vector2[] LoadNeighbors()
    {
        return new Vector2[]
        {
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0, -1),
            new Vector2(-1, 0),
        };
    }

    List<Node> GetNeighbors(Node node, List<Node> openNodes, HashSet<Node> closedNodes)
    {
        List<Node> nodes = new List<Node>();
        Vector2[] neighborPos = LoadNeighbors();
        foreach (Vector2 neighbor in neighborPos)
        {
            if (openNodes.Any(n => n.Position == neighbor + node.Position))
            {
                nodes.Add(openNodes.Single(n => n.Position == neighbor + node.Position));
            }
            else if (closedNodes.Any(n => n.Position == neighbor + node.Position))
            {
                nodes.Add(closedNodes.Single(n => n.Position == neighbor + node.Position));
            }
            else
            {
                nodes.Add(new Node(neighbor + node.Position));
            }
        }
        return nodes;
    }

    List<Node> FindPath(Vector2 startPosition, Vector2 endPosition)
    {
        Node start = new Node(startPosition);
        Node end = new Node(endPosition);
        List<Node> Open = new List<Node>();
        HashSet<Node> Closed = new HashSet<Node>();
        Open.Add(start);

        while(Open.Count > 0)
        {
            Node current = Open[0];

            for (int i = 1; i < Open.Count; i++)
            {
                if(Open[i].fCost < current.fCost || current.fCost == Open[i].fCost)
                {
                    if(Open[i].hCost < current.hCost)
                    {
                        current = Open[i];
                    }
                }
            }

            Open.Remove(current);
            Closed.Add(current);

            if (current.Position == end.Position)
            {
                return RetraceNodes(start, current); ;
            }

            foreach (Node node in GetNeighbors(current, Open, Closed))
            {
                if (!node.Walkable || Closed.Contains(node)) continue;

                int costToNeighbor = current.gCost + GetDistance(current, node);
                if (costToNeighbor < node.gCost || !Open.Contains(node))
                {
                    node.gCost = costToNeighbor;
                    node.hCost = GetDistance(node, end);
                    node.parent = current;

                    if (!Open.Contains(node))
                    {
                        Open.Add(node);
                    }
                }
            }
        }
        return null;
    }

    List<Node> RetraceNodes(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node current = end;
        while (current != start)
        {
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    int GetDistance(Node A, Node B)
    {
        int x = Mathf.Abs((int)A.Position.x - (int)B.Position.x);
        int y = Mathf.Abs((int)A.Position.y - (int)B.Position.y);

        if (x > y)
            return 10 * y + 10 * (x - y);
        return 10 * x + 10 * (y - x);
    }
}

public class Node
{
    public bool Walkable;
    public Vector2 Position;
    public int hCost;
    public int gCost;
    public Node parent;

    public Node(Vector2 position)
    {
        Position = position;
        Walkable = !KEGrid.GetEntitiesAtPosition(position).Any(n => EntityType.ENVIRONMENT == n.Type);
    }

    public int fCost
    {
        get
        {
            return hCost + gCost;
        }
    }
}
