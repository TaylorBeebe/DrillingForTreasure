using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

    public int scrapTotal;
    public int gemsTotal;
    public int scoreTotal;

    int scrapToAdd;
    int gemsToAdd;
    int scoreToAdd;

    public Text scrapText;
    public Text gemsText;
    public Text scoreText;

    void Start()
    {
        
    }
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Q))
            addScrap(10);
        if (Input.GetKeyDown(KeyCode.W))
            addGems(10);
        if (Input.GetKeyDown(KeyCode.E))
            addScore(10);
        */

        scrapText.text = scrapTotal.ToString();
        gemsText.text = gemsTotal.ToString();
        scoreText.text = scoreTotal.ToString();

    }
    public void addScrap(int scrap)
    {
        scrapToAdd += scrap;
        InvokeRepeating("addScrapEffect", 0, 1f / scrapToAdd);       
    }
    
    public void addGems(int gems)
    {
        gemsToAdd += gems;
        InvokeRepeating("addGemsEffect", 0, 1f / gemsToAdd);
    }
    public void addScore(int score)
    {
        scoreToAdd += score;
        InvokeRepeating("addScoreEffect", 0, 1f / scoreToAdd);
    }


    void addScrapEffect()
    {
        if (scrapToAdd == 0)
        {
            CancelInvoke("addScrapEffect");
            return;
        }

        scrapTotal++;
        scrapToAdd--;
    }
    void addGemsEffect()
    {
        if (gemsToAdd == 0)
        {
            CancelInvoke("addGemsEffect");
            return;
        }

        gemsTotal++;
        gemsToAdd--;
    }
    void addScoreEffect()
    {
        if (scoreToAdd == 0)
        {
            CancelInvoke("addScoreEffect");
            return;
        }

        scoreTotal++;
        scoreToAdd--;
    }
}
