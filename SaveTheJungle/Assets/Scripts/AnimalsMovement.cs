using UnityEngine;
using System.Collections;

public class AnimalsMovement : MonoBehaviour
{

    public bool free;
    public float speed;
    public float angle;
    public float timer;
    public float maxTimer;
    public float scale;

	void Update ()
    {
        if (free)
        {
            timer += Time.deltaTime;
            if (timer > maxTimer)
            {
                Direction();
            }

            if ((gameObject.GetComponent<Rigidbody2D>().velocity.x < -0.1f || gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1f) || (gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.1f || gameObject.GetComponent<Rigidbody2D>().velocity.y > 0.1f))
                gameObject.GetComponent<Animator>().speed = 1;
            else
                gameObject.GetComponent<Animator>().speed = 0;

            if (transform.localScale.x == -1)
                this.gameObject.transform.GetChild(0).transform.localScale = new Vector2(-0.75f, this.gameObject.transform.GetChild(0).transform.localScale.y);
            else
                this.gameObject.transform.GetChild(0).transform.localScale = new Vector2(0.75f, this.gameObject.transform.GetChild(0).transform.localScale.y);

            gameObject.GetComponent<Rigidbody2D>().velocity  = (Quaternion.Euler(0, 0, angle) * Vector2.right * speed * scale); // Set Vector to Move

            gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y / 2.56) * -2 + 199; // Set Order In Layer

        }
    }

    void Direction ()
    {
        timer = 0;
        maxTimer = Random.Range(0.5f, 2);
        angle = Random.Range(90, 270);
        scale = ((Random.Range(0, 2)) * 2) - 1;
        
        transform.localScale = new Vector2(scale, transform.localScale.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        angle *= -1;
        scale *= -1;
        timer = 0;
    }
}
