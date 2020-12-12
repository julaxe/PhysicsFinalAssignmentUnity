using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    public Vector3 momentum;
    public float mass;
    public Vector3 velocity;
    public float friction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity -= velocity*(friction* Time.deltaTime);
        momentum = velocity*mass;
        transform.position += new Vector3(velocity.x, 0.0f , velocity.z)* Time.deltaTime;
    }
}
