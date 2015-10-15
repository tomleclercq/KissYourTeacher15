using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	public ToolIndex requiredTool;
	public GameObject cluePrefab = null;
	GameObject book;

	private GameObject clueInstance = null;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			if((requiredTool == ToolIndex.hand) || (other.GetComponent<Player>().HasTool(requiredTool))){
				GetComponent<SpriteRenderer>().enabled = false;
				foreach(Transform child in transform){
					child.GetComponent<SpriteRenderer>().enabled = true;
					child.GetComponentInChildren<MeshRenderer>().enabled = true;
				}
                StartCoroutine(OpenBookInAfew(0.75f));

			}else if (cluePrefab != null && clueInstance == null){
				clueInstance = Instantiate(cluePrefab, transform.position, Quaternion.identity) as GameObject;
				clueInstance.transform.position += Vector3.down * 2;
			}
		}
	}

	IEnumerator OpenBookInAfew( float _second)
	{
		yield return new WaitForSeconds(_second);
        ApplicationScript.current.UIRoot.Book.GetComponent<BookScript>().AddNewCollection(GetComponentInChildren<LanguageTranslaterScript>().jsonKey);
		
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
