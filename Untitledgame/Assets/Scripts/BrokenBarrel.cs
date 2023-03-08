using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBarrel : MonoBehaviour
{
    public GameObject Boss;
    public GameObject BroBarrel;
    private bool test = true;
    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        this.BroBarrel.transform.position = this.transform.position;
        //Debug.Log("this.BrokenBarrel = " + this.transform.position);
        Boss.GetComponent<Boss>().countBarrel();
        Boss.GetComponent<Boss>().setFix();
        //Debug.Log("*********************************************************************************************************************************************************************** ");
    }

    // Update is called once per frame
    void Update()
    {
        //Boss.GetComponent<Boss>().setFix(test);
    }
}
