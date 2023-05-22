using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject bullet;
    public GameObject bulletPrefab;
    private GameObject point;
    private GameObject Player;

    void Start()
    {
        if (!Enemy.IsDead)
        {
            point = GameObject.FindWithTag("Point");
            Player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update() { }

    void LateUpdate()
    {
        if (!Enemy.IsDead && !PauseMenu.GameIsPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Quaternion rotationVector = Quaternion.Euler(0, 0, 90);
                bullet = Instantiate(
                    bulletPrefab,
                    point.transform.position,
                    Player.transform.rotation * rotationVector
                );
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bullet.transform.up * -20;
                FindObjectOfType<AudioManager>().Play("Click");
            }
        }
    }
}
