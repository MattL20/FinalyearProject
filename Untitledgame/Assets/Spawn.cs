using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Barrels;
    // Start is called before the first frame update
    void Start()
    {
        SpawnAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnAll()
    {
        Instantiate(Boss, new Vector3(-1, 18, 0), Boss.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[1].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[4].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[7].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[10].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[13].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[16].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[19].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[22].position, Barrels.transform.rotation);
        Debug.Log("Everything Spawned");
    }
}
