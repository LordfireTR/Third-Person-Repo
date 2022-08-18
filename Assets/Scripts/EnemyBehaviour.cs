using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Transform turret;
    [SerializeField] Transform compass;
    [SerializeField] Transform gunPoint;
    [SerializeField] Transform player;
    [SerializeField] GameObject bullet;

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
        compass.LookAt(player);
        turret.rotation = Quaternion.RotateTowards(turret.rotation, compass.rotation, 1.0f);
        
        if (turret.localEulerAngles.x < 360.0f)
        {
            if (turret.localEulerAngles.x < 345.0f && turret.localEulerAngles.x > 180.0f)
            {
                turret.localEulerAngles = new Vector3(345.0f, turret.localEulerAngles.y, turret.localEulerAngles.z);
            }
            else if(turret.localEulerAngles.x > 10.0f && turret.localEulerAngles.x <= 180.0f)
            {
                turret.localEulerAngles = new Vector3(10.0f, turret.localEulerAngles.y, turret.localEulerAngles.z);
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

    public void IdlePose()
    {
        compass.localEulerAngles = Vector3.zero;
        turret.rotation = Quaternion.RotateTowards(turret.rotation, compass.rotation, 1.0f);
    }
}
