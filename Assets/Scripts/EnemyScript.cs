using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxHP = 100;
    private float currentHP;
    void Awake()
    {
        currentHP = maxHP; 
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
            Destroy(gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
