﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region PlayerMovement - Variables/Components, Alexander Dolk
    public KeyCode[] movementKeys = { KeyCode.D, KeyCode.A, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.W, KeyCode.UpArrow };
    public Vector2[] dir = { Vector2.right, Vector2.left, Vector2.right, Vector2.left };
 

    Rigidbody2D rb1;
    Rigidbody2D rb2;
    Transform player1;
    Transform player2;

    public float player1Speed = 4;
    public float player2Speed = 4; 
    public float jumpForce = 150;
    public bool jump1 = false;
    public bool jump2 = false;
    float vel;
    public float brakePercentage = .8f;
    #endregion PlayerMovement - Variables/Components, Alexander Dolk
    #region PlayerGFX - Variables/Components, Alexander Dolk
    Vector2 player1Scale;
    Vector2 player2Scale;
    public Animator[] player1Animators;
    public Animator[] player2Animators;
    #endregion PlayerGFX - Variables/Components, Alexander Dolk

    GameManager gM;
    UIManager UIM; 
    private void Start()
    {
        #region PlayerMovement - Components, Alexander Dolk
        player1 = GameObject.Find("Player1").GetComponent<Transform>();
        rb1 = player1.GetComponent<Rigidbody2D>();
        player2 = GameObject.Find("Player2").GetComponent<Transform>();
        rb2 = player2.GetComponent<Rigidbody2D>();
        #endregion PlayerMovement - Components, Alexander Dolk
        #region PlayerGFX - Components, Alexander Dolk  
        player1Scale = player1.localScale;
        player2Scale = player2.localScale; 
        #endregion PlayerGFX - Components, Alexander Dolk
        gM = FindObjectOfType<GameManager>();
        UIM = FindObjectOfType<UIManager>(); 
    }

    private void Update()
    {
        #region PlayerMovement, Alexander Dolk
        if (!gM.isPaused && !UIM.pauseIsActive)
        {
            vel = brakePercentage * (1 / Time.deltaTime / 60); // Deacceleration velocity 

            for (int i = 0; i < 6; i++)
            {
                if (Input.GetKey(movementKeys[i])) //Checks for input
                {
                    if (i < 2) //if buttons D/A are pressed - moves player1 right/left
                    {
                        rb1.velocity += dir[i] * player1Speed * 60 * Time.deltaTime;
                    }
                    else if (i > 1 && i < 4) //if buttons ->/<- are pressed - moves player2 right/left
                    {
                        rb2.velocity += dir[i] * player2Speed * 60 * Time.deltaTime;
                    }
                }
            }

            rb1.velocity = new Vector2(rb1.velocity.x * vel, rb1.velocity.y); // Deaccelerates player1 constantly - keeping player from accelerating infinitly
            rb2.velocity = new Vector2(rb2.velocity.x * vel, rb2.velocity.y);// Deaccelerates player2 constantly - keeping player from accelerating infinitly
        }

        if (Input.GetKeyDown(movementKeys[4]) && !jump1) //Checks if W is pressed - if so, player1 jumps
        {
            rb1.AddForce(Vector2.up * jumpForce * Time.deltaTime);
            jump1 = true;
        }

        if (Input.GetKeyDown(movementKeys[5]) && !jump2) //Checks if upArrow is pressed - if so, player2 jumps
        {
            rb2.AddForce(Vector2.up * jumpForce * Time.deltaTime);
            jump2 = true;
        }
        #endregion PlayerMovement, Alexander Dolk
        #region PlayerGFX, Alexander Dolk

        for (int i = 0; i < player1Animators.Length; i++)  //ändrade och skapade två animator arrays med en for loop för att effektivisera lite mer samt underlätta olika skins / ledger
        {
            player1Animators[i].SetFloat("Speed", rb1.velocity.x);
        }
        for (int i = 0; i < player2Animators.Length; i++)
        {
            player2Animators[i].SetFloat("Speed", rb2.velocity.x);
        }

        if (Input.GetKeyDown(movementKeys[0]) && player1.localScale.x < 0) { player1.localScale = new Vector2(player1Scale.x, player1Scale.y); }
        else if(Input.GetKeyDown(movementKeys[1]) && player1.localScale.x > 0) { player1.localScale = new Vector2(-player1Scale.x, player1Scale.y); }
        if(Input.GetKeyDown(movementKeys[2]) && player2.localScale.x < 0) { print("Test"); player2.localScale = new Vector2(-player2Scale.x, player2Scale.y); }
        else if(Input.GetKeyDown(movementKeys[3]) && player2.localScale.x > 0) { print("Test"); player2.localScale = new Vector2(player2Scale.x, player2Scale.y); }
        #endregion PlayerGFX, Alexander Dolk
    }

}
