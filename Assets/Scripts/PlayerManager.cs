using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject token;

	// Use this for initialization
	void Start () {
		CreateTokens (5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateTokens (int n) {
		for (int i = 0; i < n; ++i) {
			GameObject tokenClone = (GameObject) Instantiate(token, new Vector3(0, 0, CardManager.depth), transform.rotation);
		}
	}
}
