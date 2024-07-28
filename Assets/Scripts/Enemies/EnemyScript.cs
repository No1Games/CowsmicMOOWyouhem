using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private float maxHP;
    [SerializeField] private float defense;
    private float currentHP;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackSpeed;

    List<TypeOfDamage> damageList = new List<TypeOfDamage>();
    
    [SerializeField] TypeOfEnemy type;


    GameObject target;
    NavMeshAgent agent;
    SceneControllerScript sceneController;

    private Coroutine attackCoroutine;
    private bool isAttacking;

    void Awake()
    {
        currentHP = maxHP; 
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        sceneController = GameObject.Find("SceneController").GetComponent<SceneControllerScript>();
        
    }
    public void TakeDamage(float incomeDamage, TypeOfDamage damageType)
    {
        damageList.Add(damageType);
        float reflectedDamage = defense / 100 * incomeDamage;
        float damage = incomeDamage - reflectedDamage;
        if(currentHP > damage)
        {
            currentHP -= damage;

        }
        else
        {
            currentHP = 0;

            sceneController.AddKill(type, new List<TypeOfDamage>(damageList));
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
            script.TakeDamage(attackDamage, type);
            yield return new WaitForSeconds(attackSpeed);
        }
        
            

    }

    void FixedUpdate()
    {
        PlayerFollow();
    }


}
