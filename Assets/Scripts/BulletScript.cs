using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 startPosition;
    private float maxDistance;
    private float bulletDamage;

    public void Initialize(float maxDistance, float bulletDamage)
    {
        this.maxDistance = maxDistance;
        this.bulletDamage = bulletDamage;
        startPosition = transform.position;
    }

    void Update()
    {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
                enemy.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
