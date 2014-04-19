using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject token;

	private int nbPlayers;
	private int currentPlayer = 0;
	private Color[] colors;
	private Token tokenScript;
	private GameObject[] listTokens;

	// Use this for initialization
	void Start () {
		colors = new Color[] {
			new Color(0.0f, 0.0f, 0.0f), 
			new Color(1.0f, 0.0f, 0.0f), 
			new Color(0.0f, 1.0f, 0.0f), 
			new Color(0.0f, 0.0f, 1.0f), 
			new Color(1.0f, 1.0f, 0.0f), 
			new Color(1.0f, 0.0f, 1.0f), 
			new Color(0.0f, 1.0f, 1.0f),
			new Color(1.0f, 1.0f, 1.0f),
			new Color(0.5f, 0.5f, 0.5f),
			new Color(0.5f, 0.5f, 0.0f),
		};
	}

	void Update() {

	}

	public void CreateTokens (int n) {
		nbPlayers = n;
		for (int i = 0; i < n; ++i) {
			GameObject tokenClone = (GameObject) Instantiate(token, new Vector3(0, 0, CardManager.depth), Quaternion.AngleAxis(90, new Vector3(1.0f, 0.0f, 0.0f)));
			tokenClone.renderer.material.color = colors[i];
			tokenScript = tokenClone.GetComponent<Token>();
			tokenScript.SetID (i);
		}

		listTokens = GameObject.FindGameObjectsWithTag ("Token");

		foreach (GameObject t in listTokens) {
			tokenScript = t.GetComponent<Token>();
			
			if (tokenScript.GetID () == 0) {
				tokenScript.Active ();
			}
		}
	}

	public void ActiveNextPlayer () {

		foreach (GameObject t in listTokens) {
			tokenScript = t.GetComponent<Token>();

			if (tokenScript.GetID () == (currentPlayer + 1) % nbPlayers)
				tokenScript.Active ();
			else if (tokenScript.GetID () == currentPlayer)
				tokenScript.Unactive ();
		}

		NextPlayer ();
	}

	void NextPlayer() {
		currentPlayer = (currentPlayer + 1) % nbPlayers;
	}
}
