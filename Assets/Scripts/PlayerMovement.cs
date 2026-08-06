using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 7f;
    public float sprintSpeed = 11f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Animator animator;
    private AudioSource footsteps;

    private Vector3 movement;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        footsteps = GetComponent<AudioSource>();

        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            if (animator != null)
                animator.SetFloat("Speed", 0f);

            if (footsteps != null && footsteps.isPlaying)
                footsteps.Stop();

            movement = Vector3.zero;
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        movement = (forward * v + right * h).normalized;

        bool sprinting = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = sprinting ? sprintSpeed : walkSpeed;

        if (movement != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, movement, rotationSpeed * Time.deltaTime);

            if (footsteps != null)
            {

            footsteps.pitch = sprinting ? 3.2f : 2.7f;

                if (!footsteps.isPlaying)
                    footsteps.Play();
            }
        }
        else
        {
            if (footsteps != null && footsteps.isPlaying)
                footsteps.Stop();
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0f)
            return;

        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
}