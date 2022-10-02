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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TriggerNextLevel"))
        {
            Transition.SetFade(Transition.FADE_IN);
        }
        else if (collision.gameObject.CompareTag("TriggerPipe"))
        {
            Player.transf.position = collision.gameObject.transform.position - Vector3.right * 0.05f;
            Player.canPlay = false;
            Player.rb.gravityScale = 0;
            Player.rb.velocity = Vector2.up * 20f;
        }
    }

    #endregion
}
