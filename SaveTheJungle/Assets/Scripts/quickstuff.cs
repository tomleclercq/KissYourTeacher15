using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class quickstuff : MonoBehaviour {

	public void SetLittleChild( bool _state)
	{
		Transform[] parents = GetComponentsInChildren<Transform>();
		List<Transform> childs = new List<Transform> ();

		foreach( Transform p in parents )
			if( p != transform )
				childs.Add ( p.GetComponentsInChildren<Transform>().First());

		foreach (Transform c in childs)
			c.gameObject.GetComponent<Renderer>().enabled = _state;

	}
}
