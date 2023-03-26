using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject Barrel;
    public GameObject HalfBrokenBarrel;
    public AudioSource punch;
    void Start()
    {
        this.Barrel.transform.position = transform.position;
        maxHealth = 60;
        currentHealth = maxHealth;
    }
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
    private void OnDestroy() {
         Instantiate(HalfBrokenBarrel,Barrel.transform);
    }
}
