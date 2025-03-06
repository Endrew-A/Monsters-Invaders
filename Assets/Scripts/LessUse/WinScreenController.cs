using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public static WinScreenController Instance { get; private set; }
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


    public GameObject win_screen_canva, scorecount_canva;
    public Text score_text;

    AudioSource sound;
    public AudioClip win_audio;
    private void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }
    public void EnableWinScreen(int score_)
    {
        scorecount_canva.SetActive(false);
        win_screen_canva.SetActive(true);
        score_text.text = score_.ToString("D7");
        sound.PlayOneShot(win_audio);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
