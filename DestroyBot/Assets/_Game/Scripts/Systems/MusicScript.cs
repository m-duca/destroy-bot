using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static float defaultVolume = 0.9f;

    public static float musicCurTime;

    // Components
    public static AudioSource audioSourceMusic;

    #region Engine Functions
    private void Awake()
    {
        audioSourceMusic = gameObject.GetComponent<AudioSource>();
    }
    #endregion
}
