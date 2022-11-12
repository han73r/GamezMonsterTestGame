using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private float leftBound = -10;
    private GameManager gameManagerScript;
    [SerializeField] private float speed;

    private void Start()
    {
        playerControllerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = gameManagerScript.gameSpeed;
    }

    private void Update()
    {
        // If game is not over, move to the left
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }

}
