using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    //Max health and current health variables
    public int maxHealth;
    public int currentHealth;
    public GameObject Barrel;
    public GameObject HalfBrokenBarrel;
    public AudioSource punch;
    //Set this barrel position as the objects position
    void Start()
    {
        this.Barrel.transform.position = transform.position;
        maxHealth = 60;
        currentHealth = maxHealth;
    }
    //Take in a damage value and take it from the health 
    //if health reaches a certain value destroy this barrel
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth > 20)
        {
            punch.Play();
        }
        if(currentHealth<=20){
            Instantiate(HalfBrokenBarrel,this.transform.position, this.transform.rotation);
            Destroy(Barrel);
        }
    }
    // when destroyed instantiate a halfbrokenbarrel to the posistion of the barrel destroyed
    private void OnDestroy() {
         Instantiate(HalfBrokenBarrel,Barrel.transform);
    }
}
