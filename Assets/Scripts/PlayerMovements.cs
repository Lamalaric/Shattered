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
    private bool _canJump = true;
    private int _jumpCount = 2;
    private float _currentJumpCd;

    private Animator _animator;


    //Récupère le RigidBody au chargement du script
    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentJumpCd = jumpCd;
    }

    //Appelé à chaque frame
    void Update()
    {
        Debug.Log(_jumpCount);
        PlayerMove();
        PlayerJump();
    }

    //Check for specific key press for Left / Right movements
    private void PlayerMove()
    {
        //Déplacements GAUCHE - DROITE
        float horizontal = Input.GetAxis("Horizontal");
        _playerRb.velocity = new Vector2(horizontal * speed, _playerRb.velocity.y);
        
        
        //Modification de la direction dans laquelle le player regarde
        if (_playerRb.velocity.x < -0.01f)
        {
            _playerRb.transform.localScale = new Vector3(-1, 1, 1);      //Regarde à droite
        }
        else if (_playerRb.velocity.x > 0.01f)
        {
            _playerRb.transform.localScale = new Vector3(1, 1, 1);      //Regarde à gauche
        }

        //Animation
        _animator.SetBool("Run", horizontal != 0);
    }
    
    //Check for a Spacebar click to make the player jump
    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && CanJump())
        {
            StartCoroutine(JumpRoutine());
        }
    }
    //Indicate wether the player can jump or not
    private bool CanJump()
    {
        Debug.Log("Nb of jumps:"+_jumpCount+",    Cooldown:"+_currentJumpCd);
        //If no more jump left
        if (_jumpCount <= 0) return false;
        //If the cooldown for jump is not ready
        if (_currentJumpCd < jumpCd) return false;
        
        return true;
    }

    private IEnumerator JumpRoutine()
    {
        //Avoid concurrent routines
        if(!_canJump) yield break;
        //Disable jumping
        _canJump = false;

        //Proceed to the jump
        _playerRb.velocity = new Vector2(_playerRb.velocity.x, jumpForce/2);
        _jumpCount--;      //Remove 1 jump count

        //Waits for 3 seconds and then will come back here
        yield return new WaitForSeconds(jumpCd);

        // after the cooldown allow next jump
        _canJump = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            _jumpCount = 2;
        }
    }
}
