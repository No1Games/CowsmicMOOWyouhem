using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    PlayersInput control;
    
    [Header("Shooting parameters")]
    
    [SerializeField] string bulletPrefabName;
    [SerializeField] float fireRate;
    [SerializeField] private float bulletDamage;
    [SerializeField] float bulletSpeed = 1;
    [SerializeField] float bulletRange = 1;

    private Coroutine shootingCoroutine;
    private bool isShooting;
   
    

    private void Awake()
    {
        control = new PlayersInput();

        int playerLayer = LayerMask.NameToLayer("Player");
        int bulletLayer = LayerMask.NameToLayer("Bullet");
        Physics.IgnoreLayerCollision(playerLayer, bulletLayer);

    }
    private void OnEnable()
    {
        control.GameInput.Enable();
        control.GameInput.Shot.performed += _ => StartShooting();
        control.GameInput.Shot.canceled += _ => StopShooting();
    }

    private void StartShooting()
    {
        if(shootingCoroutine == null)
        {
            isShooting = true;
            shootingCoroutine = StartCoroutine(ShootingCoroutine());
            
        }
        
    }
    private void StopShooting()
    {
        isShooting = false;
              
    }

    IEnumerator ShootingCoroutine()
    {
        while (isShooting)
        {
        Shot();
        yield return new WaitForSeconds(fireRate);
        }
        shootingCoroutine = null;

    }
    
        
    void Shot()
    {
        
        GameObject bullet = GenerateBullet();
        BulletScript bulletS = bullet.GetComponent<BulletScript>();
        bulletS.Initialize(bulletRange, bulletDamage);
       
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);


    }

    GameObject GenerateBullet()
    {
        GameObject bulletPref = Resources.Load<GameObject>(bulletPrefabName);
        Vector3 spawnPoint = transform.position;
        GameObject bullet = Instantiate(bulletPref, spawnPoint, Quaternion.identity);
        return bullet;
    }



    private void OnDisable()
    {
        control.GameInput.Disable();
    }
}
