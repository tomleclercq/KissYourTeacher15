using UnityEngine;
using System.Collections;

public class TreeGenerator : MonoBehaviour {

	public GameObject treePrefab;
	int maxWidth = 5;
	int maxHeight = 5;
	
	int nbTrees = 5;

	// Use this for initialization
	void Start () {		
		for (int i = 0;i < nbTrees; i++) {
			GameObject tree =  Instantiate (treePrefab, new Vector3(Random.Range(-1 * maxWidth, maxWidth), Random.Range(-1 * maxHeight, maxHeight), 0), Quaternion.identity) as GameObject;
//			tree.layer = 	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
