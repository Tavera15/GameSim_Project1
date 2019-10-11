using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelKeyGoal : MonoBehaviour
{
    public int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.score % 5 == 0)
        {
            Destroy(gameObject);
            GameManager.LoadLevel(levelIndex);
        }
    }
}
