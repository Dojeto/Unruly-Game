using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject Player;
    private GameObject Bullet;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private BoxCollider2D cd;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<BoxCollider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Bullet = GameObject.FindGameObjectWithTag("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (Player)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * 5.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
