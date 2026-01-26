using UnityEngine;

public class ZMovement : MonoBehaviour
{
    public float speed = 10f;
    public GameObject spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posZ = transform.position;
        posZ.z += speed * Time.deltaTime;

        if (posZ.z >= 0f)
        {
            posZ.z = 0f;

            Instantiate(spawner, posZ, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        transform.position = posZ;
    }
}
