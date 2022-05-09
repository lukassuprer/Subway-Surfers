using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ControlsScript : MonoBehaviour
{
    private float screenWidth;
    private float screenHeight;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public PlayerMovement playerMovement;
    public LayerMask obstacleLayer;
    private int obstaclesHit;
    private DeathManager deathManager;

    private void Awake()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        startPosition = new Vector3(0.0f, 0.0f, 0.0f);
        deathManager = FindObjectOfType<DeathManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPosition = GetTouchPos(touch);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endPosition = GetTouchPos(touch);
            }
        }

        MouseMovement();
        CheckForObstacles(Vector3.right);
    }

    private Vector3 GetTouchPos(Touch touch)
    {
        Vector2 pos = touch.position;
        pos.x = (pos.x - screenWidth) / screenWidth;
        pos.y = (pos.y - screenHeight) / screenHeight;
        return new Vector3(-pos.x, pos.y, 0.0f);
    }

    private Vector3 GetMousePos()
    {
        Vector2 pos = Input.mousePosition;
        pos.x = (pos.x - screenWidth) / screenWidth;
        pos.y = (pos.y - screenHeight) / screenHeight;
        return new Vector3(-pos.x, pos.y, 0.0f);
    }

    private void MouseMovement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startPosition = GetMousePos();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            endPosition = GetMousePos();

            float moveX = Vector3.Dot(Vector2.left, endPosition - startPosition);
            float moveY = Vector3.Dot(Vector2.up, endPosition - startPosition);
            Move(moveX, moveY);
        }
    }

    private void Move(float x, float y)
    {
        if (x > 0.2f)
        {
            if (CheckForObstacles(Vector3.right))
            {
                playerMovement.MovePosition(1);
            }
            else
            {
                obstaclesHit++;
                playerMovement.isSwiping = true;
            }
        }
        else if (x < -0.2f)
        {
            if (CheckForObstacles(Vector3.left))
            {
                playerMovement.MovePosition(-1);
            }
            else
            {
                obstaclesHit++;
                playerMovement.isSwiping = true;
            }
        }

        if (obstaclesHit > 2)
        {
            deathManager.DeadState();
        }

        if (!playerMovement.isSwiping)
        {
            playerMovement.GroundCheck();
            if (y > 0 && playerMovement.isGrounded)
            {
                playerMovement.Jump();
            }
            else if (y < 0)
            {
                playerMovement.Crouch();
            }
        }
        playerMovement.isSwiping = false;
    }

    private bool CheckForObstacles(Vector3 direction)
    {
        return !Physics.Raycast(playerMovement.transform.position, direction, 5f, obstacleLayer);
    }
}