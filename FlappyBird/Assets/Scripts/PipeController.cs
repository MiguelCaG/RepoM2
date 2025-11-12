using UnityEngine;

public class PipeController : MonoBehaviour
{
    private GameManager gameManager;

    private Rigidbody rb;

    [HideInInspector] public float speed = -5f;

    void Start()
    {
        gameManager = GameManager.instance;

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (gameManager.actualState == GameManager.State.GameOver)
            rb.velocity = new Vector3(0f, 0f, 0f);
        else
            rb.velocity = new Vector3(speed, 0f, 0f);
    }
}
