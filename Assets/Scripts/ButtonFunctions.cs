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

	public void ChangeScene() {
    	SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() {
    	Application.Quit();
    }

    public void ShowMenu() {
    	LogClick();
    	EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    	Settings.PAUSED = true;
    	var menu = Instantiate(menuPrefab, GameObject.Find("Canvas").transform);
    	//menu.transform.SetParent(GameObject.Find("Canvas").transform);
    	//menu.transform.GetComponent<RectTransform>().Pos = new Vector3(0, 0, 0);
    }

    public void LogClick() {
    	Debug.Log(EventSystem.current.currentSelectedGameObject.name + " clicked!");
    }
}
