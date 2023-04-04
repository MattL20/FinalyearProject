using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Saves the position of the player at a checkpoint so they can retry the game if they die
public class CheckPointSaved : MonoBehaviour
{
    public GameObject Player;
    public Vector3 pos;
    public float rotx;
    public float roty;
    public float rotz;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player"){
            pos = Player.transform.position;
            rotx = Player.transform.rotation.x;
            roty = Player.transform.rotation.y;
            rotz = Player.transform.rotation.z;
        }
    }
    
}
