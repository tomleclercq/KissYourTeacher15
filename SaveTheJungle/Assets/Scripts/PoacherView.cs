using UnityEngine;
using System.Collections;

public class PoacherView : MonoBehaviour 
{
	public Transform target;

	void Start ()
	{
		//target = GameObject.
	}

	void Update ()
	{
		if (Vector3.Distance(target.transform.position, transform.position) < 5)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
