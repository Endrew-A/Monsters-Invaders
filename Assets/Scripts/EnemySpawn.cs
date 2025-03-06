using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn Instance { get; private set; }
    //SPAWNS
    public List<GameObject> spawns;
    public GameObject enemy_basic_prefab, enemy2_prefab, enemy3_prefab, boss_prefab;

    //CONTROLADOR DE AUDIO
    AudioSource sound;
    public AudioClip explosion_audio;

    //CONTROLE DE SPAWN
    float cooldown=-3;
    public float time;
    bool can_spawn, stopcooldown = false;
    EntityStats player;
    int count;

    //BOSS SUMMON
    GameObject[] summon_spawns = new GameObject[2];
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
    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        summon_spawns[0] = spawns[0];
        summon_spawns[1] = spawns[5];
    }

    // Update is called once per frame
    void Update()
    {
        if(can_spawn)
        {     
            float random = Random.value;
            if(random <= 0.6f)
            {
                NormalSpawn();
            }else if(random>=0.61f && random <= 0.9f)
            {
                DoubleSpawn();
            }
            else
            {
                TripleSpawn();
            }
            
            

            cooldown = 0;
            can_spawn = false;
        }
        if (!stopcooldown) { Cooldown();}
        
    }

    void NormalSpawn()
    {
            int random = Random.Range(0, 13);
            if(random >=0 && random <= 5)
            {
                Instantiate(enemy_basic_prefab, spawns[Random.Range(0, 5)].transform.position, Quaternion.identity);
            }else if(random>=6 && random <= 9){
                Instantiate(enemy2_prefab, spawns[Random.Range(0, 5)].transform.position, Quaternion.identity);
            }else if(random>=10 && random <= 12)
            {
                Instantiate(enemy3_prefab, spawns[Random.Range(0, 5)].transform.position, Quaternion.identity);
            }
    }

    void DoubleSpawn()
    {
        int random_spawn = Random.Range(0, 5);
        for(int i=0; i < 2; i++)
        {
            int random = Random.Range(0, 13);
            
            if (random >= 0 && random <= 5)
            {
                Instantiate(enemy_basic_prefab, spawns[random_spawn].transform.position, Quaternion.identity);
            }
            else if (random >= 6 && random <= 9)
            {
                Instantiate(enemy2_prefab, spawns[random_spawn].transform.position, Quaternion.identity);
            }
            else if (random >= 10 && random <= 12)
            {
                Instantiate(enemy3_prefab, spawns[random_spawn].transform.position, Quaternion.identity);
            }

            if (random_spawn == 5) { random_spawn = 0; } else { random_spawn++; }
        }
    }

    void TripleSpawn()
    {
        int random_spawn = Random.Range(0, 5);
        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, 13);

            if (random >= 0 && random <= 5)
            {
                Instantiate(enemy_basic_prefab, spawns[random_spawn].transform.position, Quaternion.identity);
            }
            else if (random >= 6 && random <= 9)
            {
                Instantiate(enemy2_prefab, spawns[random_spawn].transform.position, Quaternion.identity);
            }
            else if (random >= 10 && random <= 12)
            {
                Instantiate(enemy3_prefab, spawns[random_spawn].transform.position, Quaternion.identity);
            }

            if (random_spawn == 5) { random_spawn = 0; } else { random_spawn++; }
        }
    }

    void Cooldown()
    {
        if(cooldown < time)
        {
            cooldown += Time.deltaTime;
        }
        else { can_spawn = true; }
    }

    public void BossSummon()
    {
        for(int i=0; i < 2; i++)
        {
            float random = Random.value;
            if(random <= 0.45f)
            {
                //inimigo 1
                Instantiate(enemy_basic_prefab, summon_spawns[i].transform.position, Quaternion.identity);
            }else if(random >0.45f && random <= 0.75f)
            {
                //inimigo 2
                Instantiate(enemy2_prefab, summon_spawns[i].transform.position, Quaternion.identity);
            }
            else
            {
                //inimigo 3
                Instantiate(enemy3_prefab, summon_spawns[i].transform.position, Quaternion.identity);
            }
        }
    }

    public void SpawnBoss()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        can_spawn = false; stopcooldown = true;

        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<EntityStats>().Damage(100);
        }

        Instantiate(boss_prefab, new Vector3(spawns[2].transform.position.x +1, spawns[2].transform.position.y, 0), Quaternion.identity);
    }

    public void PlayExplosionAudio()
    {
        sound.pitch = 2.5f;
        sound.volume = 0.10f;
        sound.PlayOneShot(explosion_audio);
    }

    public void Countexp(int exp_)
    {
        count += exp_;
        if (!stopcooldown)
        {
            if (count >= 500 && time >=1f)
            {
                count = 0;
                time -= 0.2f;
            }
        }
    }
}
