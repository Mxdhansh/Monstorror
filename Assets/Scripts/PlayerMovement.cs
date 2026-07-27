using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Animator animator;
    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        movement = (forward * vertical + right * horizontal).normalized;

        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        Vector3 horizontalVelocity = movement * speed;

        rb.linearVelocity = new Vector3(
            horizontalVelocity.x,
            velocity.y,
            horizontalVelocity.z
        );
    }
}

// Bi-weekly game progress 5 //