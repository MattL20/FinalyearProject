using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private enum State{
        Idle,
        Active,
    }
    public GameObject Camera;
    public GameObject Boss;
    public GameObject Barrels;
    public GameObject Player;
    public SpawnTrigger trigger;
    public float FollowSpeed = 10f;

    private float _waitTime = 3f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private bool camMove = false;

    private State state;
    private void Awake() {
        state = State.Idle;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[1].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[4].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[7].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[10].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[13].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[16].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[19].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[22].position, Barrels.transform.rotation);
        
        trigger.OnPlayerEnterTrigger += SpawnTrigger_OnPlayerEnterTrigger;
    }
    private void SpawnTrigger_OnPlayerEnterTrigger(object sender, EventArgs e){
        if(state == State.Idle){
        SpawnAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("FollowSpeed  " + FollowSpeed);
        if(camMove){
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, new Vector3(-1, 18, -10), FollowSpeed * Time.deltaTime);
        }
    }
    private void SpawnAll()
    {
        Camera.GetComponent<CameraFollow>().enabled = false;
        Player.GetComponent<playermovement>().animator.SetFloat("Speed",0);
        Player.GetComponent<playermovement>().enabled = false;
        StartCoroutine(WaitForFunction());
        camMove = true;
        state = State.Active;
       // Debug.Log("Everything Spawned");
    }
    IEnumerator WaitForFunction()
{
    yield return new WaitForSeconds(8);
    Instantiate(Boss, new Vector3(-1, 18, 0), Boss.transform.rotation); 
    yield return new WaitForSeconds(2);
    Camera.GetComponent<CameraFollow>().enabled = true;
    Player.GetComponent<playermovement>().enabled = true;

    camMove = false;
}
}
