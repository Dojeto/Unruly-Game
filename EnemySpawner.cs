using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRadius = 7;

    [SerializeField]
    private float time = 1.0f;
    public GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {
        if (!Enemy.IsDead)
        {
            Vector2 spawnPos = GameObject.FindWithTag("Player").transform.position;
            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(enemies, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(time);
            StartCoroutine(SpawnEnemy());
        }
    }
}
