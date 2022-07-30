using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [Header("Transition Settings:")]

    [SerializeField, Range(0f, 1f)]
    private float fadeInSpeed;

    [SerializeField, Range(0f, 1f)]
    private float fadeOutSpeed;

    [SerializeField]
    private string nextLevelName;

    public static string FADE_OUT = "Fade Out";
    public static string FADE_IN = "Fade In";

    private static string fade;

    private Image imgTransitionCircle;
    private Image imgTransitionBg;

    #region Engine Functions
    private void Awake()
    {
        imgTransitionBg = GameObject.Find("Canvas").transform.Find("Img Transition Background").GetComponent<Image>();

        SetFade(Transition.FADE_OUT);
    }

    // Update is called once per frame
    private void Update()
    {
        if (fade == Transition.FADE_OUT)
        {
            if (imgTransitionBg.color.a > 0)
            {

                float a = imgTransitionBg.color.a;
                a -= fadeOutSpeed * Time.deltaTime;
                imgTransitionBg.color = new Color(imgTransitionBg.color.r, imgTransitionBg.color.g, imgTransitionBg.color.b, a);

                if (imgTransitionBg.color.a <= 0.2)
                {
                    Player.canPlay = true;
                }
                
            }
            else
            {
                fade = "";
                
            }
        }
        else if (fade == Transition.FADE_IN)
        {
            if (imgTransitionBg.color.a < 1)
            {
                float a = imgTransitionBg.color.a;
                a += fadeInSpeed * Time.deltaTime;
                imgTransitionBg.color = new Color(imgTransitionBg.color.r, imgTransitionBg.color.g, imgTransitionBg.color.b, a);
                Player.canPlay = false;
            }
            else
            {
                // Next Level
                if (nextLevelName != "")
                {
                    SceneManager.LoadScene(nextLevelName);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
        
    }
    #endregion

    #region Custom Functions

    public static void SetFade(string type)
    {
       fade = type;
    }

    #endregion

}
