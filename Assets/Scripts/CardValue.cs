using UnityEngine;
using System.Collections;

public class CardValue : MonoBehaviour {

	public string spriteName;
	public int value;

	private Vector3 cardPositionInit;
	private Vector3 distanceWithCard;
	private bool isReturned = false;
	private Vector3 mousePosition;
	private PlayerManager playerManager;	
	private CardManager cardManager;	

	void Start () {
		playerManager = GameObject.Find ("PlayerManager").GetComponent<PlayerManager>();
		cardManager = GameObject.Find ("CardManager").GetComponent<CardManager>();
	}

	void OnMouseDown() {
		CardManager.depth -= 0.0001f;
		cardPositionInit = transform.position;
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		distanceWithCard = cardPositionInit - mousePosition;
	}

	void OnMouseUp() {
		if (Mathf.Abs (cardPositionInit.x - transform.position.x) > 0.5 || Mathf.Abs (cardPositionInit.y - transform.position.y) > 0.5) {
		} else {
			if (!isReturned) {
				if (playerManager.sipsToDistributed <= 0) {
					isReturned = true;
					this.animation.Play("animReturnCard");
					cardManager.LaunchRules(value);
				}
				else 
					Debug.Log ("Toutes les gorgées n'ont pas encore été distribuées !");
			}
		}
	}

	void OnMouseDrag ()
	{
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = mousePosition + distanceWithCard;
		this.transform.position = new Vector3 (transform.position.x, transform.position.y, CardManager.depth);
	}

	void ChangeSprite () {
		Sprite spr = Resources.Load<Sprite>("Cards/" + spriteName);
		SpriteRenderer sprRenderer = (SpriteRenderer)renderer;
		sprRenderer.sprite = spr;
	}
}
