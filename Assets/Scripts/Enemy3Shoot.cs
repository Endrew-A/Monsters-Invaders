using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Shoot : MonoBehaviour
{
    public List<GameObject> shoot_spawns;

    public GameObject projectile;

    public float attack_range;
    float cooldown;

    float reducaoabertura = 0.5f;
    public float[] angles = new float[4];
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
                Vector3[] direction = new Vector3[4];
                direction[0] = transform.up * reducaoabertura;
                direction[1] = transform.right * reducaoabertura;
                direction[2] = -transform.up * reducaoabertura;
                direction[3] = -transform.right * reducaoabertura;
                
                for(int i=0;i<4;i++)
                {

                    GameObject new_projectile = Instantiate(projectile, shoot_spawns[i].transform.position, Quaternion.identity);

                    new_projectile.GetComponent<Directionshoots>().SetEnemy(gameObject);
                    new_projectile.GetComponent<Directionshoots>().SetDirection(direction[i].normalized, angles[i]);                    
                    new_projectile.GetComponent<ProjectileDamage>().dmg = gameObject.GetComponent<EntityStats>().attack_dmg;
                    new_projectile.GetComponent<ProjectileDamage>().lifespan = gameObject.GetComponent<EntityStats>().attack_lifespan;
                    
                }

                cooldown = 0;
            }
            else { cooldown = 0; }
        }
        Cooldown();
    }

    void Cooldown()
    {
        if (cooldown < 1.5f)
        {
            cooldown += Time.deltaTime;
        }
    }
}
