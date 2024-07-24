//Viktor's check

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 startPosition;
    private float maxDistance;
    private float bulletDamage;
    private TypeOfDamage damageType = TypeOfDamage.MainWeapon;

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
                enemy.TakeDamage(bulletDamage, damageType);
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Obstacle"))
                {
                    Destroy(gameObject);
                }
                
                    
           
        }
        
    }
}
