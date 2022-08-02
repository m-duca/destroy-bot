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
    private Transform transfObjective;

    #region Engine Functions
    private void Awake()
    {
        sprObjective = gameObject.transform.Find("Objective").GetComponent<SpriteRenderer>();
        transfObjective = gameObject.transform.Find("Objective").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetObjectiveSprite();
        objective = objective.ToUpper();
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
                transfObjective.position = gameObject.transform.position;
                transfObjective.position += new Vector3(0.012f, -0.5f, 0f);
                transfObjective.localScale = new Vector3(1.25f, 1.25f, 1f);
                break;
        }
    }

    #endregion

}
