using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Player;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(GameManager.characters[1], Vector3.zero, Quaternion.identity);
        if (!Enemy.IsDead)
        {
            Player = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!Enemy.IsDead)
        {
            tempPos = transform.position;
            tempPos.x = Player.position.x;
            tempPos.y = Player.position.y;
            transform.position = tempPos;
        }
    }
}
