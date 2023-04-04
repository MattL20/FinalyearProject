using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBrokenBarrel : MonoBehaviour
{
    //health variables
    public int maxHealth;
    public int currentHealth;
    //reference to the boss, half broken barrel and broken barrel objects and prefabs
    public GameObject Boss;
    public GameObject HalfBarrel;
    public GameObject BrokenBarrel;
    //sound of punching the barrels
    public AudioSource punch;
    // Start is called before the first frame update
    void Start()
    {
        //set the positions in for each barrels seperately
        this.HalfBarrel.transform.position = transform.position;
        this.BrokenBarrel.transform.position = transform.position;
        currentHealth = maxHealth;
    }
    //take in a damage value and take away from currenthealth
    //if health reaches zero destroy the half barrel
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth > 0)
        {
            punch.Play();
        }
        if (currentHealth<=0){
            Instantiate(this.BrokenBarrel,this.HalfBarrel.transform.position, this.HalfBarrel.transform.rotation);
            Boss.GetComponent<Boss>().setBarrel(this.BrokenBarrel);
            Destroy(HalfBarrel);
        }
    }
    //spawn the broken barrel when half barrel is destroyed
    private void OnDestroy() {
         Instantiate(BrokenBarrel,HalfBarrel.transform);
    }
}
