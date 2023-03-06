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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int dmg)
    {

        currentHealth -= dmg;
        if(currentHealth<=0){
            Instantiate(BrokenBarrel,HalfBarrel.transform.position, HalfBarrel.transform.rotation);
            Debug.Log("Pos = " + BrokenBarrel.transform.position);
            Boss.GetComponent<Boss>().setBarrel(BrokenBarrel);
            
            Destroy(HalfBarrel);
        }
    }
    private void OnDestroy() {
         Instantiate(BrokenBarrel,HalfBarrel.transform);
    }
}
