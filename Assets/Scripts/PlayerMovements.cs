using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCd;
    private Rigidbody2D _playerRb;
    private int _jumpCount = 2;
    private float _currentJumpCd = 0f;


    //Récupère le RigidBody au chargement du script
    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _currentJumpCd = jumpCd;
    }

    //Appelé à chaque frame
    void Update()
    {
        Debug.Log(_jumpCount);
        PlayerMove();
        PlayerJump();

        ResetJumpCount();
    }

    //Check for specific key press for Left / Right movements
    private void PlayerMove()
    {
        //Déplacements GAUCHE - DROITE
        _playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _playerRb.velocity.y);
        
        
        //Modification de la direction dans laquelle le player regarde
        if (_playerRb.velocity.x > 0)
        {
            _playerRb.transform.localScale = new Vector3(-1, 1, 1);      //Regarde à droite
        }
        else
        {
            _playerRb.transform.localScale = new Vector3(1, 1, 1);      //Regarde à gauche
        }
    }
    
    //Check for a Spacebar click to make the player jump
    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && _jumpCount > 0)
        {
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, jumpForce/2);
            _jumpCount--;      //Remove 1 jump count
            // jumpCd = 
        }
    }
    //Indicate wether the player can jump or not
    private bool CanJump()
    {
        //If no more jump left
        if (_jumpCount < 0) return false;
        //If the cooldown for jump is not ready
        if (_currentJumpCd < jumpCd) return false;
        
        return true;
    }
    
    //Reset the Jump count if the player touches the ground
    private void ResetJumpCount()
    {
        //If player touches the ground
        // if (Collision.)
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            _jumpCount = 2;
        }
    }
}
