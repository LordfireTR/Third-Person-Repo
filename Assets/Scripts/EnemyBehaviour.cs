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
    [SerializeField] float turretLowerLimit = 345.0f;
    [SerializeField] float turretUpperLimit = 10.0f;
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
        
        
        if (compass.localEulerAngles.x < turretUpperLimit && compass.localEulerAngles.x > 180.0f)
        {
            turret.localEulerAngles = new Vector3(turretUpperLimit, compass.localEulerAngles.y, compass.localEulerAngles.z);
        }
        else if(compass.localEulerAngles.x > turretLowerLimit && compass.localEulerAngles.x <= 180.0f)
        {
            turret.localEulerAngles = new Vector3(turretLowerLimit, compass.localEulerAngles.y, compass.localEulerAngles.z);
        }
        
    }

    public void FireBullet()
    {
        if (fireCooldown <= 0)
        {
            Quaternion gunPointBase = gunPoint.rotation;
            Vector3 gunInaccuracy = GaussianRandom(); 
            gunPoint.localEulerAngles += gunInaccuracy;
            Instantiate(bullet, gunPoint.position, gunPoint.rotation);
            fireCooldown = _fireCooldown;
            gunPoint.rotation = gunPointBase;
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

    private Vector3 GaussianRandom()
    {
        float rand1 = Random.Range(0.05f, 1.0f);
        float rand2 = Random.Range(0.05f, 1.0f);

        float gaussianX = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2 * Mathf.PI) * rand2);
        float gaussianY = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Sin((2 * Mathf.PI) * rand2);

        return new Vector3(gaussianX, gaussianY, 0);
    }
}
