using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Player playerScript;

    #region Engine Functions

    private void Start()
    {
        playerScript = gameObject.GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Objective")) { 
            playerScript.Respawn(collision.gameObject.tag);
            Player.canPlay = false;
        }
    }

    #endregion
}
