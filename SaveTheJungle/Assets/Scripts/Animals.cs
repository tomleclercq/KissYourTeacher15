using UnityEngine;
using System.Collections;

public class Animals : MonoBehaviour 
{
	Vector3 mousePos = Vector2.zero;
	public Camera myCamera;
	//RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.up*0.1f);

	void FixedUpdate () 
	{
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.up * 0.1f);
            //Debug.DrawRay(mousePos, Vector2.up*0.1f, Color.green);
            if (hit.collider != null)
            {
                foreach (LanguageTranslaterScript lts in GetComponentsInChildren<LanguageTranslaterScript>())
                {
                    if (lts.jsonKey.Contains(hit.collider.tag.ToLower()))
                    {
                        Debug.Log("HIT " + hit.collider.tag);
                        lts.PlaySound();
                    }
                }
            }
        }
	}


}
