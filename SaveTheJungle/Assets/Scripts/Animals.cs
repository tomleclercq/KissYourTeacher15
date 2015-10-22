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

            Vector3 p = myCamera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myCamera.nearClipPlane));
            p = myCamera.ViewportToWorldPoint(p);

            mousePos = new Vector2(p.x,p.y);//myCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.up );
            //Debug.DrawRay(mousePos, Vector2.up * 10, Color.blue);
            if (hit.collider != null && hit.distance < 0.1f)
            {
                //print("HIT with" + hit.transform.name);
                foreach (LanguageTranslaterScript lts in GetComponentsInChildren<LanguageTranslaterScript>())
                {
                    if (lts.jsonKey.Contains(hit.collider.tag.ToLower()))
                    {
                        //Debug.Log("HIT " + hit.collider.tag);
                        lts.PlaySound();
                    }
                }
            }
        }
	}


}
