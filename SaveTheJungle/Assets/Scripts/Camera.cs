using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour 
{

	private Transform target;

	void Start () 
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update () 
	{
		transform.position = new Vector3 (target.position.x, target.position.y, -10);
	}
}
