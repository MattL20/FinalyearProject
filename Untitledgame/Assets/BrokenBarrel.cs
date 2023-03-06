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
       Debug.Log("JAIUHISAUDFIASJDAJSD PEDJHFBVESHFPIUWEHFBOAIUDHFBIUECHOIBAUEWHFBFAIWCBOFEWFOIUAWEFOIUDG");
    }
    void Start()
    {
        
        Boss.GetComponent<Boss>().setFix();
    }

    // Update is called once per frame
    void Update()
    {
        //Boss.GetComponent<Boss>().setFix(test);
    }
}
