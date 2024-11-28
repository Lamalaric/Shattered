using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IEnemy
{
    [SerializeField] private float damage;
    [SerializeField] private float everyXSeconds;
    private bool isPlayerInContact = false;
    private Coroutine actionCoroutine;

    //Damage the player
    public void MakeDamage(IDestroyable target, float value)
    {
        target.TakeDamage(value);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            isPlayerInContact = true;

            // Démarre la coroutine si elle n'est pas déjà active
            if (actionCoroutine == null)
            {
                actionCoroutine = StartCoroutine(DamagePerSecond(other.gameObject));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            isPlayerInContact = false;

            // Arrête la coroutine quand le joueur sort
            if (actionCoroutine != null)
            {
                StopCoroutine(actionCoroutine);
                actionCoroutine = null;
            }
        }
    }

    private IEnumerator DamagePerSecond(GameObject target)
    {
        while (isPlayerInContact)
        {
            //Apply damage every x sec
            MakeDamage(target.GetComponent<IDestroyable>(), damage);
            yield return new WaitForSeconds(everyXSeconds);
        }
    }
    private void DoSomething()
    {
        // Action que vous voulez exécuter
        Debug.Log("Doing something while the player is in contact!");
    }
}
