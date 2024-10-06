using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    // Determines whether player is on ground or not.
    private bool onGround = true;

    private Rigidbody rb;
    private Animator animator;

    private bool rightMoveInit;
    private bool leftMoveInit;

    private bool rightMove;
    private bool leftMove;

    private Quaternion lookLeft = Quaternion.Euler(0, -90, 0);
    private Quaternion lookRight = Quaternion.Euler(0, 90, 0);

    public float moveSpeed = 5f;
    public float jumpForce = 700;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= 3;
    }

    // Reset Maps and Character when restart game.
    public void Initialize(int level) {
        rightMove = false;
        leftMove = false;

        transform.rotation = lookRight;
        switch (level) {
            case 1:
                transform.position = new Vector3(-16.85f, 0.5f, -19.8f);
                break;
            case 2:
            case 3:
            case 4:
            default:
                throw new NotImplementedException();
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround) Jump();
        if (Input.GetKeyDown(KeyCode.RightArrow)) rightMoveInit = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) leftMoveInit = true;

        if (Input.GetKey(KeyCode.RightArrow)) rightMove = true;
        else rightMove = false;
        if (Input.GetKey(KeyCode.LeftArrow)) leftMove = true;
        else leftMove = false;

        Move(leftMove, rightMove);
        Look(leftMoveInit, rightMoveInit);

        rightMoveInit = false;
        leftMoveInit = false;
        
    }

    private void Jump() {
        onGround = false;
        rb.AddForce(rb.mass * jumpForce * Vector3.up);
        animator.SetBool("Jump_b", true);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onGround = true;
            animator.SetBool("Jump_b", false);
        }
        else if (collision.gameObject.CompareTag("Money")) {
            LevelManager.instance.AddMoney();
            collision.gameObject.SetActive(false);
        }
    }

    private void Look(bool left, bool right) {
        if (left && !right)
        {
            transform.rotation = lookLeft;
            animator.SetFloat("Speed_f", 1f);
        }
        else if (right && !left) { 
            transform.rotation = lookRight;
            animator.SetFloat("Speed_f", 1f);
        }
        else animator.SetFloat("Speed_f", 0f);
    }

    private void Move(bool left, bool right) {
        
        if (left && !right) transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        else if (right && !left) transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
    }

}
