using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoWeaponScript : MonoBehaviour
{
    [SerializeField] float autoAttackRange = 15;
    [SerializeField] float autoAttackSpeed = 100;
    [SerializeField] float autoAttackDamage = 10;
    [SerializeField] float fireRate = 1f;
    string bulletPrefabName = "Bullet";
    string enemyLayerName = "Enemy";
    

    private int enemyLayer;
    
    private Coroutine shootingCoroutine;
    private bool isShooting;

    private void Awake()
    {
        enemyLayer = LayerMask.NameToLayer(enemyLayerName);
    }

    void Update()
    {
        GameObject target = ChooseTarget();
        
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position);
            StartShooting(direction);
        }
        else
        {
            StopShooting();
        }
        
    }

    
    private Collider FindNearestEnemy()
    {
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, autoAttackRange);
        Collider nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        

        foreach (Collider enemy in enemiesAround)
        {
            if (enemy.gameObject.layer == enemyLayer)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < shortestDistance)
                {
                    
                    shortestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                    nearestEnemy = enemy;
                    
                }

            }

        }
        return nearestEnemy;
    }
    private GameObject ChooseTarget()
    {
        Collider nearestEnemy = FindNearestEnemy();
        GameObject target = null;
        if (nearestEnemy != null)
        {
            
            Vector3 directionToEnemy = (nearestEnemy.gameObject.transform.position - transform.position);
            RaycastHit hit;
            Ray ray = new Ray(transform.position, directionToEnemy);

            if (Physics.Raycast(ray, out hit, autoAttackRange) && hit.collider.gameObject.layer == enemyLayer)
            {
                target = hit.collider.gameObject;
            }
        }
        return target;
    }

    private void StartShooting(Vector3 direction)
    {
        if (shootingCoroutine == null)
        {
            isShooting = true;
            shootingCoroutine = StartCoroutine(ShootingCoroutine(direction));

        }

    }
    private void StopShooting()
    {
        isShooting = false;

    }

    IEnumerator ShootingCoroutine(Vector3 shootingDirection)
    {
        
        while (isShooting)
        {
            Shot(shootingDirection);
            yield return new WaitForSeconds(fireRate);
        }
        shootingCoroutine = null;

    }
    void Shot(Vector3 shotDirection)
    {

        GameObject bullet = GenerateBullet();
        BulletScript bulletS = bullet.GetComponent<BulletScript>();
        bulletS.Initialize(autoAttackRange, autoAttackDamage);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(shotDirection * autoAttackSpeed, ForceMode.Impulse);


    }

    GameObject GenerateBullet()
    {
        
        GameObject bulletPref = Resources.Load<GameObject>(bulletPrefabName);
        Vector3 spawnPoint = transform.position;
        GameObject bullet = Instantiate(bulletPref, spawnPoint, Quaternion.identity);
        return bullet;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, autoAttackRange);
    }
}
