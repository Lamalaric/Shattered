using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour, IDestroyable
{
    [SerializeField] private float damage;
    [SerializeField] private float maxHealth;
    private float health;
    public HeartManager heartManager;

    private void Start()
    {
        health = maxHealth;
        heartManager.InitializeHearts((int)maxHealth);
        heartManager.UpdateCurrentHealth(health);
    }
    
    private void Update()
    {
        Debug.Log("Health: "+health);
    }

    public void TakeDamage(float value)
    {
        health -= value;
        health = Mathf.Clamp(health, 0, maxHealth);
        heartManager.UpdateCurrentHealth(health);

        // Check for player death
        if (health <= 0) Destroy();
    }
    public void Destroy()
    {
        Debug.Log("Player is dead");
        Destroy(gameObject);
    }
}
