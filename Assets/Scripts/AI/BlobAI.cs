using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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