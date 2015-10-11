using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	public ToolIndex requiredTool;
	public GameObject cluePrefab = null;
	public GameObject animal;
	public GameObject book;

	private GameObject clueInstance = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (animal) {
//			if (animal.activeSelf) {
//				animal.GetComponent<Rigidbody2D> ().MovePosition(Vector3.down * Time.deltaTime);
//			}
//		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			if((requiredTool == ToolIndex.hand) || (other.GetComponent<Player>().HasTool(requiredTool))){
				GetComponent<SpriteRenderer>().enabled = false;
				//				book.SetActive(true);
				//				Time.timeScale = 0;
				foreach(Transform child in transform){
					child.gameObject.SetActive(true);
				}
//				StartCoroutine(AnimalLeave (GetComponentInChildren<Rigidbody2D>()));
			}else if (cluePrefab != null && clueInstance == null){
				clueInstance = Instantiate(cluePrefab, transform.position, Quaternion.identity) as GameObject;
				clueInstance.transform.position += Vector3.down * 2;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
		    if (clueInstance != null){
				GameObject.Destroy(clueInstance);
				clueInstance.transform.localScale = new Vector2 (0,0);
				print (clueInstance.name);
			}else{
				//GetComponent<Animator>().SetTrigger("StopAnim");
			}
		}
	}

	IEnumerator AnimalLeave(Rigidbody2D animalRB){
		while (animalRB.gameObject) {
			animalRB.MovePosition(Vector3.down);
			yield return new WaitForSeconds(1);
		}
	}
}
