using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChangeaudioDaviMessage : MonoBehaviour
{
    public static ChangeaudioDaviMessage Instance { get; private set; }

    public PlayableDirector timeline;
    public AudioSource audio_controller;
    public AudioClip sound1, sound2;

    bool flag;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeaudioPlay()
    {
        if (!flag)
        {
            flag = true;
            float random = Random.value;
            if (random < 0.5f)
            {
                audio_controller.clip = sound1;
                audio_controller.volume = 0.5f;
            }
            else { audio_controller.clip = sound2; audio_controller.volume = 0.25f; }

            DaviMessageController.Instance.PlayTimeline();
            StartCoroutine(Wait1sec());
        }

    }

    public IEnumerator Wait1sec()
    {
        yield return new WaitForSeconds(1);

        audio_controller.Play();

        yield return new WaitForSeconds(5);
        flag = false;
        yield return null;
    }
}
