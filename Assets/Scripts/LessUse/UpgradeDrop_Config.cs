using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDrop_Config : MonoBehaviour
{
    public List<Sprite> upgrade_type_sprite;
    int powerup;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10f);
        int random = Random.Range(0, 3);
        if(random == 0)
        {
            //Receber sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = upgrade_type_sprite[0];
            powerup = 1;
        }
        else if(random == 1)
        {
            //Receber sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = upgrade_type_sprite[1];
            powerup = 2;
        }
        else if(random == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = upgrade_type_sprite[2];
            powerup = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(3 * Time.deltaTime, 0,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            switch (powerup)
            {
                case 1:
                    collision.gameObject.GetComponent<PlayerAttack>().can_attack_special = true;
                    collision.gameObject.GetComponent<PlayerAttack>().multishot_special = true;
                    collision.gameObject.GetComponent<PlayerAttack>().effect_cooldown = 0;
                    break;
                case 2:
                    collision.gameObject.GetComponent<PlayerAttack>().control_cooldown = 0.1f;
                    collision.gameObject.GetComponent<PlayerAttack>().can_attack_special = true;
                    //collision.gameObject.GetComponent<PlayerAttack>().multishot_special = false;
                    collision.gameObject.GetComponent<PlayerAttack>().effect_cooldown = 0;
                    break;
                case 3:
                    collision.gameObject.GetComponent<EntityStats>().invulnerability_effect = true;
                    collision.gameObject.GetComponent<EntityStats>().cooldown = 0;
                    break;
            }
            collision.GetComponent<EntityStats>().UpgradeAudio();
            Destroy(this.gameObject);
        }   
    }
}
