using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBrokenBarrel : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject Boss;
    public GameObject HalfBarrel;
    public GameObject BrokenBarrel;
    public AudioSource punch;
    // Start is called before the first frame update
    void Start()
    {
        this.HalfBarrel.transform.position = transform.position;
        this.BrokenBarrel.transform.position = transform.position;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int dmg)
    {
        //Debug.Log("this.HalfBrokenBarrel = " + this.transform.position);
        currentHealth -= dmg;
        if (currentHealth > 0)
        {
            punch.Play();
        }
        if (currentHealth<=0){
            Instantiate(this.BrokenBarrel,this.HalfBarrel.transform.position, this.HalfBarrel.transform.rotation);
            //Debug.Log("HELP = " + this.BrokenBarrel.transform.position);
            Boss.GetComponent<Boss>().setBarrel(this.BrokenBarrel);
            
            Destroy(HalfBarrel);
        }
    }
    private void OnDestroy() {
         Instantiate(BrokenBarrel,HalfBarrel.transform);
    }
}
