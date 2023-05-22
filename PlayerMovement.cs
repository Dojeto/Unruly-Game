using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float y = Mathf.Sin(0);
    private float x = Mathf.Cos(0);
    private float _speed = 6.0f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!Enemy.IsDead && !PauseMenu.GameIsPaused)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;

            Vector2 playerToCursor = mousePosition - transform.position;
            float angle = Mathf.Atan2(playerToCursor.y, playerToCursor.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (Input.GetMouseButtonDown(0))
            {
                float anglez = (transform.eulerAngles.z) - 180.0f;
                y = Mathf.Sin(anglez * Mathf.Deg2Rad);
                x = Mathf.Cos(anglez * Mathf.Deg2Rad);
            }
            transform.position += new Vector3(x, y, 0f) * Time.deltaTime * _speed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
            Enemy.IsDead = true;
            FindObjectOfType<AudioManager>().Stop("Theme");
            Score.CalculateScore();
            ProgressSystem.progression += Mathf.Clamp(Score.score / 1000f, 0f, 1f);
            PlayerPrefs.SetFloat("progress", ProgressSystem.progression);
            PlayerPrefs.Save();
            Debug.Log(Score.score);
        }
    }
}
