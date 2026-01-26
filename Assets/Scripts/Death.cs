using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameOverManager gameOverManager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            gameOverManager.RestartScene();
        }
    }
}
