using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directionshoots : MonoBehaviour
{
    public float speed;
    [HideInInspector] public Vector3 direction;
    GameObject enemy3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 newdirection, float angle)
    {
        direction = newdirection;
        Vector3 Eulerenemy3 = enemy3.transform.rotation.eulerAngles;
        float newangle = Eulerenemy3.z - angle;
        transform.Rotate(Vector3.forward * (newangle-45));
    }
    public void SetEnemy(GameObject enemy)
    {
        enemy3 = enemy;
    }
}
