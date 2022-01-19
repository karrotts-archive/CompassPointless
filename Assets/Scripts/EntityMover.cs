using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMover : MonoBehaviour
{
    public Vector2 MovePosition;
    public bool Moving;

    void Start()
    {
        MovePosition = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(MovePosition, (Vector2)transform.position) > .10f)
        {
            Moving = true;
            Vector2 dir = (MovePosition - (Vector2)transform.position).normalized;
            transform.position += (Vector3)dir * 2.5f * Time.deltaTime;
        } 
        else
        {
            transform.position = MovePosition;
            Moving = false;
        }
    }
}
