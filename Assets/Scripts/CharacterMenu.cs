using UnityEngine;
using System.Collections;

/// <summary>
///  Class "MenuPlayer" displays the main menu of the player in the first person.
/// </summary>
public class CharacterMenu : MonoBehaviour {
	public GUISkin skin;
	
	public Material mat;

	private float savedTimeScale;
	
	private bool showfps;

	public Color lowFPSColor = Color.red;
	public Color highFPSColor = Color.green;
	
	public int lowFPS = 30;
	public int highFPS = 50;
	
	private bool start = true;

	public Color statColor = Color.yellow;

	public string[] credits= 
	{
		"Project card game",
		"Programming by Victor François",
		"Copyright (c) 2014, VicKing"} ;

	public Texture[] crediticons;
	
	public enum Page 
	{
		None, Main, SettingPart, Options, Scores, Credits
	}
	
	private Page currentPage;

	private float fps;
	
	private int toolbarInt = 0;
	private string[]  toolbarstrings =  {"Audio", "Graphics", "Stats", "System"};
	
	/// <summary>
	/// Pause the game and starts the menu at the beginning of the game.
	/// </summary>
	void Start()
	
	{
		Time.timeScale = 1;
		PauseGame();
	}

	/// <summary>
	/// Pause / unpause the game when the player presses the escape.
	/// </summary>
	void LateUpdate () 
	{
		if (showfps) 
		{
			FPSUpdate();
		}
		
		if (Input.GetKeyDown("escape")) {
			switch (currentPage) {
			case Page.None: 
				PauseGame(); 
				Screen.showCursor = true;
				break;
				
			case Page.Main: 
				if (!IsBeginning()) 
					UnPauseGame(); 
				break;

			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}

	/// <summary>
	/// Navigate through the menus.
	/// </summary>
	void OnGUI () 
	{
		if (skin != null) {
			GUI.skin = skin;
		}
		ShowStatNums();
		if (IsGamePaused()) 
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Stop");
			GUI.color = statColor;
			switch (currentPage) {
				case Page.Main: MainPauseMenu(); break;
				case Page.SettingPart: ShowSettingPart(); break;
				case Page.Options: ShowToolbar(); break;
				case Page.Scores: ShowScores(); break;
				case Page.Credits: ShowCredits(); break;
			}
		}   
	}

	void ShowSettingPart() {
		BeginPage(300,300);
		string stringToEdit = "";
		stringToEdit = GUILayout.TextField(stringToEdit, 25);
		Debug.Log (stringToEdit);
		EndPage();
	}

	/// <summary>
	/// Show the toolbar of the menu.
	/// </summary>
	void ShowToolbar() 
	{
		BeginPage(300,300);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
		switch (toolbarInt) {
			case 0: VolumeControl(); break;
			case 3: ShowDevice(); break;
			case 1: Qualities(); QualityControl(); break;
			case 2: StatControl(); break;
		}
		EndPage();
	}

	/// <summary>
	/// Show the credits of the game.
	/// </summary>
	void ShowCredits() 
	{
		BeginPage(300,300);
		foreach(string credit in credits) {
			GUILayout.Label(credit);
		}
		foreach( Texture credit in crediticons) {
			GUILayout.Label(credit);
		}
		EndPage();
	}

	/// <summary>
	/// Show the scores of the game.
	/// </summary>
	void ShowScores() 
	{
		BeginPage(300,300);
		GUILayout.Label ("The Drunkard : ");
		GUILayout.Label ("The Donor Spanking : ");
		GUILayout.Label ("The Most Honorable : ");
		GUILayout.Label ("The Less Honorable : ");
		EndPage();
	}

	/// <summary>
	/// Display a back button in the menu.
	/// </summary>
	void ShowBackButton() 
	{
		if (GUI.Button(new Rect(20, Screen.height - 50, 50, 20), "Back")) 
		{
			currentPage = Page.Main;
		}
	}

	/// <summary>
	/// Display the current configuration of the game.
	/// </summary>
	void ShowDevice() 
	{
		GUILayout.Label("Unity player version "+Application.unityVersion);
		GUILayout.Label("Graphics: "+SystemInfo.graphicsDeviceName+" "+
		                SystemInfo.graphicsMemorySize+"MB\n"+
		                SystemInfo.graphicsDeviceVersion+"\n"+
		                SystemInfo.graphicsDeviceVendor);
		GUILayout.Label("Shadows: "+SystemInfo.supportsShadows);
		GUILayout.Label("Image Effects: "+SystemInfo.supportsImageEffects);
		GUILayout.Label("Render Textures: "+SystemInfo.supportsRenderTextures);
	}

	/// <summary>
	/// Changes the graphics quality of the game.
	/// </summary>
	void Qualities() 
	{
		switch (QualitySettings.currentLevel) 
		
		{
		case QualityLevel.Fastest:
			GUILayout.Label("Fastest");
			break;
		case QualityLevel.Fast:
			GUILayout.Label("Fast");
			break;
		case QualityLevel.Simple:
			GUILayout.Label("Simple");
			break;
		case QualityLevel.Good:
			GUILayout.Label("Good");
			break;
		case QualityLevel.Beautiful:
			GUILayout.Label("Beautiful");
			break;
		case QualityLevel.Fantastic:
			GUILayout.Label("Fantastic");
			break;
		}
	}

	/// <summary>
	/// Displays two clickable buttons that increase / decrease the graphics quality of the game.
	/// </summary>
	void QualityControl() 
	{
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Decrease")) 
		{
			QualitySettings.DecreaseLevel();
		}
		if (GUILayout.Button("Increase")) 
		{
			QualitySettings.IncreaseLevel();
		}
		GUILayout.EndHorizontal();
	}

	/// <summary>
	/// Displays a slider for controlling the audio volume of the game
	/// </summary>
	void VolumeControl() 
	{
		GUILayout.Label("Volume");
		AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
	}

	/// <summary>
	/// 
	/// </summary>
	void StatControl() 
	{
		GUILayout.BeginHorizontal();
		showfps = GUILayout.Toggle(showfps,"FPS");
		GUILayout.EndHorizontal();
	}

	/// <summary>
	/// Computes the framerate.
	/// </summary>
	void FPSUpdate() 
	{
		float delta = Time.smoothDeltaTime;
		if (!IsGamePaused() && delta !=0.0) 
		{
			fps = 1 / delta;
		}
	}

	/// <summary>
	/// Displays the framerate.
	/// </summary>
	void ShowStatNums() 
	{
		GUILayout.BeginArea(new Rect(Screen.width - 100, 10, 100, 200));
		if (showfps) 
		{
			string fpsstring= fps.ToString ("#,##0 fps");
			GUI.color = Color.Lerp(lowFPSColor, highFPSColor,(fps-lowFPS)/(highFPS-lowFPS));
			GUILayout.Label (fpsstring);
		}
		GUILayout.EndArea();
	}

	/// <summary>
	/// Computes the location of the menu page.
	/// </summary>
	/// <param name=width>Width of the page</param>
	/// <param name=height>Height of the page</param>
	void BeginPage(int width, int height) 
	{
		GUILayout.BeginArea( new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
	}

	/// <summary>
	/// Displays the back button in the menu if needed.
	/// </summary>
	void EndPage() 
	{
		GUILayout.EndArea();
		if (currentPage != Page.Main) 
		{
			ShowBackButton();
		}
	}

	/// <summary>
	/// Checks if it's the beginning of the game or not.
	/// </summary>
	/// <returns>Return "true" if it's the beginnig of the game, "false" otherwise.</returns>
	bool IsBeginning() 
	{
		return start == true;
	}

	public void SetStart (bool b) 
	{
		start = b;
	}
	
	/// <summary>
	/// Function that displays the buttons on the main menu page.
	/// </summary>
	void MainPauseMenu() 
	{
		BeginPage(300, 300);
		if (GUILayout.Button ((IsBeginning()) ? "Play" : "Continue")) 
		{
			currentPage = Page.SettingPart;
			//UnPauseGame();
		}
		if (!IsBeginning()) 
		{
			if (GUILayout.Button ("Restart")) 
			{
				RestartGame();
			}
		}
		if (GUILayout.Button ("Options")) 
		{
			currentPage = Page.Options;
		}
		if (GUILayout.Button ("Scores")) 
		{
			currentPage = Page.Scores;
		}
		if (GUILayout.Button ("Credits")) 
		{
			currentPage = Page.Credits;
		}
		if (GUILayout.Button ("Quit")) 
		{
			QuitGame();
		}

		EndPage();
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	void PauseGame() 
	{
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
		currentPage = Page.Main;
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	void UnPauseGame() 
	{
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		currentPage = Page.None;
		
		if (IsBeginning()) 
		{
			start = false;
		}
	}

	/// <summary>
	/// Restart the game.
	/// </summary>
	public void RestartGame() 
	{
		Application.LoadLevel("Game");
		Time.timeScale = 1;
	}

	/// <summary>
	/// Quit the game.
	/// </summary>
	void QuitGame() 
	{
		Application.Quit();
	}

	/// <summary>
	/// Returns whether the game is paused.
	/// </summary>
	bool IsGamePaused() 
	{
		return (Time.timeScale == 0);
	}

	/// <summary>
	/// Pauses the sound if the game is paused.
	/// </summary>
	void OnApplicationPause(bool pause) 
	{
		if (IsGamePaused()) 
		{
			AudioListener.pause = true;
		}
	}
}