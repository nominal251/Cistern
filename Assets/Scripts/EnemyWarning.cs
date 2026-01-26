using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
    public Transform indicator;
    public float spawnTime;
    public GameObject enemyPrefab;

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indicator.localScale = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        indicator.localScale = Vector2.one * (timer / spawnTime);

        if (timer >= spawnTime)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
