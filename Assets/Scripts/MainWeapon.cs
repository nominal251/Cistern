using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainWeapon : MonoBehaviour
{
    public Transform origin;
    public GameObject projectile;

    private Rigidbody2D shooterRb;

    public float speed = 40f;
    public float delay = 0.15f;

    public int ammoCapacity = 30;
    public float reloadIncrement = 0.01f;
    public float ammoDelay = 0.5f;

    private float counter;
    private float reloadIncrementCounter;

    private int ammo;
    private float ammoTimer;

    public float ammoPercent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammo = ammoCapacity;
        shooterRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // fire weapon
        if (Keyboard.current.leftArrowKey.isPressed && counter >= delay && ammo > 0)
        {
            counter = 0;

            GameObject firedProjectile = Instantiate(projectile, origin.position, origin.rotation);
            Rigidbody2D rb = firedProjectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = (Vector2)(origin.up * speed) + shooterRb.linearVelocity; //conserve player momentum in bullet

            ammo--;

            ammoTimer = 0;
        }

        counter += Time.deltaTime;

        ammoPercent = (float)ammo / ammoCapacity;

        Reload();
    }

    void Reload()
    {
        //clamp ammo
        if (ammo >= ammoCapacity) 
        {
            ammo = ammoCapacity;
            return;
        }

        ammoTimer += Time.deltaTime;
        reloadIncrementCounter += Time.deltaTime;

        //regen ammo
        if (ammoTimer >= ammoDelay && reloadIncrementCounter >= reloadIncrement)
        {
            ammo++;
            reloadIncrementCounter = 0;
        }
    }
}
