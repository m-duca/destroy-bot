using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region Engine Functions

    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKey)
        {
            Transition.SetFade(Transition.FADE_IN);
        }
    }
    #endregion
}
