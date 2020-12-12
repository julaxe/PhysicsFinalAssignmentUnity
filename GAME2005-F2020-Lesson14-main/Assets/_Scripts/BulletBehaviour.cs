using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public float range;
    public Vector3 momentum;
    public float mass;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        mass = 1.0f;
        velocity = direction * speed;
        momentum = velocity* mass;
    }

    // Update is called once per frame
    void Update()
    {
        
        _Move();
        _CheckBounds();
        momentum = velocity* mass;
       
    }

    private void OnEnable()
    {
        velocity = direction * speed;
        momentum = velocity* mass;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void _Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if ((Vector3.Distance(transform.position, Vector3.zero) > range) || (momentum.magnitude < 0.1))
        {
            Disable();
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}