using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject bulletPrefab;
    private GameObject bullet;

    //public Joystick joystick;
    private GameObject point;
    // Start is called before the first frame update
    void Start()
    {
        point = GameObject.FindWithTag("Point");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(joystick.Horizontal);
        //Debug.Log(joystick.Vertical);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;

        Vector2 playerToCursor = mousePosition - transform.position;
        float angle = Mathf.Atan2(playerToCursor.y, playerToCursor.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Quaternion rotationVector = Quaternion.Euler(0, 0, 90);
            FindObjectOfType<SAudioManager>().Play("Click");
            bullet = Instantiate(bulletPrefab, point.transform.position, transform.rotation * rotationVector);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bullet.transform.up * -20f;
        }
    }
}
