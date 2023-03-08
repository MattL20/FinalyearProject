using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject Barrel;
    public GameObject HalfBrokenBarrel;
    // Start is called before the first frame update
    void Start()
    {
        this.Barrel.transform.position = transform.position;
        maxHealth = 60;
        currentHealth = maxHealth;
        //Debug.Log("Pos = " + Barrel.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int dmg)
    {
        //Debug.Log("this.Barrel = " + this.transform.position);
        currentHealth -= dmg;
        if(currentHealth<=20){
            Instantiate(HalfBrokenBarrel,this.transform.position, this.transform.rotation);
            //Debug.Log("Pos = " + HalfBrokenBarrel.transform.position);
           // HalfBrokenBarrel.GetComponent<HalfBrokenBarrel>().maxHealth = currentHealth;
            Destroy(Barrel);
        }
    }
    private void OnDestroy() {
         Instantiate(HalfBrokenBarrel,Barrel.transform);
    }
}
