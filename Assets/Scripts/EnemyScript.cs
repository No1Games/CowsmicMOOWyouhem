using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    
    private float maxHP = 100;
    private float currentHP;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackPace;
    
    GameObject target;
    NavMeshAgent agent;
    GameObject sceneController;

    private Coroutine attackCoroutine;
    private bool isAttacking;

    void Awake()
    {
        currentHP = maxHP; 
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        sceneController = GameObject.Find("SceneController");
    }
    public void TakeDamage(float damage)
    {
        if(currentHP > damage)
        {
            currentHP -= damage;

        }
        else
        {
            currentHP = 0;
            SceneControllerScript script = sceneController.GetComponent<SceneControllerScript>();
            script.currentEnemiesAmount--;
            Destroy(gameObject);
        }
        
    }
    
    private void PlayerFollow()
    {
        Vector3 targetPosition = target.transform.position;
        agent.SetDestination(targetPosition);


    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScript script = collision.gameObject.GetComponent<PlayerScript>();
            if(!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(MeleAttack(script));
            }
               
        }

    }
    private void OnCollisionExit (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                isAttacking = false;
            }
        }
    } 

    IEnumerator MeleAttack(PlayerScript script)
    {
        while (true)
        {
            script.TakeDamage(attackDamage);
            yield return new WaitForSeconds(attackPace);
        }
        
            

    }

    void FixedUpdate()
    {
        PlayerFollow();
    }


}
