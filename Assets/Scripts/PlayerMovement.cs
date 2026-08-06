using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 7f;
    public float sprintSpeed = 11f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 movement;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        movement = (forward * v + right * h).normalized;

        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        if (movement != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, movement, rotationSpeed * Time.deltaTime);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
}