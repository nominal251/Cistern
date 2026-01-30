using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public Transform reticle;

    public float thrustForce = 15f;
    public float strafeForce = 10.0f;

    public float boostForce = 30f;

    public float aimTorque = 1f;

    public float maxBoost = 5f;
    public float drainRate = 1f;
    public float rechargeRate = 1f;
    public float boostDelay = 1f;

    private float boost;
    private float counter;

    public float boostPercent;

    public float deadZone = 0.5f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        boost = maxBoost;
    }

    void FixedUpdate()
    {
        AimTowardReticle();

        //accelerate, check for boost
        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.wKey.isPressed && boost > 0)
        {
            rb.AddForce(transform.up * boostForce, ForceMode2D.Force);
            boost -= drainRate * Time.fixedDeltaTime;
            counter = 0; //reset recharge counter
        }
        else if (Keyboard.current.wKey.isPressed)
        {
            rb.AddForce(transform.up * thrustForce, ForceMode2D.Force);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            rb.AddForce(-transform.up * thrustForce, ForceMode2D.Force);
        }

        Recharge();

        //strafe
        if (Keyboard.current.dKey.isPressed)
        {
            rb.AddForce(-transform.right * thrustForce, ForceMode2D.Force);
        }

        if (Keyboard.current.aKey.isPressed)
        {
            rb.AddForce(transform.right * thrustForce, ForceMode2D.Force);
        }

        boostPercent = boost / maxBoost;
    }

    void Recharge()
    {
        if (boost >= maxBoost)
        {
            boost = maxBoost;
            return;
        }

        counter += Time.fixedDeltaTime;

        if (counter >= boostDelay)
        {
            boost += rechargeRate * Time.fixedDeltaTime;
        }
    }

    void AimTowardReticle()
    {
        Vector2 toReticle = (Vector2)(reticle.position - transform.position);

        if (toReticle.sqrMagnitude < 0.0001f)
            return;

        float targetAngle = Mathf.Atan2(toReticle.y, toReticle.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = rb.rotation;

        float angleError = Mathf.DeltaAngle(currentAngle, targetAngle);

        if (Mathf.Abs(angleError) < deadZone)
        {
            rb.angularVelocity = 0f;
            return;
        }

        float desiredAngularVelocity = angleError * aimTorque;

        rb.angularVelocity = desiredAngularVelocity;
    }
}
