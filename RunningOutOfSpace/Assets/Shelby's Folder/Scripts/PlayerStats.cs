using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public float maxHealth;
    public float health;

    public float maxDrillHealth;
    public float drillHealth;

    public float maxWeaponCharge;
    public float weaponCharge;

	void Update ()
    {
        GameObject.Find("PlayerHealthBar").GetComponent<Image>().fillAmount = health / maxHealth;
        GameObject.Find("DrillHealthBar").GetComponent<Image>().fillAmount = drillHealth / maxDrillHealth;
        GameObject.Find("WeaponEnergy").GetComponent<Image>().fillAmount = weaponCharge / maxWeaponCharge;
    }
}
