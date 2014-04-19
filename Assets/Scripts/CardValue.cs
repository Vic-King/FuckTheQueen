﻿using UnityEngine;
using System.Collections;

public class CardValue : MonoBehaviour {

	public string spriteName;
	public int value;

	private Vector3 cardPositionInit;
	private Vector3 distanceWithCard;
	private bool isReturned = false;
	private Vector3 mousePosition;

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
				isReturned = true;
				this.animation.Play("animReturnCard");
				CardManager.LaunchRules(value);
				GameObject.Find ("PlayerManager").GetComponent<PlayerManager>().ActiveNextPlayer();
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
		SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
		sprRenderer.sprite = spr;
	}
}
