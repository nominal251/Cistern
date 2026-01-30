using UnityEngine;

public class LockRotation : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 euler = transform.eulerAngles;
        euler.z = 0f;
        transform.eulerAngles = euler;
    }
}
