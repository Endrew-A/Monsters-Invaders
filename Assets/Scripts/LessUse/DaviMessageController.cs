using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DaviMessageController : MonoBehaviour
{
    public static DaviMessageController Instance { get; private set; }


    public GameObject timeline_davimessage;
    public Text textbox;

    int exp;

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

    public void PlayTimeline()
    {
        float random = Random.value;
        if(random <= 0.35f) { textbox.text = "Kill them all!!!"; }else if(random >0.35f && random <= 0.55f) { textbox.text = "You must defeat them!!!"; }
        else if(random >0.55f && random <= 0.80f) { textbox.text = "You also need to kill the boss!!!"; } else if(random > 0.8f) { textbox.text = "I AM DEILIPOPO"; }


        timeline_davimessage.GetComponent<PlayableDirector>().Play();
    }

    public void Countexp(int exp_)
    {
        exp += exp_;
        if(exp >= 500)
        {
            exp = 0;
            float random = Random.value;
            //Debug.Log(random);
            if(random >= 0.6f)
            {
                ChangeaudioDaviMessage.Instance.ChangeaudioPlay();
            }
        }
    }
}
