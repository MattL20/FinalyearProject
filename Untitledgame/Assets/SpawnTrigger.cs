using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public event EventHandler OnPlayerEnterTrigger;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider) {
        playermovement player = collider.GetComponent<playermovement>();
        if(player != null){
            Debug.Log("In Trigger");
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
