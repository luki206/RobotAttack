using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    } 
    
    public void TakeDamage(int damage)
    {
        if(damage > currentHealth) {Death();}
        currentHealth -= damage;
    }

    private void Death()
    {
        //Play death animation and destroy an object
    }
}
