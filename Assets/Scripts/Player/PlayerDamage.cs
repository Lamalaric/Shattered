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
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        health = maxHealth;
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Debug.Log("Health: "+health);

        //Kill player if he hits lava floor
        if (HitLava()) TakeDamage(maxHealth);
    }

    public void TakeDamage(float value)
    {
        health -= value;

        //Check for player death
        if (health <= 0)
        {
            Destroy();
            GameManager.UpdateGameState(GameState.Lose);
        }
    }
    public void Destroy()
    {
        Debug.Log("Player is dead");
        Destroy(gameObject);
    }

    private bool HitLava()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, .2f, LayerMask.GetMask("Lava"));
        return raycastHit.collider;
    }

    public float getMaxHealth() { return maxHealth; }
    public float getCurrHealth() { return health; }
    public void setCurrHealth(float value) { health = value; }
}
