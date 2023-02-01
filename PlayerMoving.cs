using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    // Start is called before the first frame update
    private float y = Mathf.Sin(0);
    private float x = Mathf.Cos(0);

    private float _speed = 5.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 playerToCursor = mousePosition - transform.position;
            float angle = (Mathf.Atan2(playerToCursor.y, playerToCursor.x) * Mathf.Rad2Deg) - 180.0f;
            y = Mathf.Sin(angle * Mathf.Deg2Rad);
            x = Mathf.Cos(angle * Mathf.Deg2Rad);
        }
        transform.position += new Vector3(x, y, 0f) * Time.deltaTime * _speed;
    }
}
