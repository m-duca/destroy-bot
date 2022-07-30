using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMonitor : MonoBehaviour
{
    [Header("Objective Monitor Settings:")]

    [SerializeField]
    // SPIKE, ROCKET
    private string objective;

    [SerializeField]
    private Sprite[] objectiveSprites;


    private const string SPIKE = "SPIKE";
    private const string ROCKET = "ROCKET";

    private SpriteRenderer sprObjective;

    #region Engine Functions
    private void Awake()
    {
        sprObjective = gameObject.transform.Find("Objective").GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetObjectiveSprite();
    }

    #endregion

    #region Custom Functions
    private void SetObjectiveSprite()
    {
        switch (objective)
        {
            case SPIKE:
                sprObjective.sprite = objectiveSprites[0];
                break;

            case ROCKET:
                sprObjective.sprite = objectiveSprites[1];
                break;
        }
    }

    #endregion

}
