using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject Player;
    public GameObject Boss;
    public GameObject Checkpoint;
    private Vector3 pos;
    private float x;
    private float y;
    private float z;
    public GameObject spawn;
    public void Died(){
        if (Player.GetComponent<playermovement>().getHealth() == 0 && Player.GetComponent<playermovement>().getIsBossAlive())
        {
            DeathScreen.SetActive(true);
            var BossClone = GameObject.FindGameObjectsWithTag("Boss");
            foreach (var bar in BossClone)
            {
                bar.GetComponent<Boss>().enabled = false;
            }

            Player.GetComponent<playermovement>().animator.SetFloat("Speed", 0);
            Player.GetComponent<playermovement>().enabled = false;
            Player.GetComponent<PlayerInput>().enabled = false;
            Player.GetComponent<Attack>().enabled = false;
            var Barrels = GameObject.FindGameObjectsWithTag("Barrel");
            var BrokenBarrel = GameObject.FindGameObjectsWithTag("BrokenBarrel");
            var HalfBrokenBarrel = GameObject.FindGameObjectsWithTag("HalfBrokenBarrel");
            foreach (var bar in Barrels)
            {
                Destroy(bar);
            }
            foreach (var bar in BrokenBarrel)
            {
                Destroy(bar);
            }
            foreach (var bar in HalfBrokenBarrel)
            {
                Destroy(bar);
            }
        }
    }
    public void Retry(){
        DeathScreen.SetActive(false);
        pos = Checkpoint.GetComponent<CheckPointSaved>().pos;
        Player.GetComponent<playermovement>().respawnPlayer(pos);
        Player.GetComponent<playermovement>().enabled = true;
        Player.GetComponent<PlayerInput>().enabled = true;
        Player.GetComponent<Attack>().enabled = true;
        var BossClone = GameObject.FindGameObjectsWithTag("Boss");
        foreach (var bar in BossClone)
        {
            Destroy(bar);
        }
        spawn.GetComponent<Spawn>().SpawnAgain();
        StartCoroutine(BossSpawn());

    }
    IEnumerator BossSpawn()
{
    yield return new WaitForSeconds(0.5f);
    Player.GetComponent<playermovement>().resetHealth();
    yield return new WaitForSeconds(2);
    Instantiate(Boss, new Vector3(-1, 18, 0), Boss.transform.rotation);
}
}
