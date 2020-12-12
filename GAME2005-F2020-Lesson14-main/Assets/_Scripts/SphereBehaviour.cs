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
    public float EnergyLostCoeficient;

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
        float offSet = 0.06f;

        if(Mathf.Abs(dx)> Mathf.Abs(dy) &&  Mathf.Abs(dx)>  Mathf.Abs(dz)) // dx is the biggest
        {
            
            bulletBehaviour.velocity.x *= -1;
             if(dx < 0)
            {
                transform.position = new Vector3(box.max.x + offSet, transform.position.y,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(box.min.x - offSet, transform.position.y,transform.position.z);
            }
        }
        else if (Mathf.Abs(dy) > Mathf.Abs(dx) &&  Mathf.Abs(dy) >  Mathf.Abs(dz)) // dy is the biggest
        {
            bulletBehaviour.velocity.y *= -1;
            if(dy < 0)
            {
                transform.position = new Vector3(transform.position.x, box.max.y + offSet,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, box.min.y - offSet,transform.position.z);
            }
        }else // dz is the biggest
        {
            bulletBehaviour.velocity.z *= -1;
            if(dz < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,box.max.z + offSet);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,box.min.z - offSet);
            }
        }
        
    }

    public void LoseMomentum()
    {
        Vector3 lostMomentum = bulletBehaviour.momentum * EnergyLostCoeficient;
        bulletBehaviour.momentum -= lostMomentum;
        bulletBehaviour.velocity = bulletBehaviour.momentum / bulletBehaviour.mass;
    }
    public void CalculateMomentum(CubeBehaviour box)
    {
        Vector3 lostMomentum = bulletBehaviour.momentum * EnergyLostCoeficient;
        box.cube.momentum += lostMomentum;
        box.cube.velocity = box.cube.momentum / box.cube.mass;
    }
}
