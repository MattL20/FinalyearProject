using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    //controls when the player enters the spawn trigger area and the calls the spawning of the boss
    public event EventHandler OnPlayerEnterTrigger;
    private void OnTriggerEnter2D(Collider2D collider) {
        playermovement player = collider.GetComponent<playermovement>();
        player.enabled = false;
        if(player != null){
            Debug.Log("In Trigger");
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
