using System.Collections;
using System.Collections.Generic;
using KarrottEngine.EntitySystem;
using UnityEngine;

public class TargetEntity : MonoBehaviour
{
    public Entity Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (Vector3.Distance(transform.position, Target.EntityObject.transform.position) - 20f > .002f)
        {
            Vector3 direction = (Target.EntityObject.transform.position - this.transform.position);
            transform.position += direction * 5f * Time.deltaTime;
            Vector3 fix = transform.position;
            fix.z = -20;
            transform.position = fix;
        }
    }
}
