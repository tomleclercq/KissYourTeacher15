﻿using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {


	void Start () 
	{
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y / 2.56)*-2 + 200;
	}
}
