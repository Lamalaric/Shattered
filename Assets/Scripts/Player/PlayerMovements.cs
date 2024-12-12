using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCd;
    [SerializeField] private float dashForce;     // Force du dash
    [SerializeField] private float dashDuration; // Durée du dash
    [SerializeField] private float dashCooldown;
    private Rigidbody2D _playerRb;
    private BoxCollider2D _boxCollider;
    private bool _canJump = true;
    private int _jumpCount = 1;
    private float _currentJumpCd;
    private bool _canDash = true;
    private bool _isDashing = false;
    private float _dashDirection;

    private Animator _animator;


    //Récupère le RigidBody au chargement du script
    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _currentJumpCd = jumpCd;
    }

    //Appelé à chaque frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerDash();
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
        if (Input.GetKey(KeyCode.Space) && CanJump())   //Classic jump
            StartCoroutine(JumpRoutine());

        if (onWall())   //Stick on the wall
        {
            _animator.SetBool("Wallride", true);
            _playerRb.gravityScale = 0;
            _playerRb.velocity = Vector2.zero;

            if (Input.GetKeyDown(KeyCode.Space))    //Walljump
            {
                _animator.SetBool("Wallride", false);
                _playerRb.gravityScale = 4;
                //Proceed to the jump
                _playerRb.velocity = new Vector2(Mathf.Sign(transform.localScale.x)*10, jumpForce/2);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else    //Reset gravity if unstick the wall
        {
            _playerRb.gravityScale = 4;

            _animator.SetBool("Wallride", false);
        }
    }
    
    //Indicate wether the player can jump or not
    private bool CanJump()
    {
        //Reset the nb of jump if on ground
        if (isGrounded()) _jumpCount = 1;
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

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        return raycastHit.collider;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, new Vector2(transform.localScale.x, 0), 0.1f, LayerMask.GetMask("Ground"));
        return raycastHit.collider != null;
    }
    
    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash && !_isDashing)
        {
            // Détermine la direction du dash
            _dashDirection = _playerRb.transform.localScale.x > 0 ? 1 : -1;
            StartCoroutine(DashRoutine());
        }
    }
    
    private IEnumerator DashRoutine()
    {
        // Commence le dash
        _canDash = false;
        _isDashing = true;

        // Durée pendant laquelle le dash est actif
        float dashTime = 0;

        // Applique la force de dash tant que la durée du dash n'est pas atteinte
        while (dashTime < dashDuration)
        {
            _playerRb.velocity = new Vector2(_dashDirection * dashForce, _playerRb.velocity.y);
            dashTime += Time.deltaTime; // Incrémente le temps du dash
            yield return null; // Attend la prochaine frame
        }

        // Termine le dash en remettant la vitesse horizontale à zéro
        _playerRb.velocity = new Vector2(0, _playerRb.velocity.y);
        _isDashing = false;

        // Cooldown avant de pouvoir dasher à nouveau
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }
}