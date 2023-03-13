using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour
{
    public GameObject BossOrPlayer;
    public HealthBar HealthBar;
    private int max;
    private int current;
    public GameObject winningScreen; 
    private void Start()
    {
        SetMaxHealth();
       Debug.Log( BossOrPlayer.name);
    }
    private void Update()
    {
        SetHealth();
        
        if(BossOrPlayer.name == "OCD_Monster")
        {
           
            if (BossOrPlayer.GetComponent<Boss>().getHealth() == 0 && BossOrPlayer.GetComponent<Boss>().getIsPlayerAlive())
            {
                StartCoroutine(winScreen());
            }
        }
    }
    IEnumerator winScreen()
    {
        yield return new WaitForSeconds(1);
        winningScreen.SetActive(true);
    }

    public void SetMaxHealth()
    { 
        if(BossOrPlayer.name == "Player")
        {
            max = BossOrPlayer.GetComponent<playermovement>().getMaxHealth();
            HealthBar.slider.maxValue = max;
            HealthBar.slider.value = max;

        }else if(BossOrPlayer.name == "OCD_Monster")
        {
            max = BossOrPlayer.GetComponent<Boss>().getMaxHealth();
            HealthBar.slider.maxValue = max;
            HealthBar.slider.value = max;
        }
        HealthBar.fill.color = HealthBar.gradient.Evaluate(1f);
    }
    public void SetHealth()
    {
        if (BossOrPlayer.name == "Player")
        {
            current = BossOrPlayer.GetComponent<playermovement>().getHealth();
            HealthBar.slider.value = current;

        }
        else if (BossOrPlayer.name == "OCD_Monster")
        {
            //Debug.Log("In Here");
            current = BossOrPlayer.GetComponent<Boss>().getHealth();
            HealthBar.slider.value = current;
        }

        HealthBar.fill.color = HealthBar.gradient.Evaluate(HealthBar.slider.normalizedValue);
    }
}
