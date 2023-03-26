using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBarrel : MonoBehaviour
{
    public GameObject Boss;
    public GameObject BroBarrel;

    void Start()
    {
        this.BroBarrel.transform.position = this.transform.position;
        Boss.GetComponent<Boss>().countBarrel();
        Boss.GetComponent<Boss>().setFix();
    }
}
