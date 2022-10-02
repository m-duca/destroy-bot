using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   
    [Header("Last Bot:")]

    [SerializeField]
    private GameObject prefabLastBot;

    [SerializeField]
    private Sprite wrongLastBot;

    [SerializeField]
    private Sprite rightLastBot;

    [HideInInspector]
    public static Rigidbody2D rb;

    [HideInInspector]
    public static SpriteRenderer spr;

    [HideInInspector]
    public static Animator animator;

    [HideInInspector]
    public static Transform transf;

    [HideInInspector]
    public static bool canPlay = false;

    [HideInInspector]
    public static bool isReseting = false;

    private static Transform respawnPoint;

    #region Engine Functions

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        transf = gameObject.GetComponent<Transform>();

        respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform;
    }

    private void Start()
    {
        MusicScript.audioSourceMusic.time = MusicScript.musicCurTime;
        MusicScript.audioSourceMusic.volume = MusicScript.defaultVolume;
        Spawn();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Transition.SetFade(Transition.FADE_RESTART);
        }
    }

    #endregion

    #region Custom Functions

    private void Spawn()
    {
        Player.transf.position = respawnPoint.position;
    }

    public void Respawn(string colTag)
    {
        Player.isReseting = true;
        GameObject lastBot = Instantiate(prefabLastBot, Player.transf.position, Quaternion.Euler(0, 0, 0));
        lastBot.GetComponent<SpriteRenderer>().flipX = Player.spr.flipX;
        lastBot.GetComponent<Rigidbody2D>().gravityScale = Player.rb.gravityScale;
        if (colTag == "Obstacle")
        {
            // Spawn Explosion
            ExplosionParticleSystem.SpawnExplosion(2f, Player.transf.position, ExplosionParticleSystem.obstacleColor);
           
            lastBot.GetComponent<SpriteRenderer>().sprite = wrongLastBot;
            Player.transf.position = respawnPoint.position;
            Player.isReseting = false;
        }
        else if (colTag == "Objective")
        {
            // Spawn Explosion
            ExplosionParticleSystem.SpawnExplosion(2f, Player.transf.position, ExplosionParticleSystem.objectiveColor);
            
            lastBot.GetComponent<SpriteRenderer>().sprite = rightLastBot;
            Transition.SetFade(Transition.FADE_IN);
            gameObject.SetActive(false);
        }
    }

    #endregion
}
