using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EntityStats : MonoBehaviour
{
    //Stats
    public int hp, max_hp, attack_dmg;
    public float attack_lifespan;
    public int exp;
    AudioSource sound;
    
    //Player GO
    GameObject player;

    //Canvas
    public Text score_text;

    //Explosion GO & Audio
    public GameObject explosion;
    public AudioClip upgrade_audio;

    //Sprites & Isboss
    public Sprite sprite_normal, sprite_blink_normal, sprite_lowhp, sprite_lowhp_blink, sprite_lowhp2, sprite_lowhp2_blink;
    public bool isboss = false;

    public GameObject Rastro;

    //PowerUp & Drop
    public GameObject powerup, drop;

    //CONTROLE DE EFEITO (INVULNERABILIDADE)
    [HideInInspector] public bool invulnerability_effect = false;
    [HideInInspector] public float cooldown;

    //GameOver Screen & Win Screen
    public GameObject gmover_controler;

    //Player Hp
    public Text hp_text;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag != "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else { hp_text.text = "X" + max_hp.ToString(); }
        hp = max_hp;
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerability_effect)
        {
            Invulnerability_cooldown();
        }
    }

    public void Damage (int dmg)
    {
        if (!invulnerability_effect)
        {
            hp -= dmg;
            //CONTROLE DE MUDANÇA DE SPRITES
            if (gameObject.tag == "Enemy" && !isboss)
            {
                if (hp >= 2)
                {
                    Invoke("BlinkNormal", 0f);
                    Invoke("ReturnSprite", 0.1f);
                }
                else
                {
                    Invoke("Blinklowhp", 0f);
                    Invoke("ReturnSpritelowhp", 0.1f);
                    Destroy(Rastro);
                }
            }
            else if(gameObject.CompareTag("Enemy") && isboss)
            {
                if (hp > 10)
                {
                    Invoke("BlinkNormal", 0f);
                    Invoke("ReturnSprite", 0.1f);
                }
                else
                {
                    Invoke("Blinklowhp", 0f);
                    Invoke("ReturnSpritelowhp", 0.1f);
                }
            }
            else if (gameObject.tag == "Player")
            { 
                PlayerSprites();
                UpdatePlayerHp();
            }
                

            Death();
        }
    }

    public void UpdatePlayerHp()
    {
        hp_text.text = "X" + hp.ToString();
    }

    public void PlayerSprites()
    {
                if (hp >= 3)
                {
                    Invoke("BlinkNormal", 0f);
                    Invoke("ReturnSprite", 0.1f);
                }
                else if (hp == 2)
                {
                    Invoke("Blinklowhp", 0f);
                    Invoke("ReturnSpritelowhp", 0.1f);
                }
                else
                {
                    Invoke("Blinklowhp2", 0f);
                    Invoke("ReturnSpritelowhp2", 0.1f);
                }
    }


    public void Death()
    {
        if (hp <= 0)
        {
            GameObject a =Instantiate(explosion, transform.position, Quaternion.identity);

            EnemySpawn.Instance.PlayExplosionAudio();
            Destroy(a, 0.45f);
            if(gameObject.tag == "Enemy") { player.GetComponent<EntityStats>().exp += exp; EnemySpawn.Instance.Countexp(exp); DaviMessageController.Instance.Countexp(exp);
                player.GetComponent<EntityStats>().UpdateScoreCount();
                //Instanciar um GO
                float random = Random.value;
                if (random <=0.33f)
                {
                    Instantiate(powerup, transform.position, Quaternion.identity);
                }else if(random > 0.63f)
                {
                    Instantiate(drop, transform.position, Quaternion.identity);
                }
                if (isboss)
                {
                    WinScreenController.Instance.EnableWinScreen(player.GetComponent<EntityStats>().exp);
                    DaviMessageController.Instance.textbox.text = "You did it! Now let's return to the base.";
                    ChangeaudioDaviMessage.Instance.audio_controller.clip = ChangeaudioDaviMessage.Instance.sound1;
                    ChangeaudioDaviMessage.Instance.audio_controller.volume = 0.5f;
                    DaviMessageController.Instance.timeline_davimessage.GetComponent<PlayableDirector>().Stop();
                    DaviMessageController.Instance.timeline_davimessage.GetComponent<PlayableDirector>().Play();
                    StartCoroutine(ChangeaudioDaviMessage.Instance.Wait1sec());
                    CameraShaker.Instance.ShakeOnce(6, 6, 0, 0.5f);
                }
                else { CameraShaker.Instance.ShakeOnce(4, 4, 0, 0.5f); }
            }
            else if (gameObject.CompareTag("Player"))
            {
                gmover_controler.GetComponent<GameOverController>().EnableGmOver(exp); CameraShaker.Instance.ShakeOnce(4, 4, 0, 0.5f);
            }
            Destroy(this.gameObject);
            
        }
    }
    public void UpdateScoreCount()
    {
        BossControler.Instance.Playerscore(exp);
        score_text.text = exp.ToString("D7");
    }

    public void UpgradeAudio()
    {
        sound.volume = 0.2f;
        sound.pitch = 1.05f;
        sound.PlayOneShot(upgrade_audio);
    }

    public void Invulnerability_cooldown()
    {
        if(cooldown < 5f)
        {
            cooldown += Time.deltaTime;
        }
        else { cooldown = 0; invulnerability_effect = false; }
    }


    void BlinkNormal()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_blink_normal;
    }
    void ReturnSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_normal;
    }
    void Blinklowhp()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_lowhp_blink;
    }
    void ReturnSpritelowhp()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_lowhp;
    }
    void ReturnSpritelowhp2()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_lowhp2;
    }
    void Blinklowhp2()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_lowhp2_blink;
    }
}
