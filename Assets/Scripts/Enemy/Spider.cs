using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, IDestroyable, IEnemy
{
    [SerializeField] private float damage;
    [SerializeField] private float maxHealth;
    private float health;

    private void Start()
    {
        health -= maxHealth;
    }

    //Make damage to the player
    public void MakeDamage(IDestroyable target, float value)
    {
        target.TakeDamage(value);
    }
    //Receive damage
    public void TakeDamage(float value)
    {
        health -= value;

        //Check for spider death
        if (health <= 0) Destroy();
    }
    //Destroy enemy on death
    public void Destroy()
    {
        Debug.Log("Enemy Spider is dead");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        //Attaque le player pour {damage} PV
        if (other.CompareTag("Player"))
        {
            MakeDamage(other.gameObject.GetComponent<IDestroyable>(), damage);
        }
    }
}
