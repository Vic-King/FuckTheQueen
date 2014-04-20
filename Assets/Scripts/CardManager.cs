using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {

	public GameObject card;
	public static float depth = 0;
	public int currentCard;

	private PlayerManager playerManager;
	private int nbCards = 52;
	private string[] cards = {"c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "cj", "cq", "ck", 
								"d1", "d2", "d3", "d4", "d5", "d6", "d7", "d8", "d9", "d10", "dj", "dq", "dk", 
								"h1", "h2", "h3", "h4", "h5", "h6", "h7", "h8", "h9", "h10", "hj", "hq", "hk",
								"s1", "s2", "s3", "s4", "s5", "s6", "s7", "s8", "s9", "s10", "sj", "sq", "sk"};

	private int[] values = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};

	// Use this for initialization
	void Start () {
		playerManager = GameObject.Find ("PlayerManager").GetComponent<PlayerManager>();
		float x = 0, y = 0, angle;
		Vector3 translation, rotation;
		
		for (int n = 0; n < nbCards; n++) {
			x = Random.Range(-5.0f, 5.0f);
			y = Random.Range(-1.0f, 1.0f);
			angle = Random.Range(0, 180);
			translation = new Vector3(x, y, depth);
			rotation = new Vector3(0, 0, angle);
			GameObject go = (GameObject)Instantiate(card, translation, Quaternion.identity);
			go.transform.Rotate(0, 0, angle);
			go.GetComponent<CardValue>().spriteName = cards[n];
			go.GetComponent<CardValue>().value = values[n % 13];
			depth -= 0.0001f;
		}
	}
	
	public void LaunchRules (int n) {
		playerManager.sipsToDistributed = 0;
		currentCard = n;

		switch (n) {
		case 1:
			Debug.Log ("1 gorgée, un peu d'honneur ?");
			playerManager.sipsToDistributed = 1;
			break;
		case 7:
			Debug.Log ("Thème");
			playerManager.sipsToDistributed = 1;
			break;
		case 8:
			Debug.Log("La règle");
			break;
		case 9:
			Debug.Log("La phrase");
			playerManager.sipsToDistributed = 1;
			break;
		case 10:
			Debug.Log("Le doigt");
			break;
		case 11:
			Debug.Log("Le freeze");
			break;
		case 12:
			Debug.Log("Changement de prénom");
			break;
		case 13:
			Debug.Log("Jeu du doigt");
			playerManager.sipsToDistributed = 1;
			break;
		default:
			Debug.Log(n + " gorgées à distribuer !");
			playerManager.sipsToDistributed = n;
			break;
		}

		playerManager.TestChangePlayer ();
	}
}
