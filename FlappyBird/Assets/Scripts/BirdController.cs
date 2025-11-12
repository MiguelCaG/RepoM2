using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Rigidbody rb;

    private float jumpSpeed = 5f;
    private float maxRotationUp = 30f;
    private float maxRotationDown = -40f;
    private float rotationLerpSpeed = 5f;
    private float targetRotation = 0f;

    private Animator birdAnim;

    private AudioSource birdAudio;
    public AudioClip jumpAudio;
    public AudioClip collisionAudio;

    private GameManager gameManager;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        birdAnim = GetComponent<Animator>();
        birdAudio = GetComponent<AudioSource>();

        gameManager = GameManager.instance;
        gameManager.actualState = GameManager.State.Playing;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && gameManager.actualState == GameManager.State.Playing)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);

            targetRotation = maxRotationUp;

            birdAnim.SetTrigger("Fly");

            birdAudio.clip = jumpAudio;
            birdAudio.Play();
        }

        if (rb.velocity.y < 0)
            targetRotation = maxRotationDown;

        float newZRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, rotationLerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newZRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pipe"))
        {
            gameManager.actualState = GameManager.State.GameOver;
            birdAudio.clip = collisionAudio;
            birdAudio.Play();

            StartCoroutine("Fall");
        }
    }

    private IEnumerator Fall()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(3.0f);

        gameManager.OnDie();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Score"))
        {
            gameManager.OnScore();
        }
    }
}
