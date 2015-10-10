﻿using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour 
{
	float speed;
	public float choseSpeed = 5;
	public int health = 3;
	public float scale = 1;

	private Rigidbody2D rb2d;
	private Animator anim;
	private bool[] inventory;

	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		speed = choseSpeed;

		inventory = new bool[Enum.GetValues (typeof(ToolIndex)).Length];
		for(int i = 0; i < Enum.GetValues(typeof(ToolIndex)).Length; i++){
			inventory[i] = false;
		}
	}
	void Update ()
	{
<<<<<<< HEAD
//		gameObject.layer = (int)(transform.position.y / 2.56f - 1);
=======
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y / 2.56)*-2 + 199;
>>>>>>> cf44b921426cc154e4304f6ec35cf60ead1194e6
	}

	void FixedUpdate ()
	{
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

	public bool HasTool(ToolIndex toolIndex){
		return inventory [(int)toolIndex];
	}

	public void AddTool(ToolIndex toolIndex){
		inventory [(int)toolIndex] = true;
	}
}
