using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBotLauncherVertical : MonoBehaviour
{
    [Header("Rocket Launcher Bot Settings:")]

    [SerializeField] private float spawnRocketTime;

    [SerializeField] private GameObject rocketPrefab;

    // Stretch & Squash Variables
    private float defaultScaleX, defaultScaleY;

    // References
    private Transform rocketSpawnPoint;

    #region Engine Functions
    private void Awake()
    {
        rocketSpawnPoint = transform.Find("Rocket Spawn Point").transform;
    }


    // Start is called before the first frame update
    private void Start()
    {
        // Get default scale
        defaultScaleX = gameObject.transform.localScale.x;
        defaultScaleY = gameObject.transform.localScale.y;

        // Start Coroutine Spawn Rocket Bot
        StartCoroutine(SpawnRocketBot(spawnRocketTime));
    }

    private IEnumerator SpawnRocketBot(float time)
    {
        yield return new WaitForSeconds(time);

        // Apply Stretch & Squash Effect before spawn Rocket
        StartCoroutine(StretchSquash(defaultScaleX, defaultScaleY, 2.25f * Mathf.Sign(defaultScaleX),  0.85f * Mathf.Sign(defaultScaleY), 0.8f, 0.15f));

        // Instantiate Rocket Bot
        GameObject rocketBot = Instantiate(rocketPrefab, rocketSpawnPoint.position, Quaternion.Euler(0, 0, 0));

        // Get Rocket Bot´s Script
        RocketBot rocketBotScript = rocketBot.GetComponent<RocketBot>();

        // Set Rocket Launcher Type
        rocketBotScript.type = "VERTICAL";

        // Set Rocket Bot´s move direction
        rocketBotScript.moveDirection = Mathf.Sign(gameObject.transform.localScale.y);

        // Reference Rocket Launcher´s hit box
        rocketBotScript.rocketLauncherHitBox = gameObject.GetComponents<BoxCollider2D>();

        // Start Coroutine again
        StartCoroutine(SpawnRocketBot(spawnRocketTime));
    }

    #endregion

    #region Custom Functions
    private IEnumerator StretchSquash(float scaleX, float scaleY, float targetX, float targetY, float lerpSpeed, float time)
    {
        gameObject.transform.localScale = new Vector3(Mathf.Lerp(scaleX, targetX, lerpSpeed), Mathf.Lerp(scaleY, targetY, lerpSpeed), 1f);
        yield return new WaitForSeconds(time);
        gameObject.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    #endregion
}
