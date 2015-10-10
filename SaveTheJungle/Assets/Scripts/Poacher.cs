using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poacher : MonoBehaviour 
{
	float speed;
	public float choseSpeed = 5;
	public float scale = 1;
	public int ID;
	public int nInPath;
	
	private Rigidbody2D rb2d;
	private Animator anim;
	
	public List<Transform> targets;
	
	
	
	void Start () 
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		speed = choseSpeed;
	}
	
	void Update ()
	{
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y / 2.56)*-2 + 199;
		
		float dist = 0;
		foreach (Transform t in targets) {
			dist = Vector2.Distance( transform.position, t.position );
			if( dist < 2f)
			{
				int value = t.GetComponent<PoacherPath>().IDPath;
				value = value < targets.Count - 1 ? value + 1 : 0;
				nInPath = value;
				break;
			}
		}
		
				
		FindPath ();
		
		
		
		//AnimationPoacher ();
		
	}
	
	void FindPath ()
	{
		for( int i=0; i< targets.Count ; i++ )
			if (nInPath == i)
				rb2d.velocity = -transform.position + targets[i].transform.position;
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
