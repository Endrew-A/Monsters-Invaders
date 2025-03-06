using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;

    //CONTROLE DE TIROS
    float cooldown;
    bool can_attack;
    public float control_cooldown;

    //CONTROLE DE EFEITO
    [HideInInspector] public float effect_cooldown;
    public bool can_attack_special = false;
    public bool multishot_special = false;
    //public bool machinegun_special = false;

    //VELOCIDADE QUE O PROJETIL VAI IR
    public float attack_range;

    //AUDIO
    AudioSource sound;
    public AudioClip projectile_sound;
    

    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(control_cooldown == 0.1f) { sound.pitch = 1.5f; } else { sound.pitch = 1.1f; }
        if (Input.GetKey(KeyCode.Space) && can_attack && !multishot_special)
        {
            Simpleattack();
            if(control_cooldown != 0.1f) { sound.pitch = 1.1f; }
            sound.volume = 0.729f;
            sound.PlayOneShot(projectile_sound);
        }else if(Input.GetKey(KeyCode.Space)&& can_attack && multishot_special)
        {
            Multiattack();
            if(control_cooldown == 0.1f) { sound.pitch = 1.5f; }
            sound.volume = 0.729f;
            sound.PlayOneShot(projectile_sound);
        }
        Cooldown();
        if (can_attack_special) { EffectCooldown(); }
    }
    void Cooldown()
    {
        if(cooldown < control_cooldown)
        {
            cooldown += Time.deltaTime;
        } else { can_attack = true; }
    }
    void EffectCooldown()
    {
        if(effect_cooldown < 5)
        {
            effect_cooldown += Time.deltaTime;
        }
        else { can_attack_special = false; multishot_special = false; effect_cooldown = 0; control_cooldown = 0.5f; }
    }
    void Simpleattack()
    {
            GameObject projectile_instace = Instantiate(projectile, transform.position, Quaternion.identity);

            projectile_instace.GetComponent<Rigidbody2D>().AddForce(new Vector2(attack_range,0), ForceMode2D.Impulse);
            projectile_instace.GetComponent<ProjectileDamage>().lifespan = gameObject.GetComponent<EntityStats>().attack_lifespan;
            projectile_instace.GetComponent<ProjectileDamage>().dmg = gameObject.GetComponent<EntityStats>().attack_dmg;

            cooldown = 0;
            can_attack = false;
    }
    void Multiattack()
    {
        GameObject[] projectile_instances = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            projectile_instances[i] = Instantiate(projectile, transform.position, Quaternion.identity);
            projectile_instances[i].GetComponent<ProjectileDamage>().lifespan = gameObject.GetComponent<EntityStats>().attack_lifespan;
            projectile_instances[i].GetComponent<ProjectileDamage>().dmg = gameObject.GetComponent<EntityStats>().attack_dmg;
        }
        Vector2 direction = transform.up - transform.right;
        projectile_instances[0].GetComponent<Rigidbody2D>().AddForce(direction*6, ForceMode2D.Impulse);
        projectile_instances[0].transform.rotation = Quaternion.Euler(0, 0, 45);
        projectile_instances[1].GetComponent<Rigidbody2D>().AddForce(new Vector2(attack_range, 0), ForceMode2D.Impulse);
        direction = transform.up + transform.right;
        projectile_instances[2].GetComponent<Rigidbody2D>().AddForce(direction*6, ForceMode2D.Impulse);
        projectile_instances[2].transform.rotation = Quaternion.Euler(0, 0, -45);

        cooldown = 0;
        can_attack = false;

    }
}
