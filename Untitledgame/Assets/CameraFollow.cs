using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public float FollowSpeed = 2f;
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 newPos = new UnityEngine.Vector3(Target.position.x, Target.position.y, -10f);
        transform.position = UnityEngine.Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
