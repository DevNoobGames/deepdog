using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroSequence : MonoBehaviour
{
    public GameObject[] objectsToActive;
    public GameObject[] objectsToDisable;

    public TextMeshProUGUI storyText;
    public int storyLevel;

    //story1;
    public GameObject dog11;
    public GameObject dog12;
    public GameObject dog21;

    void Start()
    {
        storyLevel = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (storyLevel == 1)
            {
                dog11.SetActive(false);
                dog12.SetActive(true);
                storyLevel = 2;

                storyText.text = "Well... she fell down the hole!";
                
                return;
            }

            if (storyLevel == 2)
            {
                dog12.SetActive(false);
                dog21.SetActive(true);
                storyLevel = 3;

                storyText.text = "I will come and save you, my love!";

                return;
            }

            if (storyLevel == 3)
            {
                foreach (GameObject game in objectsToActive)
                {
                    game.SetActive(true);
                }
                foreach (GameObject gam in objectsToDisable)
                {
                    gam.SetActive(false);
                }
            }
        }
    }
}
