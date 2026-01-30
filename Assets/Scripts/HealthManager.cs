using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    InputAction repairAction;

    public GameOverManager gameOverManager;
    [SerializeField] private GameObject repairIndicator;

    public Image healthbar;
    public Image cooldownBar;

    public float maxHealth = 50f;
    private float health;

    public float repairCooldown = 15f;
    public float repairSpeed = 20f;
    public float repairDuration = 1f; // <-- NEW

    private float cooldownTimer;
    private float repairTimer;
    private bool isRepairing;

    private float blinkTimer = 0f;
    private bool blinkState = false;

    private float blinkInterval = 0.3f;

    void Start()
    {
        health = maxHealth;

        repairAction = InputSystem.actions.FindAction("Repair");
        repairAction.Enable();

        cooldownTimer = repairCooldown;

        repairIndicator.SetActive(false);
    }

    void Update()
    {
        // Death
        if (health <= 0f)
        {
            gameOverManager.RestartScene();
        }

        // Cooldown timer
        if (cooldownTimer < repairCooldown)
        {
            cooldownTimer += Time.deltaTime;
        }

        // Tap to start repair
        if (repairAction.WasPressedThisFrame() && cooldownTimer >= repairCooldown)
        {
            isRepairing = true;
            repairTimer = repairDuration;
            cooldownTimer = 0f; // cooldown starts immediately
        }

        // Repair for a fixed duration
        if (isRepairing)
        {
            repairTimer -= Time.deltaTime;

            health += repairSpeed * Time.deltaTime;
            health = Mathf.Min(health, maxHealth); // clamp

            if (repairTimer <= 0f)
            {
                isRepairing = false;
            }
        }

        if (isRepairing)
        {
            blinkTimer += Time.deltaTime;

            if (blinkTimer >= blinkInterval)
            {
                blinkTimer = 0f;
                blinkState = !blinkState;
                repairIndicator.SetActive(blinkState);
            }
        }
        else
        {
            blinkTimer = 0f;
            blinkState = false;
            repairIndicator.SetActive(false);
        }

        // UI
        healthbar.fillAmount = health / maxHealth;
        cooldownBar.fillAmount = cooldownTimer / repairCooldown;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            health -= 25f;
            Destroy(collision.gameObject);
        }
    }
}
