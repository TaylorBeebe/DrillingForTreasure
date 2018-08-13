using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillHealth : MonoBehaviour {

    [SerializeField] Image drillHealthUI;
    HealthAndVariables hav;
    float health;
    float maxHealth;
	// Use this for initialization
	void Start () {
        hav = GetComponent<HealthAndVariables>();
        health = hav.health;
        maxHealth = hav.health;
	}
	
	// Update is called once per frame
	void Update () {
        health = hav.health;

        if (health <= 0)
        {

            Debug.Log("GAME IS OVER, DRILL DIED");
            //Game over

        }

        UIUpdate();
    }

    void UIUpdate()
    {
        float uiInfo = health / maxHealth;
        uiInfo *= 100;
        Mathf.Round(uiInfo);
        uiInfo /= 100;
        drillHealthUI.fillAmount = uiInfo;
        
    }
}
