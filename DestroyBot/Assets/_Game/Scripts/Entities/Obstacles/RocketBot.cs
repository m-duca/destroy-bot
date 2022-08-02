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

        // Set Scale X to be equals the move direction
        gameObject.transform.localScale = new Vector3(moveDirection, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
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
        rb.velocity = Vector2.right * moveDirection * curMoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy Player Last Bot
        if (collision.gameObject.CompareTag("LastBot"))
        {
            Destroy(collision.gameObject);
        }

        // When collision with anything
        // Destroy this instance
        Destroy(gameObject);
    }

    #endregion



}


