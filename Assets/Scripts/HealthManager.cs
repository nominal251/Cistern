using UnityEngine;
using UnityEngine.InputSystem;

public class HealthManager : MonoBehaviour
{
    InputAction repairAction;

    public GameOverManager gameOverManager;

    public float maxHealth = 100f;

    private float health = 100f;

    public int repairCooldown = 15;
    public int repairSpeed = 100;

    public float healthToDisplay;

    private float counter = 0f;

    void Start()
    {
        repairAction.Enable();
    }

    void Update()
    {
        if (health >= 0)
        {
            gameOverManager.RestartScene();
        }

        healthToDisplay = health;

        if (repairAction.IsPressed() && counter >= repairCooldown)
        {
            Repair();
            counter = 0;
        }
    }

    void Repair()
    {
        while (health < maxHealth)
        {
            health += repairSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            health = health - 50;
        }
    }
}
