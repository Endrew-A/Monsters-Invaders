using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropsConfig : MonoBehaviour
{

    public List<Sprite> drops_sprites;
    float random;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
        random = Random.value;

        if(random <= 0.2f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = drops_sprites[0];

        }
        else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(3 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(gameObject.GetComponent<SpriteRenderer>().sprite == drops_sprites[0])
            {
                if(collision.GetComponent<EntityStats>().hp < 3)
                {
                    collision.GetComponent<EntityStats>().hp += 1;
                    collision.GetComponent<EntityStats>().PlayerSprites();
                    collision.GetComponent<EntityStats>().UpdatePlayerHp();
                    collision.GetComponent<EntityStats>().UpgradeAudio();
                }
            }
            Destroy(gameObject);
        }
    }
}
