using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitiveBackgorund : MonoBehaviour
{

    public float move_speed =-4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(move_speed*Time.deltaTime,0,0);

        if(transform.position.x <= -17.72f)
        {
            transform.position = new Vector3(17.81f, 0,5);
        }
    }
}
