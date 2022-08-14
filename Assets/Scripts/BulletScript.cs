using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletSpeed = 20;
    float bulletDuration = 5.0f;

    void Start()
    {
        StartCoroutine(BulletDuration());
    }

    // Update is called once per frame
    void Update()
    {
        BulletTravel();
    }

    void BulletTravel()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    IEnumerator BulletDuration()
    {
        yield return new WaitForSeconds(bulletDuration);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().isGameOver = true;
        }
        else
        {
            other.gameObject.SetActive(false);
        }
    }
}
