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
        if (Input.GetKeyDown(KeyCode.E))
        {
            //interaction testing
            GridController gridController = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();

            GameObject closest = null;
            foreach (GridItem item in gridController.GridItems)
            {
                closest = closest == null ? item.gameObject : closest;
                if (Vector2.Distance(transform.position, closest.transform.position) > Vector2.Distance(transform.position, item.gameObject.transform.position))
                {
                    closest = item.gameObject;
                }
            }

            closest.GetComponent<InteractionHandler>().Interact();
        }

        // ALLL dis is gross but works as intended right now. SO maybe we change later? K.
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }

        dir.Normalize();

        GetComponent<Rigidbody2D>().velocity = Speed * dir;
    }
}
