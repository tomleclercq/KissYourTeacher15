using UnityEngine;
using System.Collections;

public class Animals : MonoBehaviour 
{
	Vector3 mousePos;
	
	void FixedUpdate () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.up*0.1f);
			//Debug.DrawRay(mousePos, Vector2.up*0.1f, Color.green);
			Debug.Log (hit.collider.name);
			GetComponentInChildren<LanguageTranslaterScript>().PlaySound();
		}
	}


}
