using UnityEngine;
using System.Collections;

public enum ToolIndex {hand, key, knife, ladder};

public class Tool : MonoBehaviour {
	
	public ToolIndex toolIndex;
	public GameObject cluePrefab;
	
	private GameObject clueInstance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PlayClueAnim(){
		this.GetComponent<SpriteRenderer> ().enabled = false;
		clueInstance = Instantiate(cluePrefab, transform.position, Quaternion.identity) as GameObject;
		clueInstance.transform.position += Vector3.down * 2;
		yield return new WaitForSeconds(2);
		GameObject.Destroy(clueInstance);
		GameObject.Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			other.GetComponent<Player>().AddTool(toolIndex);
			StartCoroutine(PlayClueAnim ());
//			GameObject.Destroy(this.gameObject);

		}
	}
}
