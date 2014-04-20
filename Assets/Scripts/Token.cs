using UnityEngine;
using System.Collections;

public class Token : MonoBehaviour {
	
	public float time;
	public int sipsDrunk = 0;
	public int sipsGiven = 0;
	public int honor = 0;

	private int ID;
	private Vector2 centerScreen;
	private Vector3 tokenPositionInit;
	private bool isOpen = false;
	private bool needOpen = false;
	private float angle = 0.0f;
	private string namePlayer = "Player";
	private float timer = 0.0f;
	private float timerOK = 0.0f;
	private PlayerManager playerManager;
	private Vector3 distanceWithToken;
	private Vector3 mousePosition;

	// Use this for initialization
	void Start () {
		playerManager = GameObject.Find ("PlayerManager").GetComponent<PlayerManager>();

		centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.fixedDeltaTime;
		if ((timer > timerOK + time) && needOpen) {
			isOpen = isOpen ? isOpen = false : isOpen = true;
			needOpen = false;
		}
	}

	void OnMouseDown() {
		tokenPositionInit = transform.position;
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		distanceWithToken = tokenPositionInit - mousePosition;
		timerOK = timer;
		needOpen = true;
	}

	void OnMouseUp () {
		needOpen = false;
		if (Mathf.Abs (tokenPositionInit.x - transform.position.x) > 0.5 || Mathf.Abs (tokenPositionInit.y - transform.position.y) > 0.5) {}
		else {
			if (playerManager.sipsToDistributed > 0) {
				playerManager.TakeSip (ID);
			}
		}
	}

	void OnMouseDrag () {
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (Mathf.Abs (tokenPositionInit.x - transform.position.x) > 0.5 || Mathf.Abs (tokenPositionInit.y - transform.position.y) > 0.5)
			needOpen = false;
		transform.position = mousePosition + distanceWithToken;
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
		if (float.IsNaN(angle))
			angle = 0.0f;
	}

	void OnGUI () {
		ShowStats ();;
	}

	private void ShowStats () {
		Vector3 position = Camera.main.WorldToScreenPoint (this.transform.position);
		position = new Vector3 (position.x, Screen.height - position.y);
		Debug.Log (position + " " + angle);
		GUIUtility.RotateAroundPivot (angle, position);
		
		if (isOpen) {
			GUI.BeginGroup(new Rect(position.x + 50, position.y - 50, 100, 100));
			GUI.Box (new Rect (0.0f, 0.0f, 100, 100), "");
			namePlayer = GUILayout.TextField(namePlayer, 13, "textfield");
			GUILayout.Label("Sips drunk : " + sipsDrunk);
			GUILayout.Label("Sips given : " + sipsGiven);
			GUILayout.Label("Honor : " + honor);
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
