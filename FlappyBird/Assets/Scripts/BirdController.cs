using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static event Action Score;   // CAMBIAR AL GESTOR DE EVENTOS EN GAMEMANAGER QUE TIENE SINGLETON
    public static event Action Die;     // CAMBIAR AL GESTOR DE EVENTOS EN GAMEMANAGER QUE TIENE SINGLETON

    private Rigidbody rb;

    private float jumpSpeed = 5f;

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

            if (!birdAnim.GetCurrentAnimatorStateInfo(0).Equals("Flying"))
            {
                birdAnim.SetTrigger("Fly");
            }

            birdAudio.clip = jumpAudio;
            birdAudio.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pipe"))
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

        Die.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Score"))
        {
            Score.Invoke();
        }
    }
}
