using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = transform.position;
        movement.x += Speed * Time.deltaTime * Input.GetAxis("Horizontal");
        movement.y += Speed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.position = movement;
    }
}
