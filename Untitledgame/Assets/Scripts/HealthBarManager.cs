using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//used to control the health bar 
public class HealthBarManager : MonoBehaviour
{
    public GameObject BossOrPlayer;
    public HealthBar HealthBar;
    private int max;
    private int current;
    public GameObject winningScreen; 
    //set the max health of the health bar.
    private void Awake()
    {
        SetMaxHealth();
    }
    //set the health each frame
    private void Update()
    {
        SetHealth();
        //if the boss dies and the player is alive show the win screen
        if(BossOrPlayer.name == "OCD_Monster")
        {
            if (BossOrPlayer.GetComponent<Boss>().getHealth() == 0 && BossOrPlayer.GetComponent<Boss>().getIsPlayerAlive())
            {
                StartCoroutine(winScreen());
            }
        }
    }
    //wait a second and then stop everything in game and activate the win screen
    IEnumerator winScreen()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        winningScreen.SetActive(true);
    }
    //setting the max health what ever character the health bar is on
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
    //setting the health what ever character the health bar is on
    public void SetHealth()
    {
        if (BossOrPlayer.name == "Player")
        {
            current = BossOrPlayer.GetComponent<playermovement>().getHealth();
            HealthBar.slider.value = current;

        }
        else if (BossOrPlayer.name == "OCD_Monster")
        {
            current = BossOrPlayer.GetComponent<Boss>().getHealth();
            HealthBar.slider.value = current;
        }

        HealthBar.fill.color = HealthBar.gradient.Evaluate(HealthBar.slider.normalizedValue);
    }
}
