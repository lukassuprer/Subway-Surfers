using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using DG.Tweening;
using UnityEditor;
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
    public bool isSwiping;
    public Animator playerAnimator;
    public LayerMask groundLayer;
    private DeathManager deathManager;
    public float changeTrailTime = 0.5f;
    public float fallGravity = 5.0f;
    public LayerMask Obstacle;

    private void Start()
    {
        currentPosition = 1;
        StartCoroutine(IncreaseSpeed());
        deathManager = FindObjectOfType<DeathManager>();
    }

    private void FixedUpdate()
    {
        float speedFactor = (maxVelocity - playerRigidbody.velocity.magnitude) / maxVelocity;
        playerRigidbody.AddForce(speedFactor * velocity * Vector3.forward, ForceMode.Impulse);
        GroundCheck();
    }

    bool highPt = false;

    private void Update()
    {
        if (playerRigidbody.velocity.y < 0 && this.highPt == false)
        {
            this.highPt = true;
            playerRigidbody.AddForce(Vector3.down * fallGravity, ForceMode.Impulse);
        }

        if (isGrounded)
        {
            this.highPt = false;
        }
        HandleGravity();
    }

    public void Crouch()
    {
        StartCoroutine(BarrelRoll());
    }

    private IEnumerator BarrelRoll()
    {
        playerAnimator.SetBool("isJumping", false);
        CapsuleCollider capsuleCollider = transform.GetComponent<CapsuleCollider>();
        playerAnimator.SetBool("isScrolling", true);
        if (!isGrounded)
        {
            playerRigidbody.AddForce(Vector3.down * fallGravity, ForceMode.Impulse);
        }

        capsuleCollider.height = 0.5f;
        capsuleCollider.center = new Vector3(0, 0.22f, 0);
        yield return new WaitForSeconds(barrelRollLength);
        capsuleCollider.height = 1f;
        capsuleCollider.center = new Vector3(0.05f, 0.50f, 0.02f);
        playerAnimator.SetBool("isScrolling", false);
    }

    public void Jump()
    {
        GroundCheck();
        if (isGrounded)
        {
            StartCoroutine(JumpAnimation());
        }
    }

    private IEnumerator JumpAnimation()
    {
        playerAnimator.SetBool("isJumping", true);
        playerRigidbody.AddForce(Vector3.up * jumpHeight);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => isGrounded);
        playerAnimator.SetBool("isJumping", false);
    }


    public void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheckObject.transform.position, Vector3.down, out hit, 0.2f, groundLayer))
        {
            isGrounded = true;
        }
        else if (Physics.Raycast(groundCheckObject.transform.position, Vector3.down, out hit, 0.2f, Obstacle))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    
    private void HandleGravity()
    {
        float currentVerticalSpeed = playerRigidbody.velocity.y;
     
        if(isGrounded)
        {
            if(currentVerticalSpeed < 0f)
                currentVerticalSpeed = 0f;
        }
        else if(!isGrounded)
        {
            currentVerticalSpeed -= fallGravity * Time.deltaTime;
        }
     
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x,currentVerticalSpeed,playerRigidbody.velocity.z);
    }


    public void MovePosition(int move)
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
            StartCoroutine(VerticalMove(move));
        }
    }

    private IEnumerator VerticalMove(int move)
    {
        if (move == 1)
        {
            playerAnimator.SetBool("goingRight", true);
        }
        else if (move == -1)
        {
            playerAnimator.SetBool("goingLeft", true);
        }

        isSwiping = true;
        transform.DOMoveX(trailsList[currentPosition + move].position.x, changeTrailTime);
        currentPosition += move;
        yield return new WaitForSeconds(0.5f);
        isSwiping = false;
        playerAnimator.SetBool("goingLeft", false);
        playerAnimator.SetBool("goingRight", false);
    }

    private IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(10f);
        maxVelocity += 2f;
        StartCoroutine(IncreaseSpeed());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 6) return;
        Vector3 normal = collision.contacts[0].normal;
        normal = new Vector3(Mathf.Round(normal.x), Mathf.Round(normal.y), Mathf.Round(normal.z));
        Debug.Log(normal);
        if (normal == -(transform.forward))
        {
            //Debug.Log("DEAD");
            deathManager.DeadState();
        }

        else if (normal == transform.up)
        {
            //Debug.Log("Not dead");
        }
            
        else
        {
            return;
        }
    }
}