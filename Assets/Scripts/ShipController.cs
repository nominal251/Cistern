using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{

    public float thrustForce = 15f;
    public float strafeForce = 10.0f;

    public float boostForce = 30f;

    public float torqueForce = 5f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float turnInput = 0f;

        if (Keyboard.current.aKey.isPressed)
            turnInput -= 1f;

        if (Keyboard.current.dKey.isPressed)
            turnInput += 1f;

        //accelerate, check for boost
        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.wKey.isPressed)
        {
            rb.AddForce(transform.up * boostForce, ForceMode2D.Force);
        }
        else if (Keyboard.current.wKey.isPressed)
        {
            rb.AddForce(transform.up * thrustForce, ForceMode2D.Force);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            rb.AddForce(-transform.up * thrustForce, ForceMode2D.Force);
        }

        //rotate
        rb.AddTorque(torqueForce * turnInput, ForceMode2D.Force);

        //strafe
        if (Keyboard.current.eKey.isPressed)
        {
            rb.AddForce(-transform.right * thrustForce, ForceMode2D.Force);
        }

        if (Keyboard.current.qKey.isPressed)
        {
            rb.AddForce(transform.right * thrustForce, ForceMode2D.Force);
        }
    }
}
