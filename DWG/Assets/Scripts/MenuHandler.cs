using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuHandler : MonoBehaviour
{


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

		matchEndMenu.SetActive (true);
		Time.timeScale = 0;
		events.SetSelectedGameObject (defaultMatchEndButton);

	}
}
