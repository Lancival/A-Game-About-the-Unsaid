using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	[SerializeField] private Button button;
	[SerializeField] private string sceneName;

    void Start() {
    	button.onClick.AddListener(ChangeScene);
    }

    void ChangeScene() {
    	SceneManager.LoadScene(sceneName);
    }
}
