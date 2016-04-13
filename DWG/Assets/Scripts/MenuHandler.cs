using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuHandler : MonoBehaviour
{

	public AudioClip[] gamemenuaud = new AudioClip[3]; //size = 3
	[SerializeField]private GameObject pauseMenu;
	[SerializeField]private GameObject matchEndMenu;
	[SerializeField]private EventSystem events;
	[SerializeField]private GameObject defaultPauseButton;
	[SerializeField]private GameObject defaultMatchEndButton;
	private bool paused = false;

	// Use this for initialization
	void Start ()
	{

		events = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		Time.timeScale = 1f;

	}

	// Update is called once per frame
	void Update ()
	{
		print ("MenuHandler Update running");
		pausePressed ();
		if (Input.GetButtonDown ("Cheat")) {
		
			MatchEnd ();

		}

	}

	public void pausePressed ()
	{
		print ("function is being run");
		if (Input.GetButtonDown ("Start")) {
			print ("You pressed start!");
			paused = !paused;

			if (paused) {

				pauseMenu.SetActive (true);
				Time.timeScale = 0;
				events.SetSelectedGameObject (defaultPauseButton);
				if (Input.GetAxis ("Vertical1") != 0 || Input.GetAxis ("Vertical2") != 0 || Input.GetAxis ("Vertical3") != 0 || Input.GetAxis ("Vertical4") != 0) {
					SFX.sound.PlaySound (gamemenuaud [1]);
				}
				if (Input.GetButtonDown ("Jump1") || Input.GetButtonDown ("Jump2") || Input.GetButtonDown ("Jump3") || Input.GetButtonDown ("Jump4") || Input.GetButtonDown ("Start")) {
					SFX.sound.PlaySound (gamemenuaud [0]);
				}

			}
			if (!paused) {

				pauseMenu.SetActive (false);
				Time.timeScale = 1;

			}


		}

	}

	public void ResumeGame ()
	{
		print ("ResumeGame running");
		paused = false;
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void RestartGame()
	{

		GameObject gm = GameObject.Find ("GameManager");
		Time.timeScale = 1f;
		gm.GetComponent<GameManager>().StartSetup (gm.GetComponent<GameManager>().curMatch);

	}

	public void QuitGame()
	{
		Time.timeScale = 1f;
		GameManager.LoadScene ("MainMenu");
	}

	public void MatchEnd(){

		SFX.sound.PlaySound (gamemenuaud [2]);
		print ("Match end menu activated");
		matchEndMenu.SetActive (true);
		Time.timeScale = 0;
		events.SetSelectedGameObject (defaultMatchEndButton);
		if (Input.GetAxis ("Vertical1") != 0 || Input.GetAxis ("Vertical2") != 0 || Input.GetAxis ("Vertical3") != 0 || Input.GetAxis ("Vertical4") != 0) {
			SFX.sound.PlaySound (gamemenuaud [1]);
		}
		if (Input.GetButtonDown ("Jump1") || Input.GetButtonDown ("Jump2") || Input.GetButtonDown ("Jump3") || Input.GetButtonDown ("Jump4")) {
			SFX.sound.PlaySound (gamemenuaud [0]);
		}

	}
}
