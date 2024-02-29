using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Security.Cryptography;

public class CharacterControllerLive : MonoBehaviour
{
    public float acceleration = 100f;
    public float maxSpeed = 15f;
    public float jumpImpulse = 50f;
    public float jumpBoost = 3f;
    public bool isGrounded;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public GameObject mainCamera;
    private int score = 0;
    private int coins = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    void Reset()
    {
        Debug.Log("You died");
        gameObject.transform.position = new Vector3(3, 5, 0);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        mainCamera.GetComponent<Transform>().position = new Vector3(13.08f, 5.49f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity += Vector3.right * horizontalMovement * Time.deltaTime * acceleration;

        //CapsuleCollider col = GetComponent<CapsuleCollider>();
        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + .03f;

        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;

        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        Color lineColor = (isGrounded) ? Color.red : Color.blue;
        Debug.DrawLine(startPoint, endPoint, lineColor, 0f, true);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        else if (!isGrounded && Input.GetKey(KeyCode.Space))
        {
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoost);
            }
        }

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = 15f;
        }
        else
        {
            maxSpeed = 6f;
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            Vector3 newVel = rb.velocity;
            newVel.x = Mathf.Clamp(newVel.x, -maxSpeed, maxSpeed);
            rb.velocity = newVel;
        }

        if (Mathf.Abs(horizontalMovement) < .5f)
        {
            Vector3 newVel = rb.velocity;
            newVel.x *= 0.9f;
            rb.velocity = newVel;
        }

        float yaw = (rb.velocity.x > 0) ? 90 : -90;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        float speed = Mathf.Abs(rb.velocity.x);
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", speed);
        anim.SetBool("In Air", !isGrounded);

        // Player is dead if they go to low on the screen
        if (rb.position.y < -2)
        {
            Reset();
        }
    }

    void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        string scoreString = score.ToString();
        string finalScoreText = "";
        for (int i = 0; i < 6 - scoreString.Length; i++)
        {
            finalScoreText += "0";
        }
        finalScoreText = "MARIO\n" + finalScoreText + scoreString;
        scoreText.text = finalScoreText;
    }

    void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
        coinText.text = $"COIN: x{coins}";
    }

    void OnCollisionEnter(Collision collision)
    {
        // Must hit block from below
        if (gameObject.GetComponent<Rigidbody>().position.y + 2.25f < collision.gameObject.GetComponent<Transform>().position.y)
        {
            if (collision.gameObject.name == "Brick(Clone)") {
                Destroy(collision.gameObject);
                AddScore(100);
            }
            else if (collision.gameObject.name == "Question(Clone)") {
                AddScore(100);
                AddCoins(1);
            }
        }
    }

    // Detect collision with flag or water
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Flag(Clone)")
        {
            Debug.Log("You win!");
        }
        else if (other.gameObject.name == "Water(Clone)")
        {
            Reset();
        }
    }
}
