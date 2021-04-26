using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PlayerMain : MonoBehaviour
{

    [HideInInspector]public bool canMove = true;

    public float money;
    [HideInInspector] public float life = 5;
    [HideInInspector] public float score = 0;
    [HideInInspector] public bool activeShield = false;
    [HideInInspector] public bool shooting = false;
    [HideInInspector] public bool loaded = true;
    public float reloadTime = 0.5f;

    [Header ("Text")]
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textScore;

    [Header("Sounds")]
    public AudioSource addMoneySound;
    public AudioSource hurtSound;
    public AudioSource backGroundSound;

    [Header("Other references")]
    public Animation hurtPanelAnim;
    public chunkManager chunkMan;
    public GameObject shieldObj;
    public Transform shotPos;
    public storeScript store;
    public GameObject explanationText;

    public GameObject[] hearts;

    private void Start()
    {
        canMove = true;
        money = 0;
        textMoney.text = "$" + money.ToString();
        StartCoroutine(cancelText());
    }

    IEnumerator cancelText()
    {
        yield return new WaitForSeconds(4.5f);
        explanationText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (backGroundSound.isPlaying)
            {
                backGroundSound.Stop();
            }
            else
            {
                backGroundSound.Play();
            }
        }

        if (life > 0)
        {
            if (canMove)
            {
                moving();
            }

            if (shooting && loaded)
            {
                loaded = false;
                Instantiate(Resources.Load("bone"), shotPos.position, Quaternion.identity);
                StartCoroutine(reloading());
            }

            score += Time.deltaTime;
            textScore.text = score.ToString("F0");

            if (!store.storeCanvas.activeInHierarchy)
            {
                if (Time.timeScale <= 2)
                {
                    Time.timeScale = 1 + (score / 250);
                }
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    IEnumerator reloading()
    {
        yield return new WaitForSeconds(reloadTime);
        loaded = true;
    }
    
    public void startgun()
    {
        StartCoroutine(resetGun());
    }
    IEnumerator resetGun()
    {
        yield return new WaitForSeconds(10);
        shooting = false;
        loaded = true;
    }

    void moving()
    {
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (!screenRect.Contains(Input.mousePosition))
            return;

        var pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector2(pos.x, transform.position.y);
    }

    public void addMoney(float moneyAdd)
    {
        money += moneyAdd;
        textMoney.text = "$" + money.ToString();

        addMoneySound.Play();
    }

    public void gotHurt(float lostLife, GameObject destroyObj, bool resetTimer)
    {
        hurtSound.Play();

        if (destroyObj != null)
        {
            Destroy(destroyObj);
        }

        if (resetTimer)
        {
            chunkMan.ActiveTimer = 0.1f;
        }

        if (!activeShield)
        {
            if (life > 0)
            {
                life -= lostLife;
                hurtPanelAnim.Play();

                foreach (GameObject hrt in hearts)
                {
                    if (hrt.activeInHierarchy)
                    {
                        hrt.SetActive(false);
                        if (life <= 0)
                        {
                            store.gameOverScreen(score);
                            Time.timeScale = 0;
                        }
                        return;
                    }
                }

                if (life <= 0)
                {
                    store.gameOverScreen(score);
                    Time.timeScale = 0;
                }
            }
        }
        else
        {
            activeShield = false;
            shieldObj.SetActive(false);
        }
    }

    public void addLife(int lifeAdd)
    {
        if (life < 5)
        {
            life += lifeAdd;
            foreach (GameObject hrt in hearts.Reverse())
            {
                if (!hrt.activeInHierarchy)
                {
                    hrt.SetActive(true);
                    return;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            addMoney(10);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("spike"))
        {
            gotHurt(1, collision.gameObject.transform.parent.gameObject, true);
        }
        if (collision.CompareTag("fireball"))
        {
            gotHurt(1, collision.gameObject.transform.parent.gameObject, true);
        }
        if (collision.CompareTag("dinoball"))
        {
            gotHurt(1, collision.gameObject, false);
        }
        if (collision.CompareTag("bird"))
        {
            gotHurt(1, collision.gameObject, false);
        }
    }
}
