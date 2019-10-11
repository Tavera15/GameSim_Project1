using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Text goldAmount;
    public Text healthAmount;
    public Image healthBar;
    public Button selfHurtButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }


    public void updateUI()
    {
        if(!healthBar || !healthAmount)
        {
            Debug.Log("Health Bar or Text Missing!");
            return;
        }

        healthAmount.text = GameManager.health.ToString();
        healthBar.fillAmount = GameManager.health / 100;
        healthBar.color = (GameManager.health <= 25 ? Color.red : Color.green);

        if (!goldAmount)
        {
            Debug.Log("Gold Text Missing!");
            return;
        }

        goldAmount.text = "Gold: " + GameManager.score;
    }

    public void receiveDamage()
    {
        GameManager.instance.PlayerTakeDamage(10);
    }

    public void LoadLevel(int levelIndex)
    {
        GameManager.LoadLevel(levelIndex);
    }
}
