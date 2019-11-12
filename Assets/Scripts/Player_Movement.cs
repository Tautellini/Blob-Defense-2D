﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeedMax = 3f;
    public Rigidbody2D rb;
    public int facing;
    public bool isMovingByKey;
    TileBase clickedTile;
    Vector2 input;
    Vector3 clickPos;
    Vector2 velocity;
    public Vector2 Velocity { get => velocity; set => velocity = value; }
    Vector2 moveTo;
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject player;
    Tile_Targeting targetingscript;

    void Start()
    {
        facing = 0;
       // targetingscript = player.GetComponent<Tile_Targeting>().MouseTargetTile;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMovingByKey = false;
            facing = 0;
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log(string.Format("clickPos [X: {0} Y: {1}]", clickPos.x, clickPos.y));   
            
            clickedTile = tilemap.GetTile(new Vector3Int((int)clickPos.x, (int)clickPos.y, 0));
            
        }
        if (Input.GetKeyDown("w"))
        {
            facing = 1;
            isMovingByKey = true;
        }
        if (Input.GetKeyDown("d"))
        {
            facing = 2;
            isMovingByKey = true;
        }
        if (Input.GetKeyDown("s"))
        {
            facing = 3;
            isMovingByKey = true;
        }
        if (Input.GetKeyDown("a"))
        {
            facing = 4;
            isMovingByKey = true;
        }
        if(isMovingByKey)
        {
            clickedTile = null;
            KeyBoardMovement();
        }
        if(!isMovingByKey)
        {
            MouseMovement();
        }
    }
    void FixedUpdate()
    {
        
        if(velocity.x != 0 || velocity.y != 0)
        {
            //Move to Direction
            moveTo = rb.position + velocity * Time.fixedDeltaTime;
            rb.MovePosition(moveTo);
        }
    }
void MouseMovement()
    {
        Vector2 toClick = new Vector2(clickPos.x - rb.position.x, clickPos.y - rb.position.y);
        //  Debug.Log(string.Format("rb pos [X: {0} Y: {1}]", rb.position.x, rb.position.y));
        //    Debug.Log(string.Format("Moving to [X: {0} Y: {1}]", toClick.x, toClick.y));
        float mag = toClick.magnitude;
        if(mag >= 0.2)
        {
            float div = mag / moveSpeedMax;
            if(div > 1.0)
            {
                velocity = new Vector2(toClick.x / div, toClick.y / div);
               
            } else { velocity = toClick * div;
                if (velocity.x <= 0.5 && velocity.y <= 0.5)
                {
                    velocity = new Vector2(0, 0);
                    player.GetComponent<Tile_Targeting>().MouseTargetTile(clickPos);
                }
            }
            
        //    Debug.Log(string.Format("div {0}", div));
        } else { velocity = new Vector2(0, 0); }
    
    }
    void KeyBoardMovement()
    {
        
        //get input
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        //Stop or slow down if no input
        if (input.x == 0)
        {
            if (Mathf.Abs(velocity.x) <= 0.05)
            {
                velocity.x = 0;
            }
            else { velocity.x *= (float)0.75; }
        }
        if (input.y == 0)
        {
            if (Mathf.Abs(velocity.y) <= 0.05)
            {
                velocity.y = 0;
            }
            else { velocity.y *= (float)0.75; }
        }
        //add input to velocity
        velocity += input;
        //Limit max moveSpeedMax
        if (velocity.x > moveSpeedMax)
        {
            velocity.x = moveSpeedMax;
        }
        if (velocity.x < -moveSpeedMax)
        {
            velocity.x = -moveSpeedMax;
        }
        if (velocity.y > moveSpeedMax)
        {
            velocity.y = moveSpeedMax;
        }
        if (velocity.y < -moveSpeedMax)
        {
            velocity.y = -moveSpeedMax;
        }
    }
    
}
