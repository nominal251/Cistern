using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class ShipController : MonoBehaviour
{
    public Transform reticle;
    [SerializeField] private GameObject overheatIndicator;

    [Header("Mobility Parameters")]
    public float thrustForce = 15f;
    public float strafeForce = 10.0f;

    public float boostForce = 30f;

    public float aimTorque = 1f;

    [Header("Boost Parameters")]
    public float maxBoost = 5f;
    public float drainRate = 1f;
    public float rechargeRate = 1f;
    public float boostDelay = 1f;

    private float boost;
    private float counter;

    public float boostPercent;

    [Header("Dead Zone")]
    public float deadZone = 0.5f;

    [Header("Heat Parameters")]
    public Image heatbar;

    public float heat = 0f;
    public float maxHeat = 100f;
    public float coolRate = 1f;
    public float boostHeatRate = 1f;

    public bool heatLocked = false;
    public bool heatedThisFrame = false;
    public bool overheated = false;

    private float heatCounter;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        overheatIndicator.SetActive(false);

        boost = maxBoost;
    }

    void FixedUpdate()
    {
        heatedThisFrame = false;

        AimTowardReticle();

        //accelerate, check for boost
        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.wKey.isPressed && boost > 0 && overheated == false)
        {
            rb.AddForce(transform.up * boostForce, ForceMode2D.Force);
            boost -= drainRate * Time.fixedDeltaTime;
            counter = 0; //reset recharge counter

            heat += boostHeatRate * Time.fixedDeltaTime;
            heatedThisFrame = true;
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

        ManageHeat();
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
        void ManageHeat()
    {
        if (heatedThisFrame == false && heat > 0 && heatLocked == false)
        {
            heat -= coolRate * Time.fixedDeltaTime;
        }

        if (heat >= 100)
        {
            StartCoroutine(Overheating());
        }

        heatbar.fillAmount = heat / maxHeat;
    }

    IEnumerator Overheating()
    {
        overheatIndicator.SetActive(true);
        overheated = true;
        heat = 100;
        yield return new WaitForSeconds(5f);
        overheated = false;
        overheatIndicator.SetActive(false);
    }
}
