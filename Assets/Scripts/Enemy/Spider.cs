using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour, IDestroyable, IEnemy
{
    [SerializeField] private float damage;
    [SerializeField] private float maxHealth;
    private float health;
    
    public AudioSource audioSource;  // Audio Source attachée au joueur
    public AudioClip punchSFX;

    private void Start()
    {
        health -= maxHealth;
    }

    //Make damage to the player
    public void MakeDamage(IDestroyable target, float value)
    {
        //Play punch sound
        audioSource.PlayOneShot(punchSFX);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Attaque le player pour {damage} PV
        if (other.gameObject.CompareTag("Player"))
        {
            MakeDamage(other.gameObject.GetComponent<IDestroyable>(), damage);
        }
    }
}
