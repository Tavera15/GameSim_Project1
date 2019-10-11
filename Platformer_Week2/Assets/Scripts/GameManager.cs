using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int currentLevelIndex;
    public static int score = 0;
    public static int scoreBeforeSceneLoad = 0;

    [Range(0, 100.0f)]
    public static float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Debug.Log("Score: " + score);
    }

    public void PlayerTakeDamage(int damageTaken)
    {
        health -= damageTaken;

        if (health <= 0)
        {
            score = (score - 5 <= 0 ? 0 : score - 5);
            ReloadSameLevel();
        }
    }

    public static void LoadLevel(int LevelIndex)
    {
        scoreBeforeSceneLoad = score;
        currentLevelIndex = LevelIndex;
        SceneManager.LoadScene(LevelIndex);
    }

    private void ReloadSameLevel()
    {
        score = scoreBeforeSceneLoad;
        health = 100;
        LoadLevel(currentLevelIndex);
    }
}
