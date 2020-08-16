using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField] private GameObject saveSlotsPrefab;

    private static GameObject menu;

	public void ChangeScene() {
		Settings.PAUSED = false;
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
        if (menu != null)
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

    public void SaveGame() {
        Transform slot = EventSystem.current.currentSelectedGameObject.transform;
        PlayerPrefs.SetString("slot" + slot.GetSiblingIndex().ToString(), SceneManager.GetActiveScene().name);
        menu.SetActive(true);
        Destroy(slot.parent.gameObject);
    }

    public void LoadGame() {
        int slot = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        if (PlayerPrefs.HasKey("slot" + slot.ToString())) {
            Settings.PAUSED = false;
            SceneManager.LoadScene(PlayerPrefs.GetString("slot" + slot.ToString()));
        }
    }

    public void OpenSaveSlots() {
        if (menu != null)
            menu.SetActive(false);
        var saveSlots = Instantiate(saveSlotsPrefab, GameObject.Find("Canvas").transform);
        for (int i = 1; i <= 3; i++) {
            saveSlots.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(SaveGame);
            if (PlayerPrefs.HasKey("slot" + i.ToString()) && PlayerPrefs.GetString("slot" + i.ToString()) != "Scene 0")
                saveSlots.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Save Slot 0" + i.ToString() + ": " + PlayerPrefs.GetString("slot" + i.ToString()));
            else
                saveSlots.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Save Slot 0" + i.ToString());
        }
    }

    public void OpenLoadSlots() {
        if (menu != null)
            menu.SetActive(false);
        var saveSlots = Instantiate(saveSlotsPrefab, GameObject.Find("Canvas").transform);
        for (int i = 1; i <= 3; i++) {
            saveSlots.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(LoadGame);

            if (PlayerPrefs.HasKey("slot" + i.ToString()) && PlayerPrefs.GetString("slot" + i.ToString()) != "Scene 0")
                saveSlots.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Save Slot 0" + i.ToString() + ": " + PlayerPrefs.GetString("slot" + i.ToString()));
            else
                saveSlots.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }

    public void LogClick() {
    	Debug.Log(EventSystem.current.currentSelectedGameObject.name + " clicked!");
    }
}
