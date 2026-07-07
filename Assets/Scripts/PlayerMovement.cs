using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Keep movement parallel to the ground
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Movement relative to the camera
        Vector3 movement = (forward * vertical + right * horizontal);

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Rotate the player to face the direction of movement
        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }
}