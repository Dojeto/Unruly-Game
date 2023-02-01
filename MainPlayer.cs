using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject bulletPrefab;
    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;

        Vector2 playerToCursor = mousePosition - transform.position;
        float angle = Mathf.Atan2(playerToCursor.y, playerToCursor.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Quaternion rotationVector = Quaternion.Euler(0, 0, 90);
            transform.rotation = transform.rotation * rotationVector;
            bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bullet.transform.up * -20f;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name != bullet.gameObject.name)
        {
            Destroy(this.gameObject);
        }
    }
}
