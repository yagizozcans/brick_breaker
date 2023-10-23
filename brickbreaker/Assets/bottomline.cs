using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomline : MonoBehaviour
{

    public float movementSpeed;
    public float ballThrowSpeed;

    float screenX = 0;

    public GameObject ball;


    private void Start()
    {
        screenX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if(- (transform.GetComponent<SpriteRenderer>().bounds.size.x / 2) + transform.position.x > -screenX)
            {
                transform.position += Vector3.left * (Time.deltaTime * movementSpeed);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (transform.GetComponent<SpriteRenderer>().bounds.size.x / 2 + transform.position.x < screenX)
            {
                transform.position += Vector3.right * (Time.deltaTime * movementSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "2x")
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");

            if(balls.Length < 300)
            {
                foreach (GameObject ball in balls)
                {
                    GameObject newball = Instantiate(ball);
                    newball.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    newball.transform.Rotate(0.0f, 0.0f, Random.Range(0, 360.0f));
                    newball.transform.GetComponent<Rigidbody2D>().AddForce(ball.transform.up.normalized * ballThrowSpeed);
                }
            }
            Destroy(collision.gameObject);
        }
    }

    public void PlayGame()
    {
        ball.transform.parent = null;
        ball.transform.Rotate(0.0f, 0.0f, Random.Range(-45.0f, 45.0f));
        ball.transform.GetComponent<Rigidbody2D>().AddForce(ball.transform.up.normalized * ballThrowSpeed);
    }
}
