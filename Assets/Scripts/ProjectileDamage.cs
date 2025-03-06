using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{

    public int dmg;
    public float lifespan;

    public bool isenemy;
    
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isenemy)
        {
            collision.gameObject.GetComponent<EntityStats>().Damage(dmg);

            Destroy(this.gameObject);
        }else if(collision.CompareTag("Player") && isenemy)
        {
            collision.gameObject.GetComponent<EntityStats>().Damage(dmg);

            Destroy(this.gameObject);
        }else if (collision.CompareTag("Collider2"))
        {
            Destroy(gameObject);
        }
    }
}
