using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossControler : MonoBehaviour
{
    public static BossControler Instance { get; private set; }
    private void Awake()

    {

        if (Instance != null && Instance != this)

        {

            Destroy(this);

        }

        else

        {

            Instance = this;

        }

    }

    float cooldown_otherattack;
    bool otherattack;
    GameObject boss;
    Vector3 original_position;

    public float attack_distance = -6f, movespeed = 3.5f;

    int score;

    AudioSource sound;
    public AudioClip rage_audio;
    public AudioSource soundtrack;

    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        StartCoroutine(WaitBossSpawn());
        original_position = new Vector3(6.22f,-0.19f,-1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        if (boss != null)
        {
            Cooldown();
        }
    }
    IEnumerator WaitBossSpawn()
    {
        yield return new WaitForSeconds(60);
        if(score >= 1000)
        {
            EnemySpawn.Instance.SpawnBoss();
            //Spawnar boss, destruir inimigos e !can_spawn
        }
        else
        {
            yield return new WaitForSeconds(30);
            EnemySpawn.Instance.SpawnBoss();
        }
        DaviMessageController.Instance.textbox.text = "This is the boss, YOU MUST KILL HIM!";
        //ChangeaudioDaviMessage.Instance.ChangeaudioPlay();
        ChangeaudioDaviMessage.Instance.audio_controller.clip = ChangeaudioDaviMessage.Instance.sound1;
        DaviMessageController.Instance.timeline_davimessage.GetComponent<PlayableDirector>().Stop();
        DaviMessageController.Instance.timeline_davimessage.GetComponent<PlayableDirector>().Play();
        StartCoroutine(ChangeaudioDaviMessage.Instance.Wait1sec());
        
        soundtrack.pitch = 0.9f;
    }

    public void Playerscore(int score_)
    {
        score = score_;
    }
    public void BossGameObject(GameObject boss_)
    {
        boss = boss_;
    }

    void Cooldown()
    {
        if(cooldown_otherattack <= 3.5f)
        {
            cooldown_otherattack += Time.deltaTime;
        }
        else
        {
            float random = Random.value;
            if(random <= 0.50f && !otherattack)
            {
                StartCoroutine(RageAttack());
            }
            else if((random>0.5f && random <= 0.85f) && !otherattack)
            {
                StartCoroutine(SummonEnemies());
            }
            else
            {
                cooldown_otherattack = 0;
            }
        }
    }

    IEnumerator RageAttack()
    {
        sound.PlayOneShot(rage_audio);
        otherattack = true;
        boss.GetComponent<HenriqueBossShot>().otherattack = true;

        //trocar sprite

        while(boss.transform.position.x > -6f)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, new Vector3(attack_distance,boss.transform.position.y,0), movespeed*Time.deltaTime);
            yield return null;
        }

        //voltar sprite
        yield return new WaitForSeconds(2);
        sound.PlayOneShot(rage_audio);
        while (boss.transform.position.x < original_position.x)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, new Vector3(original_position.x, boss.transform.position.y, 0), movespeed*Time.deltaTime);
            yield return null;
        }

        //movespeed += 0.25f;
        otherattack = false;
        boss.GetComponent<HenriqueBossShot>().otherattack = false;
        cooldown_otherattack = 0;
    }

    IEnumerator SummonEnemies()
    {
        otherattack = true;
        boss.GetComponent<HenriqueBossShot>().otherattack = true;
        EnemySpawn.Instance.BossSummon();

        yield return new WaitForSeconds(3);

        otherattack = false;
        boss.GetComponent<HenriqueBossShot>().otherattack = false;
        cooldown_otherattack = 0;

        yield return null;
    }
}
