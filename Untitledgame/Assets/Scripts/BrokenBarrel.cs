using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBarrel : MonoBehaviour
{
    //reference to the boss
    public GameObject Boss;
    //reference to the broken barrel object this script is on
    public GameObject BroBarrel;

    
    void Start()
    {
        this.BroBarrel.transform.position = this.transform.position;//set this brobarrel position as the position of the broken barrel
        Boss.GetComponent<Boss>().countBarrel();//call countBarrel in the boss script
        Boss.GetComponent<Boss>().setFix();//call setFix in the boss script
    }
}
