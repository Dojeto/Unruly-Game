using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static bool IsDead = false;
    private GameObject Player;
    private GameObject Bullet;
    private Vector2 moveDirection;
    private Color randomColor;
    public new ParticleSystem particleSystem;

    Color[] colors =
    {
        new Color(60.0f / 255.0f, 35.0f / 255.0f, 189.0f / 255.0f, 1.0f),
        new Color(41.0f / 255.0f, 215.0f / 255.0f, 226.0f / 255.0f, 1.0f)
    };
    private Rigidbody2D rb;
    Score scoring;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoring = GameObject.Find("ScoreManager").GetComponent<Score>();
        int randomIndex = Random.Range(0, colors.Length);
        randomColor = colors[randomIndex];
        GetComponent<Renderer>().material.color = randomColor;
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

    void FixedUpdate()
    {
        if (Player)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * WaveSystem.enemySpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
            IsDead = true;
            FindObjectOfType<AudioManager>().Stop("Theme");
            Score.CalculateScore();
            ProgressSystem.progression += Mathf.Clamp(Score.score / 1000f, 0f, 1f);
            PlayerPrefs.SetFloat("progress", ProgressSystem.progression);
            PlayerPrefs.Save();
            Debug.Log(Score.score);
        }
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            scoring.AddScore(1);

            CameraShaker.Shake(0.5f, 2.0f);

            FindObjectOfType<AudioManager>().Play("EnemyDeath");

            ParticleSystemRenderer particleRenderer =
                particleSystem.GetComponent<ParticleSystemRenderer>();
            Material particleMaterial = particleRenderer.sharedMaterial;

            Material particleMaterialCopy =
                particleMaterial != null
                    ? new Material(particleMaterial)
                    : new Material(Shader.Find("Particles/Standard Unlit"));
            particleRenderer.material = particleMaterialCopy;

            particleMaterialCopy.color = randomColor;
            Instantiate(particleSystem, transform.position, Quaternion.identity);
        }

        // if (col.gameObject.CompareTag("Enemy"))
        // {
        //     Rigidbody2D rb1 = this.gameObject.GetComponent<Rigidbody2D>();
        //     Rigidbody2D rb2 = col.gameObject.GetComponent<Rigidbody2D>();
        //     Vector2 force =
        //         (transform.position - col.gameObject.transform.position).normalized * 100f;
        //     // Debug.Log(force);
        //     rb1.AddForce(force);
        //     rb2.AddForce(-force);
        //     // Debug.Log("WHy Not working");
        // }
    }
}
