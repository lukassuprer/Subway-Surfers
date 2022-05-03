using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float velocity = 10f;
    public List<Transform> trailsList = new List<Transform>();
    public int currentPosition;
    public float jumpHeight = 10f;
    public bool isGrounded;
    public GameObject groundCheckObject;
    public float barrelRollLength = 1f;
    public float maxVelocity;

    private void Start()
    {
        currentPosition = 1;
    }

    private void Update()
    {
        //playerRigidbody.MovePosition(transform.position + Vector3.forward);
        if (Input.GetMouseButtonDown(0))
        {
            MovePosition(-1);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            MovePosition(1);
        }
        Jump();
        Crouch();
    }

    private void FixedUpdate()
    {
        float speedFactor = (maxVelocity -  playerRigidbody.velocity.magnitude) / maxVelocity;
        playerRigidbody.AddForce(speedFactor * velocity * Vector3.forward, ForceMode.Impulse);
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftCommand) && isGrounded)
        {
            StartCoroutine(BarrelRoll());
        }
    }
    
    private IEnumerator BarrelRoll()
    {
        transform.GetComponent<CapsuleCollider>().height = 0.5f;
        yield return new WaitForSeconds(barrelRollLength);
        transform.GetComponent<CapsuleCollider>().height = 1f;
    }
    
    private void Jump()
    {
        GroundCheck();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //jde to is add force jen se musí dát mnohem větší hodnota
            playerRigidbody.AddForce(Vector3.up * jumpHeight);
        }
    }


    private void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheckObject.transform.position, Vector3.down, out hit, 1f))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    private void MovePosition(int move)
    {
        if (currentPosition + move < 0)
        {
            //Debug.Log("far left");
        }
        else if (currentPosition + move > trailsList.Count - 1)
        {
            //Debug.Log("far right");
        }
        else
        {
            transform.position = new Vector3(trailsList[currentPosition + move].position.x, transform.position.y,
                transform.position.z);
            currentPosition+=move;
        }
    }
}