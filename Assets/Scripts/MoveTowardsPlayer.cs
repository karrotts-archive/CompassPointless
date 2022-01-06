using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public GameObject Player;
    public bool Move = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            Vector2 moveDir;
            Vector2 movement = transform.position;

            GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            moveDir = (Player.transform.position - transform.position).normalized;
            movement += moveDir * 5 * Time.deltaTime;
            transform.position = movement;
        }
    }
}
