using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Movement : MonoBehaviour
{
    public float enemy_speed, enemy_speed_rotation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= 6.2f)
        {
            transform.position -= new Vector3(enemy_speed * Time.deltaTime, 0, 0);
        }
        transform.Rotate(Vector3.forward * enemy_speed_rotation * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<EntityStats>().Damage(gameObject.GetComponent<EntityStats>().attack_dmg);

            this.gameObject.GetComponent<EntityStats>().Damage(2);
        }
    }

}
