using UnityEngine;
using UnityEngine.InputSystem;

public class ReticleController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform reticle;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPos);

        float t = -ray.origin.z / ray.direction.z;

        if (t > 0f)
        {
            Vector3 hitPoint = ray.GetPoint(t);
            hitPoint.z = 0f;
            reticle.position = hitPoint;
        }
    }
}

