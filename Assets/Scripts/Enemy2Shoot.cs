using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2Shoot : MonoBehaviour
{
    public GameObject projectile;

    public float attack_range;

    float cooldown = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown >= 1.5f)
        {
            int num = Random.Range(0, 2);
            if(num == 0)
            {
                GameObject new_projectile = Instantiate(projectile, new Vector3(transform.position.x,transform.position.y, 1), Quaternion.identity);

                new_projectile.GetComponent<Rigidbody2D>().AddForce(new Vector3(-attack_range, 0,0), ForceMode2D.Impulse);
                new_projectile.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
                new_projectile.GetComponent<ProjectileDamage>().dmg = gameObject.GetComponent<EntityStats>().attack_dmg;
                new_projectile.GetComponent<ProjectileDamage>().lifespan = gameObject.GetComponent<EntityStats>().attack_lifespan;

                cooldown = 0;
            }
            else { cooldown = 0; }
        }
        Cooldown();
    }

    void Cooldown()
    {
        if(cooldown < 1.5f)
        {
            cooldown += Time.deltaTime;
        }
    }
}
