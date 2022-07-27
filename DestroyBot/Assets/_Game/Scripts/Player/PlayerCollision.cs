using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    #region Engine Functions

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Obstacle")){
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Objective")){
            GameWin();
        }
    }

    #endregion

    #region Custom Functions

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameWin()
    {

    }

    #endregion
}
