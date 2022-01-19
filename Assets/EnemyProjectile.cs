using KarrottEngine.GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = KEGrid.GetEntitiesByType(KarrottEngine.EntitySystem.EntityType.PLAYER)[0].EntityObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 10f * Time.deltaTime);
        if (Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) < .05f)
        {
            Destroy(gameObject);
            player.GetComponent<EntityManager>().TakeDamage(1, false);
        }
    }
}
