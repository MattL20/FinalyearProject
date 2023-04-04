using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
//Used to get the main camera to follow the 
public class CameraFollow : MonoBehaviour
{
    //speed at which the camera moves
    public float FollowSpeed = 2f;
    //reference to the player position
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 newPos = new UnityEngine.Vector3(Target.position.x, Target.position.y, -10f);
        transform.position = UnityEngine.Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);//move the camera to the player
    }
}
