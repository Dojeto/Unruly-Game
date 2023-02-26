using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private GameObject Player;
    private GameObject Bullet;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private BoxCollider2D cd;
    private Color randomColor;
    private GameObject Particle;
    public new ParticleSystem particleSystem;
    Color[] colors = { new Color(60.0f / 255.0f, 35.0f / 255.0f, 189.0f / 255.0f, 1.0f), new Color(41.0f / 255.0f, 215.0f / 255.0f, 226.0f / 255.0f, 1.0f) };
    // Start is called before the first frame update
    void Start()
    {
        // particleSystem = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<BoxCollider2D>();
        int randomIndex = Random.Range(0, colors.Length);
        randomColor = colors[randomIndex];
        Debug.Log(randomColor);
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
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * 6.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
            SceneManager.LoadScene("MainMenu");
            FindObjectOfType<SAudioManager>().Stop("Theme");
        }
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
            Destroy(col.gameObject);
            FindObjectOfType<SAudioManager>().Play("EnemyDeath");
            CameraShaker.Shake(0.5f, 3.0f);
            ParticleSystemRenderer particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
            Material particleMaterial = particleRenderer.sharedMaterial;

            Material particleMaterialCopy = particleMaterial != null ? new Material(particleMaterial) : new Material(Shader.Find("Particles/Standard Unlit"));
            particleRenderer.material = particleMaterialCopy;

            particleMaterialCopy.color = randomColor;
            Instantiate(particleSystem, transform.position, Quaternion.identity);
        }
    }
}
