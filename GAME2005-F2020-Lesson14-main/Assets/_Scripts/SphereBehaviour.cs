using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    public Vector3 max;
    public Vector3 min;
    public bool isColliding;
    public bool debug;
    public List<CubeBehaviour> contacts;
    public BulletBehaviour bulletBehaviour;

    private MeshFilter meshFilter;
    public float Radius;
    public Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {

        bulletBehaviour = GetComponent<BulletBehaviour>();
        debug = false;
        meshFilter = GetComponent<MeshFilter>();

        bounds = meshFilter.mesh.bounds;
        //Radius = bounds.extents.magnitude * transform.localScale.x;
        Radius = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if(contacts.Count > 0)
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
    }

     private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireSphere(transform.position, Radius);

        }
    }

    public void Bounce(CubeBehaviour box)
    {
        var dx = box.transform.position.x - transform.position.x;
        var dy = box.transform.position.y - transform.position.y;
        var dz = box.transform.position.z - transform.position.z;


        if(Mathf.Abs(dx)> Mathf.Abs(dy) &&  Mathf.Abs(dx)>  Mathf.Abs(dz)) // dx is the biggest
        {
            bulletBehaviour.velocity.x *= -1;
        }
        else if (Mathf.Abs(dy) > Mathf.Abs(dx) &&  Mathf.Abs(dy) >  Mathf.Abs(dz)) // dy is the biggest
        {
            bulletBehaviour.velocity.y *= -1;
        }else // dz is the biggest
        {
            bulletBehaviour.velocity.z *= -1;
        }
        
    }

    public void CalculateMomentum(CubeBehaviour box)
    {
         Vector3 lostMomentum = bulletBehaviour.momentum * 0.1f;
        bulletBehaviour.momentum -= lostMomentum;
        box.cube.momentum += lostMomentum;

        bulletBehaviour.velocity = bulletBehaviour.momentum / bulletBehaviour.mass;
        box.cube.velocity = box.cube.momentum / box.cube.mass;
    }
}
