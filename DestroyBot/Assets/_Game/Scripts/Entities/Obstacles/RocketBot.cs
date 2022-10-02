using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBot : MonoBehaviour
{
    [Header("Rocket Bot Settings:")]

    [SerializeField]
    private float initialSpeed;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float acceleration;

    [HideInInspector]
    public float moveDirection;

    // Movement Variables
    private float curMoveSpeed;
    private Vector2 direction;

    // Flip Control Variable
    [HideInInspector]
    public string type;

    // Components 
    private Rigidbody2D rb;
    private CircleCollider2D coll;

    // References
    [HideInInspector]
    public BoxCollider2D[] rocketLauncherHitBox = new BoxCollider2D[2];


    #region Engine Functions

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(coll, rocketLauncherHitBox[0]);
        Physics2D.IgnoreCollision(coll, rocketLauncherHitBox[1]);

        // Set current move speed to initial speed
        curMoveSpeed = initialSpeed;


        if (type == "HORIZONTAL")
        {
            // Set Scale X to be equals the move direction
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * Mathf.Sign(moveDirection), gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            // Set Movement Direction to Horizontal
            direction = Vector2.right * moveDirection;
        }
        else
        {
            // Set Scale Y to be equals the move direction
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y * Mathf.Sign(moveDirection), gameObject.transform.localScale.z);

            // Rotate Vertically
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f * Mathf.Sign(moveDirection));

            // Set Movement Direction to Vertical
            direction = Vector2.up * moveDirection;
        }

    }

    // Update is called once per frame
    private void Update()
    {
        // Increment current move speed
        if (curMoveSpeed < maxSpeed)
        {
            curMoveSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // Set current move speed to maximum speed
            curMoveSpeed = maxSpeed;
        }

    }

    private void FixedUpdate()
    {
        // Move Rocket
        rb.velocity = direction * curMoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        // Destroy Player Last Bot
        if (collision.gameObject.CompareTag("LastBot"))
        {
            Destroy(collision.gameObject);
        }
        */

        // When collision with anything
        // Spawn Explosion
        if (gameObject.CompareTag("Obstacle")) // Change Particles to Red Color
        {
            ExplosionParticleSystem.SpawnExplosion(2f, gameObject.transform.position, ExplosionParticleSystem.obstacleColor);
        }
        else // Change Particles to Green Color
        {
            ExplosionParticleSystem.SpawnExplosion(2f, gameObject.transform.position, ExplosionParticleSystem.objectiveColor);
        }
        
        // Destroy this instance
        Destroy(gameObject);
    }

    #endregion



}


