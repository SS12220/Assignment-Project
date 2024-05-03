using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public AudioSource audioSource;
    public TextMeshProUGUI scoreText;
    public GameObject completeScreen;
    public TextMeshProUGUI finalScore;
    public GameObject Player;
    internal int score = 0;

    static int i = 0;

    private void Awake()
    {
        // Make sure instance is assigned only once
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        GameManager.instance.score = 0;
    }


    public static void UpdateScore(int change, AudioClip clip = null)
    {
        GameManager.instance.score += change;

        if (clip != null)
        {
            instance.audioSource.PlayOneShot(clip);
        }

        if (change > 0)
        {
            i++;
        }

        if (i == 4)
        {
            GameManager.instance.completeScreen.SetActive(true);
            GameManager.instance.Player.SetActive(false);
            GameManager.instance.finalScore.text = "Final Score: " + GameManager.instance.score.ToString();
        }


        // Update the ScoreText to display the new score
        instance.scoreText.text = "Score: " + GameManager.instance.score.ToString();


    }

    public void Restrat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
