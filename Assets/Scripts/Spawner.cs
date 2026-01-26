using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float intervalMin;
    public float intervalMax;
    public float radius;
    public float groupSizeMin;
    public float groupSizeMax;

    private float groupSize;
    private float interval;

    private float counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = 0;

        groupSize = Random.Range(groupSizeMin, groupSizeMax);
        interval = Random.Range(intervalMin, intervalMax);
    }

    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= interval)
        {
            counter = 0;

            for (int i = 0; i < groupSize; i++)
            {
                Vector2 offset = Random.insideUnitCircle * radius;
                Vector2 spawnPos = (Vector2)transform.position + offset;

                spawnPos.x = Mathf.Clamp(spawnPos.x, -40f, 40f);
                spawnPos.y = Mathf.Clamp(spawnPos.y, -40f, 40f);

                Instantiate(prefab, spawnPos, Quaternion.identity);
            }

            groupSize = Random.Range(groupSizeMin, groupSizeMax);
            interval = Random.Range(intervalMin, intervalMax);
        }
    }
}
