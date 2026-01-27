using UnityEngine;
using UnityEngine.InputSystem;

public class ZoneControl : MonoBehaviour
{
    public GameOverManager gameOverManager;

    public GameObject warning;
    public GameObject particle;

    private bool playerPresent = false;
    private bool zoneActive = false;

    public float secondsToComplete = 10;
    private float timer;

    public int chanceToActivate; // chance to activate per second is 1 over this value

    public float percentActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Reroll), 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (zoneActive)
        {
            timer += Time.deltaTime;

            if (timer >= secondsToComplete)
            {
                gameOverManager.RestartScene();
            }

            if (playerPresent && Keyboard.current.spaceKey.isPressed)
            {
                zoneActive = false;
                warning.SetActive(false);
                Instantiate(particle, transform.position, Quaternion.identity);
                timer = 0;
            }
        }

        percentActive = timer / secondsToComplete;
    }

    void Reroll()
    {
        if (!zoneActive && Random.Range(0, chanceToActivate) == 0)
        {
            zoneActive = true;
            warning.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPresent = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPresent = false;
        }
    }
}
