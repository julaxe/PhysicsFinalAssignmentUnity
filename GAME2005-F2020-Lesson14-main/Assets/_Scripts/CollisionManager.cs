using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public CubeBehaviour[] actors;
    public SphereBehaviour[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        actors = FindObjectsOfType<CubeBehaviour>();
        
    }

    // Update is called once per frame
    void Update()
    {
        bullets = FindObjectsOfType<SphereBehaviour>();

         for (int i = 0; i < actors.Length; i++)
            {
                actors[i].isColliding = false;
            }
          for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].isColliding = false;
            }

        //Collision Between Boxes;
        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                if (i != j)
                {
                    if(CheckAABBs(actors[i], actors[j]))
                    {

                       if(actors[i].cube != null)
                       {
                            if(actors[j].cube != null)
                            {
                                actors[i].CalculateMomentum(actors[j]);
                            }
                            actors[i].Bounce(actors[j]);
                       }
                       if(actors[i].player != null)
                        {
                            actors[i].CheckBorders(actors[j]);
                        }
                       
                    }

                }
            }
        }
        //Collision Bullet and Boxes
        for (int i = 0; i < bullets.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                if(CheckAABBSphereBox(actors[j], bullets[i]))
                {
                    if(actors[j].player != null)
                    {
                        break;
                    }

                    if (actors[j].cube != null)
                    {
                        bullets[i].CalculateMomentum(actors[j]);
                    }


                    bullets[i].LoseMomentum();
                    bullets[i].Bounce(actors[j]);
                    break;
                    
                }
            }
        }


       
    }
    public static bool CheckAABBs(CubeBehaviour a, CubeBehaviour b)
    {
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
            }
            a.isColliding = true;
            return true;
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
            }
           return false;
        }
    }

    public static bool CheckAABBSphereBox(CubeBehaviour box, SphereBehaviour sphere)
    {
        var x = Mathf.Max(box.min.x, Mathf.Min(sphere.transform.position.x, box.max.x));
        var y = Mathf.Max(box.min.y, Mathf.Min(sphere.transform.position.y, box.max.y));
        var z = Mathf.Max(box.min.z, Mathf.Min(sphere.transform.position.z, box.max.z));
        
        var distance = Mathf.Sqrt((x - sphere.transform.position.x)*(x - sphere.transform.position.x) +
                                   (y - sphere.transform.position.y)*(y - sphere.transform.position.y) + 
                                   (z - sphere.transform.position.z)*(z - sphere.transform.position.z));

        if(distance < sphere.Radius)
        {
            box.isColliding = true;
            sphere.isColliding = true;
            return true;
        }
        return false;
    }

}
