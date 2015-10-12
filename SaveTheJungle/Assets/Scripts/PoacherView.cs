using UnityEngine;
using System.Collections;

public class PoacherView : MonoBehaviour 
{
	public Transform target;
	public float radius = 4;

	void Start ()
	{
		//target = GameObject.
	}

	void Update ()
	{
		if (Vector3.Distance(target.transform.position, transform.position) < radius)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
