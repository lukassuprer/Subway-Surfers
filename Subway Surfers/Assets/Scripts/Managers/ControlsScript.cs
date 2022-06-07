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
    public float deadZone = 0.2f;
    private AudioManager AudioManager;
    private GameManager gameManager;
    public LineRenderer lineRenderer;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        startPosition = new Vector3(0.0f, 0.0f, 0.0f);
        deathManager = FindObjectOfType<DeathManager>();
        AudioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && gameManager.GamePaused == false) 
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && gameManager.GamePaused == false)
        {
            startPosition = GetMousePos();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && gameManager.GamePaused == false)
        {
            endPosition = GetMousePos();

            float moveX = Vector3.Dot(Vector2.left, endPosition - startPosition);
            float moveY = Vector3.Dot(Vector2.up, endPosition - startPosition);
            Move(moveX, moveY);
        }
    }

    private void Move(float x, float y)
    {
        if (x > deadZone && gameManager.GamePaused == false)
        {
            if (CheckForObstacles(Vector3.right))
            {
                AudioManager.Play("Swipe");
                playerMovement.MovePosition(1);
            }
            else
            {
                obstaclesHit++;
                playerMovement.isSwiping = true;
            }
        }
        else if (x < -deadZone && gameManager.GamePaused == false)
        {
            if (CheckForObstacles(Vector3.left))
            {
                AudioManager.Play("Swipe");
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

        if (!playerMovement.isSwiping && gameManager.GamePaused == false)
        {
            playerMovement.GroundCheck();
            if (y > 0 && playerMovement.isGrounded)
            {
                AudioManager.Play("SwipeUp");
                playerMovement.Jump();
            }
            else if (y < 0)
            {
                AudioManager.Play("SwipeDown");
                playerMovement.Crouch();
            }
        }
        playerMovement.isSwiping = false;
    }

    private bool CheckForObstacles(Vector3 direction)
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(playerMovement.transform.position + Vector3.up * 2, direction, out hit, 10f, obstacleLayer))
        {
            Debug.Log("Did Hit");
            return false;
        }
        else
        {
            Debug.Log("Did not Hit");
            return true;
        }
    }
}