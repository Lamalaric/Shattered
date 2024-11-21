using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IEnemy
{
    [SerializeField] private float damage;

    //
    public void MakeDamage(IDestroyable target, float value)
    {
        target.TakeDamage(value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Attaque le player pour {damage} PV
        if (other.CompareTag("Player"))
        {
            MakeDamage(other.gameObject.GetComponent<IDestroyable>(), damage);
        }
    }
}
