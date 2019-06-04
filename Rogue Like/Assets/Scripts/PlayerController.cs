using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float runSpeed;
    public float sightDistance;
    public Animator animator;
    public GameObject sight;

    private bool isRunning;
    private Rigidbody2D rb;
    private float moveVelocity;
    private Vector2 moveInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = Mathf.Clamp(moveInput.magnitude, 0.0f, 1.0f);
        moveInput.Normalize();

        Animate();
        Sight();

        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

    }

    void FixedUpdate()
    {
        if(isRunning)
        {
            rb.velocity = moveInput * moveVelocity * runSpeed;
        }
        else
        {
            rb.velocity = moveInput * moveVelocity * speed;
        }


    }

    void Animate()
    {
        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("Horizontal", moveInput.x);
            animator.SetFloat("Vertical", moveInput.y);
        }
        animator.SetFloat("speed", rb.velocity.magnitude);
    }

    void Sight()
    {
        if(moveInput != Vector2.zero)
        {
            sight.transform.localPosition = moveInput * sightDistance;
        }
    }
}
