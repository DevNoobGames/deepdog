using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class storeScript : MonoBehaviour
{
    public PlayerMain player;

    public GameObject storeCanvas;
    public GameObject gameOvercanvas;
    public TextMeshProUGUI scoreGameOver;
    public AudioSource buySound;

    [Header("buttons & text")]
    public Button lifeButton;
    public TextMeshProUGUI lifeButtonText;    
    public Button shieldButton;
    public TextMeshProUGUI shieldButtonText;
    public Button gunButton;
    public TextMeshProUGUI gunButtonText;

    [Header("store costs")]
    public float lifeCost;
    public float shieldCost;
    public float gunCost;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (storeCanvas.activeInHierarchy)
            {
                closeStore();
            }
            else
            {
                openStore();
            }
        }
    }

    public void openStore()
    {
        if (!storeCanvas.activeInHierarchy)
        {
            Time.timeScale = 0;
            storeCanvas.SetActive(true);

            lifeButtonText.text = "$" + lifeCost.ToString();
            shieldButtonText.text = "$" + shieldCost.ToString();
            gunButtonText.text = "$" + gunCost.ToString();

            if (player.life >= 5)
            {
                lifeButton.interactable = false;
            }
            else
            {
                lifeButton.interactable = true;
            }

            if (player.shieldObj.activeInHierarchy)
            {
                shieldButton.interactable = false;
            }
            else
            {
                shieldButton.interactable = true;
            }

            if (player.shooting)
            {
                gunButton.interactable = false;
            }
            else
            {
                gunButton.interactable = true;
            }
        }
    }

    public void closeStore()
    {
        Time.timeScale = 1;
        storeCanvas.SetActive(false);
    }

    public void buyLife()
    {
        if (player.life < 5 && player.money >= lifeCost)
        {
            player.addMoney(-lifeCost);
            buySound.Play();
            player.addLife(1);
            lifeCost *= 2;
            if (player.life >= 5)
            {
                lifeButton.interactable = false;
            }
            lifeButtonText.text = "$" + lifeCost.ToString();
        }
    }

    public void buyShield()
    {
        if (player.money >= shieldCost)
        {
            player.addMoney(-shieldCost);
            buySound.Play();
            player.shieldObj.SetActive(true);
            player.activeShield = true;
            shieldCost *= 2;
            shieldButton.interactable = false;
            shieldButtonText.text = "$" + shieldCost.ToString();
        }
    }

    public void buyGun()
    {
        if (player.money >= gunCost)
        {
            player.addMoney(-gunCost);
            buySound.Play();
            gunCost *= 2;
            gunButton.interactable = false;
            gunButtonText.text = "$" + gunCost.ToString();

            player.shooting = true;
            player.startgun();
        }
    }

    public void gameOverScreen(float scored)
    {
        gameOvercanvas.SetActive(true);
        scoreGameOver.text = "You scored " + scored.ToString("F0");
    }

    public void playAgainbtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
