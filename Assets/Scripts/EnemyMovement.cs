using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemy_speed, lifespan = 14;
    public float amplitude, frequencia;

    public float tempoinicial, yinicial;
    // Start is called before the first frame update
    void Start()
    {
        tempoinicial = Time.time;
        yinicial = transform.position.y;
        if(Random.value <= 0.50f) { amplitude = 1; }else { amplitude = -1; }
        Destroy(this.gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        float tempodecorrido = Time.time - tempoinicial;
        transform.position -= new Vector3(enemy_speed*Time.deltaTime, 0,0);

        float newY = amplitude * Mathf.Sin(frequencia * tempodecorrido);

        transform.position = new Vector3(transform.position.x, yinicial+newY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<EntityStats>().Damage(gameObject.GetComponent<EntityStats>().attack_dmg);

            this.gameObject.GetComponent<EntityStats>().Damage(1);
        }
    }
}
