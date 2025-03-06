using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

   public float move_speed, base_speed;
    


    // Start is called before the first frame update
    void Start()
    {
        move_speed = base_speed;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal*move_speed*Time.deltaTime, vertical * move_speed * Time.deltaTime));

        if((horizontal > 0 || horizontal < 0) && (vertical > 0 || vertical < 0))
        {
            move_speed = base_speed * 0.66f;
        }else
        {
            move_speed = base_speed;
        }
    }
}
