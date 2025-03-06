using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverController : MonoBehaviour
{
    public GameObject gmover_canvas, scorecount_canvas;
    public Text score_text;




    public void EnableGmOver(int exp)
    {
        scorecount_canvas.SetActive(false);
        gmover_canvas.SetActive(true);
        score_text.text = exp.ToString("D7");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
