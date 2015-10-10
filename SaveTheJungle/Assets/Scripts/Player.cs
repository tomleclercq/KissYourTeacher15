using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed;
	public int health = 3;

	private Rigidbody2D rb2d;
	private Animator anim;

	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update () 
	{

	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");
		if (Input.GetAxis ("Horizontal") != 0)
		{
			rb2d.velocity = new Vector2(speed * h, rb2d.velocity.y);
		}
		
		float v = Input.GetAxis ("Vertical");
		if (Input.GetAxis ("Vertical") != 0)
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, speed * v);
		}

		if (rb2d.velocity.x > 0.1f)
		{
			if (rb2d.velocity.y == 0)
			{
				anim.Play("Profile");
				transform.localScale = new Vector2(1,1);
			}
			else if (rb2d.velocity.y > 0)
			{
				anim.Play("34Back");
				transform.localScale = new Vector2(1,1);
			}
			else if (rb2d.velocity.y < 0)
			{
				anim.Play("34Front");
				transform.localScale = new Vector2(1,1);
			}
		}
		else if (rb2d.velocity.x < 0.1f)
		{
			if (rb2d.velocity.y == 0)
			{
				anim.Play("Profile");
				transform.localScale = new Vector2(-1,1);
			}
			else if (rb2d.velocity.y > 0)
			{
				anim.Play("34Back");
				transform.localScale = new Vector2(-1,1);
			}
			else if (rb2d.velocity.y < 0)
			{
				anim.Play("34Front");
				transform.localScale = new Vector2(-1,1);
			}
		}
		else if (rb2d.velocity.x == 0)
		{
			if (rb2d.velocity.y > 0)
			{
				anim.Play("Front");
			}
			else if (rb2d.velocity.y < 0)
			{
				anim.Play("Back");
			}
		}
	}
}
