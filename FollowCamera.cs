using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform Player;
    private Vector3 tempPos;
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public float fromnegativeXRange = -40f;
    public float tonegativeXRange = -60f;
    public float frompositiveXRange = 12f;
    public float topositiveXRange = 20f;
    public float tonegativeYRange = -40f;
    public float frompositiveYRange = 40f;
    private IEnumerator Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(3.0f);
        }
    }

    // Update is called once per frame
    void Update() { }

    void LateUpdate()
    {
        tempPos = transform.position;
        tempPos.x = Player.position.x;
        tempPos.y = Player.position.y;
        transform.position = tempPos;
    }

    private void SpawnEnemy()
    {
        Vector3 spawnLocation = new Vector3(
            Random.Range(-60.0f, -40.0f),
            Random.Range(-40.0f, 40.0f),
            0
        );
        Vector3 SecondspawnLocation = new Vector3(
        Random.Range(40, 60.0f),
        Random.Range(-40.0f, 40.0f),
        0
    );
        GameObject clone = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        GameObject clone2 = Instantiate(enemyPrefab, SecondspawnLocation, Quaternion.identity);
    }
}
