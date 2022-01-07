using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public GameObject Player;
    public bool CanMove = false;
    public bool StopWhenReachedTarget = true;
    public float StopDistance = 1f;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //StopDistance = Random.Range(1, 3);
        if (CanMove)
        {
            Vector2 moveDir;
            Vector2 movement = transform.position;

            GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            moveDir = (Player.transform.position - transform.position).normalized;
            movement += moveDir * 5 * Time.deltaTime;

            if (StopWhenReachedTarget && Vector2.Distance(Player.transform.position, movement) < StopDistance)
            {
                movement = transform.position;
            } 
            
            transform.position = movement;
        }
    }
}
