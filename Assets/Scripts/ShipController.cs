using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{

    public float thrustForce = 15f;
    public float turnSpeed = 180f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float turnInput = 0f;

        if (Keyboard.current.aKey.isPressed)
            turnInput -= 1f;

        if (Keyboard.current.dKey.isPressed)
            turnInput += 1f;

        if (Keyboard.current.wKey.isPressed)
        {
            rb.AddForce(transform.up * thrustForce, ForceMode2D.Force);
        }

        if (turnInput != 0f)
        {
            rb.MoveRotation(rb.rotation + turnSpeed * turnInput * Time.fixedDeltaTime);
        }
    }
}
