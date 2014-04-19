using UnityEngine;
using System.Collections;

public class Token : MonoBehaviour {

	private Vector2 centerScreen;
	private Vector3 tokenPositionInit;
	private bool isOpen = false;
	private float angle;
	public int ID;
	private string namePlayer = "Player";
	private int sipsDrunk = 0;
	private int sipsGiven = 0;

	// Use this for initialization
	void Start () {
		centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		tokenPositionInit = transform.position;
	}

	void OnMouseUp () {
		if (Mathf.Abs (tokenPositionInit.x - transform.position.x) > 0.5 || Mathf.Abs (tokenPositionInit.y - transform.position.y) > 0.5) {
		} else {
			isOpen = isOpen ? isOpen = false : isOpen = true;
		}
	}

	void OnMouseDrag () {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.transform.position = new Vector3 (transform.position.x, transform.position.y, CardManager.depth);

		calculAngle ();
	}

	void calculAngle() {
		Vector2 v1 = new Vector2 (0.0f, - Screen.height / 2);
		Vector2 v2 = new Vector2 (this.transform.position.x, this.transform.position.y);
		
		float m = v1.x * v2.x + v1.y * v2.y;
		
		float m1 = Mathf.Sqrt (v1.x * v1.x + v1.y * v1.y);
		float m2 = Mathf.Sqrt (v2.x * v2.x + v2.y * v2.y);
		
		angle = - Mathf.Sign(v1.x * v2.y - v1.y * v2.x) * Mathf.Acos (m / (m1 * m2)) * 180 / Mathf.PI;
	}

	void OnGUI () {
			Vector3 position = Camera.main.WorldToScreenPoint (this.transform.position);
			position = new Vector3 (position.x, Screen.height - position.y);
			GUIUtility.RotateAroundPivot (angle, position);
		if (isOpen) {
			GUI.BeginGroup(new Rect(position.x + 50, position.y - 50, 100, 100));
			GUI.Box (new Rect (0.0f, 0.0f, 100, 100), "");
			namePlayer = GUILayout.TextField(namePlayer, 13, "textfield");
			GUILayout.Label("Sips drunk : " + sipsDrunk);
			GUILayout.Label("Sips given : " + sipsGiven);
			GUI.EndGroup();
		} else {
			GUI.BeginGroup(new Rect(position.x + 50, position.y - 50, 100, 20));
			GUI.Box (new Rect (0.0f, 0.0f, 80, 20), "");
			GUILayout.Label(namePlayer);
			GUI.EndGroup();
		}
	}

	public void SetID (int id) {
		this.ID = id;
	}

	public int GetID () {
		return this.ID;
	}

	public void Active() {
		Component halo = GetComponent("Halo");
		halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
	}

	public void Unactive() {
		Component halo = GetComponent("Halo");
		halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
	}
}
