using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour, IDestroyable
{
    [SerializeField] private float damage;
    [SerializeField] private float maxHealth;
    private float health;

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        Debug.Log("Health: "+health);
    }

    public void TakeDamage(float value)
    {
        health -= value;

        //Check for player death
        if (health <= 0) Destroy();
    }
    public void Destroy()
    {
        Debug.Log("Player is dead");
        Destroy(gameObject);
    }
}
