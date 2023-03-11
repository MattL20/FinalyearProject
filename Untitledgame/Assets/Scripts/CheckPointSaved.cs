using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSaved : MonoBehaviour
{
    public GameObject Player;
    public Vector3 pos;
    public float rotx;
    public float roty;
    public float rotz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player"){
            pos = Player.transform.position;
            rotx = Player.transform.rotation.x;
            roty = Player.transform.rotation.y;
            rotz = Player.transform.rotation.z;
        }
    }
    
}
