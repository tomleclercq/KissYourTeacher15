using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	public ToolIndex requiredTool;
	public GameObject cluePrefab = null;
//	public GameObject animalPrefab;
	public GameObject book;

	private GameObject clueInstance = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if((other.tag == "Player") && (cluePrefab != null)){
			if(!(other.GetComponent<Player>().HasTool(requiredTool))){
				clueInstance = Instantiate(cluePrefab, transform.position, Quaternion.identity) as GameObject;
				clueInstance.transform.position += Vector3.down * 2;
			}else{
//				GetComponent<Animator>().SetTrigger("OpenCage");
				GetComponent<SpriteRenderer>().enabled = false;
				book.SetActive(true);
				transform.GetChild(0).gameObject.SetActive(true);
//				Time.timeScale = 0;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
		    if (clueInstance != null){
				GameObject.Destroy(clueInstance);
			}else{
				GetComponent<Animator>().SetTrigger("StopAnim");
			}
		}
	}
}
