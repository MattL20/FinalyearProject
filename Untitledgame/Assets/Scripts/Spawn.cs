using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Controls what spawns when the boss fight is started
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
    public GameObject bossHealthBar;

    
    private bool camMove = false;

    private State state;
    private void Awake() {
        state = State.Idle;
    }
    // Start is called before the first frame update
    void Start()
    {
        //spawn all barrels in appropriate spot
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
    //used to only spawn everything once 
    private void SpawnTrigger_OnPlayerEnterTrigger(object sender, EventArgs e){
        if(state == State.Idle){
        SpawnAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //pans camera to the boss when spawning
        if(camMove){
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, new Vector3(-1, 18, -10), FollowSpeed * Time.deltaTime);
        }
    }
    // moves camera and calls the spawn of the boss
    private void SpawnAll()
    {
        Camera.GetComponent<CameraFollow>().enabled = false;
        Player.GetComponent<playermovement>().animator.SetFloat("Speed",0);
        Player.GetComponent<playermovement>().enabled = false;
        StartCoroutine(WaitForFunction());
        camMove = true;
        state = State.Active;
    }
    //spawns the boss and then moves the camera back and allows movement aswell
    IEnumerator WaitForFunction()
{
    yield return new WaitForSeconds(8);
    Instantiate(Boss, new Vector3(-1, 18, 0), Boss.transform.rotation);
    yield return new WaitForSeconds(2);
    bossHealthBar.SetActive(true);
    Camera.GetComponent<CameraFollow>().enabled = true;
    Player.GetComponent<playermovement>().enabled = true;

    camMove = false;
}
    //used if the player retries to spawn all barrels new again
public void SpawnAgain(){
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[1].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[4].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[7].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[10].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[13].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[16].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[19].position, Barrels.transform.rotation);
        Instantiate(Barrels, Boss.GetComponent<Boss>().waypoints[22].position, Barrels.transform.rotation);
}
}
