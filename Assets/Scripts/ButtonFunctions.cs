using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* 
 *  How to Use this Script
 *  Attach this script to the button that should activate one of the functions
 *  below when clicked. Fill in the field for the sceneName if needed. Then,
 *  in the button, navigate to the On Click () section, click plus, and drag
 *  the button from the hierarchy to the new field. Click the drop down menu,
 *  go to button functions, and choose the function that should be activated.
 */

public class ButtonFunctions : MonoBehaviour {
	[SerializeField] private string sceneName;

	public void ChangeScene() {
    	SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() {
    	Application.Quit();
    }
}
