using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    void start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
    }

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {
                GameObject obj = ObjectPooling.current.getPooledObject();
                if(obj == null) return;

                obj.transform.position = bulletSpawn.position;
                obj.GetComponent<BulletBehaviour>().direction = bulletSpawn.forward;
                obj.SetActive(true);


                //var tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                //tempBullet.GetComponent<BulletBehaviour>().direction = bulletSpawn.forward;

                //tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }

        }
    }
}
