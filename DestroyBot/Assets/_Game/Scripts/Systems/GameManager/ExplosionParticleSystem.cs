using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleSystem : MonoBehaviour
{
    [SerializeField]
    private static GameObject explosionPrefab;

    [HideInInspector]
    public static Color32 objectiveColor = new Color32(127, 187, 71, 255);

    [HideInInspector]
    public static Color32 obstacleColor = new Color32(237, 90, 100, 255);

    private void Awake()
    {
        explosionPrefab = Resources.Load("Explosion Particle", typeof(GameObject)) as GameObject;
    }

    public static void SpawnExplosion(float lifeTime, Vector3 position, Color32 color)
    {
        // Instantiate new explosion
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.Euler(0, 0, 0));

        // Change Particles color
        explosion.GetComponent<ParticleSystem>().startColor = color;

        // Set explosion lifetime
        Destroy(explosion, lifeTime);
    }


}
