using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    [SerializeField]
    private float menuTime;

    #region Engine Functions
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("GoToMenu", menuTime);
    }
    #endregion

    #region Custom Functions
    private void GoToMenu()
    {
        Transition.SetFade(Transition.FADE_IN);
    }
    #endregion
}
