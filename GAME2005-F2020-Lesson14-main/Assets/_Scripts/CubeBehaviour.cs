using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;


[System.Serializable]
public class CubeBehaviour : MonoBehaviour
{
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public bool isColliding;
    public bool debug;
    public List<CubeBehaviour> contacts;
    public List<SphereBehaviour> sphereContacts;
    public Cube cube;
    public PlayerBehaviour player;

    private MeshFilter meshFilter;
    private Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        debug = false;
        meshFilter = GetComponent<MeshFilter>();
        
        cube = GetComponent<Cube>();
        player = GetComponent<PlayerBehaviour>();

        bounds = meshFilter.mesh.bounds;
        size = bounds.size;

    }

    // Update is called once per frame
    void Update()
    {
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
    }

    void FixedUpdate()
    {
        // physics related calculations
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireCube(transform.position, Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), transform.localScale));
        }
    }
   
    public void Bounce(CubeBehaviour box)
    {
        var dx = box.transform.position.x - transform.position.x;
        var dy = box.transform.position.y - transform.position.y;
        var dz = box.transform.position.z - transform.position.z;


        if(Mathf.Abs(dx)> Mathf.Abs(dy) &&  Mathf.Abs(dx)>  Mathf.Abs(dz)) // dx is the biggest
        {
            cube.velocity.x *= -1;
            if(dx < 0)
            {
                transform.position = new Vector3(box.max.x + 0.6f, transform.position.y,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(box.min.x - 0.6f, transform.position.y,transform.position.z);
            }
        }
        else if (Mathf.Abs(dy) > Mathf.Abs(dx) &&  Mathf.Abs(dy) >  Mathf.Abs(dz)) // dy is the biggest
        {
            cube.velocity.y *= -1;
        }else // dz is the biggest
        {
            cube.velocity.z *= -1;
            if(dz < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,box.max.z + 0.6f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,box.min.z - 0.6f);
            }
        }
        
    }

     public void CheckBorders(CubeBehaviour box)
    {
        var dx = box.transform.position.x - transform.position.x;
        var dy = box.transform.position.y - transform.position.y;
        var dz = box.transform.position.z - transform.position.z;


        if(Mathf.Abs(dx)> Mathf.Abs(dy) &&  Mathf.Abs(dx)>  Mathf.Abs(dz)) // dx is the biggest
        {
            if(dx < 0)
            {
                transform.position = new Vector3(box.max.x + 0.5f, transform.position.y,transform.position.z);
            }
            else
            {
                transform.position = new Vector3(box.min.x - 0.5f, transform.position.y,transform.position.z);
            }
        }
        else if (Mathf.Abs(dy) > Mathf.Abs(dx) &&  Mathf.Abs(dy) >  Mathf.Abs(dz)) // dy is the biggest
        {  
        }else // dz is the biggest
        {
            if(dz < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,box.max.z + 0.5f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,box.min.z - 0.5f);
            }
        }
        
    }

    public void CalculateMomentum(CubeBehaviour box)
    {
         Vector3 lostMomentum = cube.momentum * 0.1f;
        cube.momentum -= lostMomentum;
        box.cube.momentum += lostMomentum;

        cube.velocity = cube.momentum / cube.mass;
        box.cube.velocity = box.cube.momentum / box.cube.mass;
    }
}
