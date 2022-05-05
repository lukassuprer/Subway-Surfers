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
    private bool isScrolling;

    private void Start()
    {
        currentPosition = 1;
        StartCoroutine(IncreaseSpeed());
    }

    private void FixedUpdate()
    {
        float speedFactor = (maxVelocity - playerRigidbody.velocity.magnitude) / maxVelocity;
        playerRigidbody.AddForce(speedFactor * velocity * Vector3.forward, ForceMode.Impulse);
        GroundCheck();
    }

    public void Crouch()
    {
        StartCoroutine(BarrelRoll());
    }

    private IEnumerator BarrelRoll()
    {
        isScrolling = true;
        playerAnimator.SetBool("isJumping", false);
        CapsuleCollider capsuleCollider = transform.GetComponent<CapsuleCollider>();
        playerAnimator.SetBool("isScrolling", true);
        playerRigidbody.AddForce(Vector3.down * 5, ForceMode.Impulse);
        capsuleCollider.height = 0.5f;
        capsuleCollider.center = new Vector3(0, 0.25f, 0);
        yield return new WaitForSeconds(barrelRollLength);
        capsuleCollider.height = 1f;
        capsuleCollider.center = new Vector3(0.05f, 0.50f, 0.02f);
        playerAnimator.SetBool("isScrolling", false);
        isScrolling = false;
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
        else
        {
            isGrounded = false;
        }
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
        transform.DOMoveX(trailsList[currentPosition + move].position.x, 0.5f);
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
}