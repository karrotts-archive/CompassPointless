using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCollision : MonoBehaviour
{
    public bool IsCollidingWithPlayer;
    // Start is called before the first frame update
    void Start()
    {
        IsCollidingWithPlayer = false;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsCollidingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsCollidingWithPlayer = false;
    }
}
