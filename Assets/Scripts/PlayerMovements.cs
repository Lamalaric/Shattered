using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _playerRb;

    //Récupère le RigidBody au chargement du script
    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    //Appelé à chaque frame
    void Update()
    {
        //Déplacements GAUCHE - DROITE
        _playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _playerRb.velocity.y);
        
        //JUMP
        if (Input.GetKey(KeyCode.Space))
        {
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, speed/2);
        }
    }
}
