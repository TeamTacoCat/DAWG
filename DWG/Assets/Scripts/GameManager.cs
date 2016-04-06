using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager GMInstance;
	private static int[] points = new int[4];
	public static int teams{ get; set; }

	private bool levelLoaded = false;
	[SerializeField]private GameObject[] players;
	[SerializeField]private GameObject[] cams;
	[SerializeField]private GameObject look;
	[SerializeField]private GameObject[] UICanvas;
	[SerializeField]private GameObject sigilSpawner;
	public static string curScene{ get; set; }

	public UIHandler.Match curMatch{ get; set; }

	[SerializeField]private GameObject loadScreen;

	// Use this for initialization
	void Start () {
		if (GMInstance) {

			DestroyImmediate (this);

		} else {

			GMInstance = this;
			DontDestroyOnLoad(this);
			DontDestroyOnLoad (loadScreen);
			Application.targetFrameRate = 60;
			//curScene = SceneManager.GetActiveScene ().name;

		}

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) {

			Application.Quit ();

		}


	}

	public void LoadWrapper(string sceneName){

		LoadScene (sceneName);

	}

	public static void LoadScene(string sceneName){

		//SceneManager.LoadScene (sceneName);
		Application.LoadLevel (sceneName);
		if (sceneName == "LevelLayout" || sceneName == "SinglePlayerLevel") {

			for (int i = 0; i < points.Length; i++) {

				points [i] = 0;

			}

		}
		curScene = sceneName;

	}

	public void QuitGame(){

		Application.Quit ();

	}

	public static int[] GetPoints(){

		return points;

	}

	public static void AddPoints(int teamNum){

		points [teamNum - 1]++;

		print ("Current points:");

		for(int i = 0;i<points.Length;i++){

			print("Team "+(i+1)+":"+points[i]);

		}

	}

	public void StartSetup(UIHandler.Match newMatch){
			
		print ("Setup starting. Match:" + newMatch.MatchType);
		curMatch = newMatch;
		StartCoroutine ("SetupMatch", newMatch);
			
	}
		
	IEnumerator SetupMatch(UIHandler.Match newMatch){
			
		print ("Setup Coroutine called");

		levelLoaded = false;
		Time.timeScale = 1f;
			
		if (newMatch.MatchType != Application.loadedLevelName/*SceneManager.GetActiveScene ().name*/) {
			LoadScene (newMatch.MatchType);
		} else {
		
			//SceneManager.UnloadScene (SceneManager.GetActiveScene ().buildIndex);
			LoadScene (newMatch.MatchType);
		
		}
			
		//yield return new WaitUntil (() => levelLoaded == true);

		while (!levelLoaded) {
		
			loadScreen.SetActive (true);
			print ("Loading...");
			yield return null;
			//yield return new WaitForSeconds (.3f);
		

		}

		loadScreen.SetActive (false);
		print ("Loaded.");

		if (newMatch.MatchType == "SinglePlayerLevel") {
				
			SinglePlayerSetup(newMatch);
				
		} else if(newMatch.MatchType == "LevelLayout") {
				
			print ("Multiplayer setup chosen.");
			MultiPlayerSetup(newMatch);
				
		}
			
	}
		
	void OnLevelWasLoaded(){
			
		levelLoaded = true;
			
	}
		
	void SinglePlayerSetup(UIHandler.Match newMatch){

		print ("Single player setup beginning");
		teams = 1;
		GameObject sigilObj = (GameObject)Instantiate (sigilSpawner, Vector3.zero, Quaternion.Euler (0, 0, 0));

		GameObject canvasObj = (GameObject)Instantiate (UICanvas [0], Vector3.zero, Quaternion.Euler (0, 0, 0));
		canvasObj.name = "Canvas1";

		sigilObj.GetComponent<SigilSpawn> ().minimapCanvas = canvasObj;
		sigilObj.GetComponent<SigilSpawn> ().menuHandler = GameObject.Find ("Canvas1/MenuHandler");
		sigilObj.GetComponent<SigilSpawn> ().timer = canvasObj.GetComponentInChildren<TimeAttackClock> ();

		GameObject player = (GameObject)Instantiate (players [0], Vector3.zero, Quaternion.Euler (0, 0, 0));
		player.GetComponent<PlayerController> ().look = (GameObject)Instantiate (look, player.transform.position, Quaternion.Euler (0, 0, 0));
		player.GetComponent<PlayerController> ().cam = (GameObject)Instantiate (cams [0], new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 5), Quaternion.Euler (0, 0, 0));
			
		player.GetComponent<PlayerController> ().cam.GetComponent<PlayerCam> ().target = player.GetComponent<Transform>();

		GameObject fuelTrn = GameObject.Find ("Canvas1/Gauge1/FuelBackground/Fuel");
		GameObject fuelTxt = GameObject.Find ("Canvas1/Gauge1/Text");
		GameObject.Find ("Canvas1/MinimapArrow1").GetComponent<Arrow> ().player = player;

		GameObject.Find ("Canvas1/SearchTracker1").GetComponent<Searcher> ().player = player;
		GameObject.Find ("Canvas1/Score1").GetComponent<Score> ().player = player.GetComponent<Player> ();

		player.transform.position = new Vector3(-14, 1.5f, -516);
		player.GetComponent<PlayerController> ().playerNum = 1;
		player.name = "Player1";
			
	}

	void MultiPlayerSetup(UIHandler.Match newMatch){

		print ("Multiplayer setup happening");
		print ("Number of players:" + newMatch.NumPlayers);
		teams = 0;
		GameObject sigilObj = (GameObject)Instantiate (sigilSpawner, Vector3.zero, Quaternion.Euler (0, 0, 0));
		GameObject canvasObj = null;
		GameObject player;
			
		Vector3[] playerPos = new Vector3[4];
		playerPos [0] = new Vector3 (-14, 1.5f, -516);
		playerPos [1] = new Vector3 (0, 1.5f, 472.5f);
		playerPos [2] = new Vector3 (-520.3f, 1.5f, -76);
		playerPos [3] = new Vector3 (503.6f, 1.5f, -32.1f);
				
		switch (newMatch.NumPlayers) {
					
			case 2:
				canvasObj = (GameObject)Instantiate (UICanvas [1], Vector3.zero, Quaternion.Euler (0, 0, 0));
				break;
			case 3:
				canvasObj = (GameObject)Instantiate (UICanvas [2], Vector3.zero, Quaternion.Euler (0, 0, 0));
				break;
			case 4:
				canvasObj = (GameObject)Instantiate (UICanvas [3], Vector3.zero, Quaternion.Euler (0, 0, 0));
				break;
			default:
				break;
		}

		canvasObj.name = "Canvas" + (curMatch.NumPlayers.ToString ());
				
		sigilObj.GetComponent<SigilSpawn> ().minimapCanvas = canvasObj;
		sigilObj.GetComponent<SigilSpawn> ().menuHandler = GameObject.Find (canvasObj.name + "/MenuHandler");

		int[] teamRoster = new int[4];
				
		for (int i = 0; i < teamRoster.Length; i++) {
							
			teamRoster [i] = 0;
							
		}
				
				
		for (int i = 0; i < newMatch.NumPlayers; i++) {

			print ("Player " + i + " spawning");

			player = (GameObject)Instantiate(players[newMatch.PlayerType[i]], Vector3.zero, Quaternion.Euler(0, 0, 0));
			player.GetComponent<PlayerController> ().look = (GameObject)Instantiate (look, player.transform.position, Quaternion.Euler (0, 0, 0));

			print ("Player look:" + player.GetComponent<PlayerController> ().look.name);
					
			if (newMatch.NumPlayers == 2) {
									
				player.GetComponent<PlayerController> ().cam = (GameObject)Instantiate (cams [i + 1], new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 5), Quaternion.Euler (0, 0, 0));
									
			} else {
									
				player.GetComponent<PlayerController>().cam = (GameObject)Instantiate(cams[i+3], new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 5), Quaternion.Euler (0, 0, 0));
									
			}
					
			player.GetComponent<PlayerController> ().cam.GetComponent<PlayerCam> ().target = player.GetComponent<Transform>();

			GameObject fuelTrn = GameObject.Find (canvasObj.name+("/Gauge"+(i+1).ToString())+"/FuelBackground/Fuel");
			GameObject fuelTxt = GameObject.Find (canvasObj.name+("/Gauge"+(i+1).ToString())+"/Text");
			GameObject.Find (canvasObj.name + ("/MinimapArrow" + (i + 1).ToString ())).GetComponent<Arrow> ().player = player;
			print (GameObject.Find (canvasObj.name + ("/MinimapArrow" + (i + 1).ToString ())).GetComponent<Arrow> ().player.name);
			GameObject.Find (canvasObj.name + ("/SearchTracker" + (i + 1).ToString ())).GetComponent<Searcher> ().player = player;
			GameObject.Find (canvasObj.name + ("/Score" + (i + 1).ToString ())).GetComponent<Score> ().player = player.GetComponent<Player> ();
//			player.GetComponent<PlayerController> ().fuelTransform = fuelTrn.GetComponent<RectTransform>();
//			player.GetComponent<PlayerController> ().visualFuel = fuelTrn.GetComponent<Image>();
//			player.GetComponent<PlayerController> ().fuelText = fuelTxt.GetComponent<Text>();
			teamRoster [player.GetComponent<Player> ().teamNum - 1]++; 

			player.transform.position = playerPos [i];
			player.GetComponent<PlayerController> ().playerNum = i+1;
			player.name = "Player" + (i + 1).ToString ();
					
			}
				
			for (int i = 0; i < teamRoster.Length; i++) {
							
				if (teamRoster [i] > 0) {
								
					teams++;
									
				}
							
			}
				
	}						
}