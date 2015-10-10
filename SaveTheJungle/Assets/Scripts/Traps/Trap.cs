using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	public ToolIndex requiredTool;
	public GameObject cluePrefab;

	private GameObject clueInstance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			if(!(other.GetComponent<Player>().HasTool(requiredTool))){
				clueInstance = Instantiate(cluePrefab, transform.position, Quaternion.identity) as GameObject;
				clueInstance.transform.position += Vector3.down * 2;
			}
//		   if (Input.GetMouseButtonDown(0))){
//			other.transform.position += Vector3.forward;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			if (clueInstance) GameObject.Destroy(clueInstance);
		}
	}
}
