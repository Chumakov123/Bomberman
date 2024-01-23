using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float lifeTime = 2f;
    public int explosionRange = 3;

    private void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Explode();
        }
    }
    private void Explode()
    {
        Destroy(gameObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
