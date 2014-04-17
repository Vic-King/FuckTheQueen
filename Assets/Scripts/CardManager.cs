using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {

	public GameObject card;
	public static float depth = 0;

	private int nbCards = 52;
	private string[] cards = {"c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "cj", "ck", "cq", 
								"d1", "d2", "d3", "d4", "d5", "d6", "d7", "d8", "d9", "d10", "dj", "dk", "dq", 
								"h1", "h2", "h3", "h4", "h5", "h6", "h7", "h8", "h9", "h10", "hj", "hk", "hq",
								"s1", "s2", "s3", "s4", "s5", "s6", "s7", "s8", "s9", "s10", "sj", "sk", "sq"};

	// Use this for initialization
	void Start () {
		float x = 0, y = 0, angle;
		Vector3 translation, rotation;

		for (int n = 0; n < nbCards; n++) {
			x = Random.Range(-6.0f, 6.0f);
			y = Random.Range(-2.0f, 2.0f);
			angle = Random.Range(0, 180);
			translation = new Vector3(x, y, depth);
			rotation = new Vector3(0, 0, angle);
			GameObject go = (GameObject)Instantiate(card, translation, Quaternion.identity);
			go.transform.Rotate(0, 0, angle);
			go.GetComponent<CardValue>().spriteName = cards[n];

			depth -= 0.0001f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
