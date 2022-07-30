using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Player playerScript;

    // Others References
    private ScreenShake screenShakeScript;

    #region Engine Functions

    private void Awake()
    {
        playerScript = gameObject.GetComponent<Player>();

        screenShakeScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Objective")) {
            screenShakeScript.ApplyScreenShake();
            playerScript.Respawn(collision.gameObject.tag);
            Player.canPlay = false;
            Player.rb.velocity = Vector2.zero;
        }
    }

    #endregion
}
