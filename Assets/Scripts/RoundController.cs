using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KarrottEngine.EntitySystem;
using KarrottEngine.GridSystem;
using System.Linq;

public class RoundController : MonoBehaviour
{
    public GameObject MainCamera;
    public List<Entity> EntityQueue = new List<Entity>();
    public int EnemyMaxMoves = 3; 

    private Entity current = null;
    private bool canPerfom = true;
    private bool playerRoundStarted = false;
    private int currentEnemyMoves = 3;
    private TargetEntity cameraTarget;
    private Entity player;

    public void StartRounds()
    {
        List<Entity> enemies = KEGrid.GetEntitiesByType(EntityType.ENEMY).ToList();
        player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
        bool playerAdded = false;
        while (enemies.Count != 0 || !playerAdded)
        {
            int playerNum = enemies.Count;
            int r = Random.Range(0, enemies.Count + 1);
            if (r == playerNum)
            {
               if (!playerAdded)
                {
                    playerAdded = true;
                    EntityQueue.Add(player);
                }
                continue;
            }
            EntityQueue.Add(enemies[r]);
            enemies.Remove(enemies[r]);
        }
        
        cameraTarget = MainCamera.GetComponent<TargetEntity>();
    }

    void Update()
    {
        if (EntityQueue.Count > 0 && current != EntityQueue[0])
        {
            current = EntityQueue[0];
            currentEnemyMoves = 3;
            cameraTarget.Target = current;
        }

        switch (current.Type)
        {
            case EntityType.ENEMY:
                if (canPerfom)
                {
                    BlobAI ai = current.EntityObject.GetComponent<BlobAI>();
                    EntityMover mover = current.EntityObject.GetComponent<EntityMover>();

                    StartCoroutine(DoEnemyRound(ai, mover));
                    currentEnemyMoves--;
                }
                break;
            case EntityType.PLAYER:
                PlayerRoundController controller = player.EntityObject.GetComponent<PlayerRoundController>();
                if (!playerRoundStarted)
                {
                    playerRoundStarted = true;
                    controller.StartPlayerTurn();
                    break;
                }
                if (!controller.PlayerTurn)
                {
                    playerRoundStarted = false;
                    EntityQueue.Remove(current);
                    EntityQueue.Add(current);
                }
                break;
        }
    }

    IEnumerator DoEnemyRound(BlobAI ai, EntityMover mover)
    {
        canPerfom = false;

        if (currentEnemyMoves > 0)
        {
            StartCoroutine(ai.CheckMoving(mover));
        }
        else
        {
            EntityQueue.Remove(current);
            EntityQueue.Add(current);
        }

        yield return new WaitForSeconds(.75f);
        canPerfom = true;
    }
}
