using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenriqueBossShot : MonoBehaviour
{
    public GameObject fireball;
    public float attack_range;
    [HideInInspector] public bool canattack, otherattack;
    float cooldown;

    [HideInInspector] public GameObject player, fireball_instance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        BossControler.Instance.BossGameObject(gameObject);
        StartCoroutine(MoveBoss());

    }

    // Update is called once per frame
    void Update()
    {        
        if (canattack && !otherattack)
        {
            float random = Random.value;
            if (random <= 0.6f&& player != null)
            {
                //Ataque de fireball             
                GameObject fireball_ = Instantiate(fireball,fireball_instance.transform.position, Quaternion.identity);
                fireball_.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);

                Vector2 projectile_direction = player.transform.position - transform.position;
                projectile_direction.Normalize();

                float rot_z = Mathf.Atan2(projectile_direction.y, projectile_direction.x) * Mathf.Rad2Deg;
                fireball_.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

                Vector2 direction = (player.transform.position - fireball_.transform.position) * 0.5f;
                fireball_.GetComponent<Rigidbody2D>().AddForce(direction * attack_range, ForceMode2D.Impulse);                

                cooldown = 0;
                canattack = false;
            }
            else
            {
                cooldown = 0;
                canattack = false;
            }
        }
        Cooldown();
    }

    void Cooldown()
    {
        if (cooldown < 2 && !canattack && !otherattack)
        {
            cooldown += Time.deltaTime;
        }
        else { canattack = true; }
    }

    IEnumerator MoveBoss()
    {
        while(transform.position.x > 6.22f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(6.22f, -0.19f, 0), 3.5f * Time.deltaTime);
            yield return null;
        }
        
    }

}
