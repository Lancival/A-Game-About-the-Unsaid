using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/* 
 *  How to Use this Script
 *  Attach this script to the button that should activate one of the functions
 *  below when clicked. Then, in the button, navigate to the On Click ()
 *  section, click plus, and drag the button from the hierarchy to the new 
 *  field. Click the drop down menu, go to button functions, and choose the
 *  function that should be activated.
 */

public class ButtonFunctions : MonoBehaviour {
	[SerializeField] private string sceneName;
	[SerializeField] private GameObject menuPrefab;
	[SerializeField] private GameObject quitConfirmationPrefab;

    private static GameObject menu;

	public void ChangeScene() {
    	SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() {
    	Application.Quit();
    }

    public void ReturnToTitle() {
    	var menu = Instantiate(quitConfirmationPrefab, GameObject.Find("Canvas").transform);
    	EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }

    public void Cancel() {
    	menu.SetActive(true);
    	Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject);
    }

    public void OpenMenu() {
    	EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    	Settings.PAUSED = true;
    	menu = Instantiate(menuPrefab, GameObject.Find("Canvas").transform);
    }

    public void CloseMenu() {
    	GameObject.Find("Menu Button").GetComponent<Button>().interactable = true;
    	Settings.PAUSED = false;
    	Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
    }

    public void LogClick() {
    	Debug.Log(EventSystem.current.currentSelectedGameObject.name + " clicked!");
    }
}
