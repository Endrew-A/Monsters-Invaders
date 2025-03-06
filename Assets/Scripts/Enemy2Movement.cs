using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{
    public float enemy_speed, lifespan;
   
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifespan);

        gameObject.transform.rotation = Quaternion.Euler(0,0,90);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(enemy_speed * Time.deltaTime, 0, 0);
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
