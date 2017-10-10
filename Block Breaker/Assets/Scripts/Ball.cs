using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Paddle paddle;
    private Vector3 paddleToBallVector;
    private bool hasStarted = false;

    private AudioSource audio;
    private Rigidbody2D rigidbody;
    

	// Use this for initialization
	void Start () {
        paddle = GameObject.FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted)
        {
            this.transform.position = paddle.transform.position + paddleToBallVector;


            if (Input.GetMouseButtonDown(0))
            {
                print("mouse clicked");
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 6f);
                hasStarted = true;
            };
        }
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tweak = new Vector2(Random.Range(0f, 0.4f), Random.Range(0f,0.4f));
        if (hasStarted)
        {
            //BallBoing();
            rigidbody.velocity += tweak;
        }
    }

    void BallBoing()
    {
        audio.Play();
    }
}
