using UnityEngine;
using System.Collections;

public class Poacher : MonoBehaviour 
{
	float speed;
	public float choseSpeed = 5;
	public float scale = 1;
	public int ID;
	public int nInPath;

	private Rigidbody2D rb2d;
	private Animator anim;

	public Transform target0;
	public Transform target1;
	public Transform target2;
	public Transform target3;
	public Transform target4;



	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		speed = choseSpeed;
	}

	void Update ()
	{
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y / 2.56)*-2 + 199;




		/*
		float h = Input.GetAxis ("Horizontal");    //-----Movement-----\\
		if (Input.GetAxis ("Horizontal") < -0.1f)
		{
			rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
		}
		else if (Input.GetAxis ("Horizontal") > 0.1f)
		{
			rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
		}
		else
		{
			rb2d.velocity = new Vector2(0, rb2d.velocity.y);
		}
		
		float v = Input.GetAxis ("Vertical");
		if (Input.GetAxis ("Vertical") < -0.1f)
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, -speed);
		}
		else if (Input.GetAxis ("Vertical") > 0.1f)
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
		}
		else
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
		}
		*/

		//rb2d.velocity = transform.position - string.Concat ("target", nInPath);


		AnimationPoacher ();

	}

	void FindPath ()
	{

	}

	void AnimationPoacher ()
	{
		if (rb2d.velocity.x == 0 && rb2d.velocity.y == 0)
		{
			anim.Play("Idle");
		}
		else
		{
			if (rb2d.velocity.x == 0)
			{
				speed = choseSpeed;
				if (rb2d.velocity.y < 0)
				{
					anim.Play("Front");
				}
				else
				{
					anim.Play("Back");
				}
			}
			else if (rb2d.velocity.y == 0)
			{
				speed = choseSpeed;
				if (rb2d.velocity.x < 0)
				{
					anim.Play("Profile");
					transform.localScale = new Vector2(-scale,scale);
				}
				else
				{
					anim.Play("Profile");
					transform.localScale = new Vector2(scale,scale);
				}
			}
			else if (rb2d.velocity.y < 0)
			{
				speed = choseSpeed*4/5;
				if (rb2d.velocity.x < 0)
				{
					anim.Play("34Front");
					transform.localScale = new Vector2(-scale,scale);
				}
				else if (rb2d.velocity.x > 0)
				{
					anim.Play("34Front");
					transform.localScale = new Vector2(scale,scale);
				}
			}
			else if (rb2d.velocity.y > 0)
			{
				speed = choseSpeed*4/5;
				if (rb2d.velocity.x < 0)
				{
					anim.Play("34Back");
					transform.localScale = new Vector2(-scale,scale);
				}
				else if (rb2d.velocity.x > 0)
				{
					anim.Play("34Back");
					transform.localScale = new Vector2(scale,scale);
				}
			}
		}
	}
}
