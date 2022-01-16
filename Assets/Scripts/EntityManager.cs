using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public int MaxHealth;
    public int Energy;

    public int CurrentHealth {
        get
        {
            return currentHealth;
        } 
    }
    private int currentHealth;

    void Start()
    {
        currentHealth = MaxHealth;
    }

    void Update()
    {

    }

    public void AddHealth(int amount, bool canBeHigherThanMax = true)
    {
        if (canBeHigherThanMax)
        {
            currentHealth += amount;
        }
        else
        {
            if (currentHealth + amount > MaxHealth)
            {
                currentHealth = MaxHealth;
            }
            else
            {
                currentHealth += amount;
            }
        }
    }

    public bool TakeDamage(int amount, bool destroyOnDeath = true)
    {
        currentHealth -= amount;
        return currentHealth <= 0 && destroyOnDeath;
    }
}
