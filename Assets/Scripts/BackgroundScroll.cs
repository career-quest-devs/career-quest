using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float translateSpeed;
    public float destroyTime;
    public float spawnDistance;
    public float spawnPoint;

    public GameObject background;

    private bool deathFlag;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Time.deltaTime * translateSpeed * Vector2.left);
        if (gameObject.transform.position.x <= spawnDistance && !deathFlag)
        {
            SpawnNew();
            Invoke("DestroySelf", destroyTime);
            deathFlag = true;
        }
    }

    private void SpawnNew() // Would like to make this into an object pool
    {
        Instantiate(background, Vector3.right * spawnPoint + Vector3.up * 0.26f, Quaternion.identity);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
