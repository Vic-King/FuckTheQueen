using UnityEngine;
using System.Collections;

public class CardValue : MonoBehaviour {

	public string spriteName;

	private Vector3 mousePosition;
	private Vector3 cardPositionInit;
	private bool isReturned = false;

	void OnMouseDown() {
		CardManager.depth -= 0.0001f;
		cardPositionInit = transform.position;
	}

	void OnMouseUp() {
		if (Mathf.Abs (cardPositionInit.x - transform.position.x) > 0.5 || Mathf.Abs (cardPositionInit.y - transform.position.y) > 0.5) {
		} else {
			if (!isReturned) {
				isReturned = true;
				this.animation.Play("animReturnCard");
			}
		}
	}

	void OnMouseDrag ()
	{
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.transform.position = new Vector3 (transform.position.x, transform.position.y, CardManager.depth);
	}

	void ChangeSprite () {
		Sprite spr = Resources.Load<Sprite>("Cards/" + spriteName);
		SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
		sprRenderer.sprite = spr;
	}
}
