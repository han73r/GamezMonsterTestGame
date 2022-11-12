using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;
    private float yMax = 13.5f;
    private float yMin = 2.0f;

    public float floatForce;
    private float gravityModifier = 1f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    [SerializeField] private float increaseSpeed = 1.0f;
    [SerializeField] private float timeToSpeedIncrease = 15.0f;       // 1st time
    private float speedChangeInterval = 15.0f;                      // interval

    [SerializeField] private GameManager gameManagerScript;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {
        // While UpArrow is pressed and player is low enough, float up
        if (!gameOver && Input.GetKey(KeyCode.UpArrow)) //  
        {
            playerRb.AddForce(Vector3.up * floatForce * increaseSpeed);
        }

        if (Time.deltaTime > timeToSpeedIncrease)
        {
            timeToSpeedIncrease = Time.deltaTime + speedChangeInterval;
            increaseSpeed++;
        }

        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
            playerRb.velocity = Vector3.zero;
            
        }
        if (transform.position.y < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
            playerRb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            gameManagerScript.GameOver();
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 
    }

}
