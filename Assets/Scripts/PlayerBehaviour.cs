using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Initialize() {
        rightMove = false;
        leftMove = false;

        transform.rotation = lookRight;
        transform.position = new Vector3(-16f, 0.5f, 20f);
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

        Look(leftMoveInit, rightMoveInit);
        Move(leftMove, rightMove);

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
    }

    private void Look(bool left, bool right) {
        if (left && !right)
        {
            transform.rotation = lookLeft;
        }
        else if (right && !left) transform.rotation = lookRight;
    }

    private void Move(bool left, bool right) {
        if (left && !right) transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        else if (right && !left) transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
    }

}
