using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] Transform gunPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform player;

    [SerializeField] float fireCooldown;
    [SerializeField] float _fireCooldown;

    [SerializeField] float turretRange = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        _fireCooldown = 2;
        fireCooldown = 3;
    }

    public void AimAtPlayer()
    {
        turret.transform.LookAt(player);
        if (turret.transform.localEulerAngles.x < 360.0f)
        {
            if (turret.transform.localEulerAngles.x < 345.0f && turret.transform.localEulerAngles.x > 180.0f)
            {
                turret.transform.localEulerAngles = new Vector3(345.0f, turret.transform.localEulerAngles.y, turret.transform.localEulerAngles.z);
            }
            else if(turret.transform.localEulerAngles.x > 10.0f && turret.transform.localEulerAngles.x <= 180.0f)
            {
                turret.transform.localEulerAngles = new Vector3(10.0f, turret.transform.localEulerAngles.y, turret.transform.localEulerAngles.z);
            }
        }
    }

    public void FireBullet()
    {
        if (fireCooldown <= 0)
        {
            Instantiate(bullet, gunPoint.position, gunPoint.rotation);
            fireCooldown = _fireCooldown;
        }
        else
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    public bool InRange()
    {
        return ((player.position - transform.position).magnitude < turretRange);
    }
}
