using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other) {
		if((other.tag == "Player") && (Input.GetMouseButtonDown(0))){
			other.transform.position += Vector3.forward;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player"){
			other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y ,0);
		}
	}

}
