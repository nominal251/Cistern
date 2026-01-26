using UnityEngine;

public class Homing : MonoBehaviour
{
    Transform target;
    GameObject player;
    public GameObject particle;

    public float force = 15f;
    public float randomness = 0.2f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;

        direction += Random.insideUnitCircle * randomness;
        direction.Normalize();

        rb.AddForce(direction * force, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
