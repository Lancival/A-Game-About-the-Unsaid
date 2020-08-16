using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/* Script that controls dialogue flow in the visual novel */
// Usage: attach this script to the dialogue container, and provide a dialogue file

public class Conversation : MonoBehaviour {

	[SerializeField] private TextAsset file; // Dialogue source file
	[SerializeField] private GameObject optionPrefab; // Button w/ dialogue option to be instantiated
	private List<Dialogue> nodes; // Extracted dialogue from source file
	private int curr; // Number of current dialogue node

    // TODO: Move to globals settings
	private float dialogueSpeed = 0.03f; // time interval in seconds between characters being printed on screen

	private TextMeshProUGUI dialogue; // Text box which contains dialogue
	private TextMeshProUGUI character; // Text box which contains name of the person speaking
    private GameObject nextScene; // Button that moves to the next scene

	private bool printing = false;

    void Start() {
        if (optionPrefab == null)
        	Debug.Log("Error: Please provide a prefab to the Conversation script!");
        if (file == null)
        	Debug.Log("Error: Please provide a dialogue source file to the Conversation script!");

        nodes = Dialogue.extract(file.text);
        curr = 0;

        dialogue = gameObject.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        character = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        nextScene = gameObject.transform.GetChild(1).GetChild(1).GetChild(1).gameObject;

        nextScene.SetActive(false);

        if (nodes[0].getSpeakerID() == 0)
        	Response();
        else
        	ShowDialogueOptions();
    }

    void Update() {
    	// Finish printing current dialogue if return key is pressed
    	// TODO: Change this so it will work on other (keyboardless) platforms

        if (!Settings.PAUSED) {
        	if (Input.GetKeyDown(KeyCode.Return) == true && printing) {
        		printing = false;
        		dialogue.text = nodes[curr].getText();
        	}
        	else if (Input.GetKeyDown(KeyCode.Return) == true && nodes[curr].getResponseID()[0] >= 0) {
        		if (nodes[nodes[curr].getResponseID()[0]].getSpeakerID() == 0) {
        			if (gameObject.transform.GetChild(1).GetChild(1).childCount <= 2)
        				ShowDialogueOptions();
        		}
        		else {
        			curr = nodes[curr].getResponseID()[0];
        			Response();
        		}
        	}
        }
    }

    private void ShowDialogueOptions() {
        List<int> responses = nodes[curr].getResponseID();
        if (responses.Count > 1) {
        	gameObject.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        	ChangeName(CharacterInfo.names[nodes[responses[0]].getSpeakerID()]);
        	foreach (int responseID in responses) {
        		var response = Instantiate(optionPrefab, gameObject.transform.GetChild(1).GetChild(1));
    			response.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(nodes[responseID].getText());
    			response.transform.GetComponent<Button>().onClick.AddListener(ChooseOption);
        	}
        }
        else {
            curr = responses[0];
            Response();
        }
    }

    private void ChooseOption() {
        if (!Settings.PAUSED) {
        	Transform button = EventSystem.current.currentSelectedGameObject.transform;
        	curr = nodes[curr].getResponseID()[button.GetSiblingIndex() - 2];

        	Response();

        	foreach (Transform child in button.parent) {
                if (child.gameObject.name == "Dialogue")
                    child.gameObject.SetActive(true);
                else if (child.gameObject.name == "Next Scene")
                    continue;
                else
                	GameObject.Destroy(child.gameObject);
        	}
        }
    }

    private void Response() {
    	ChangeName(CharacterInfo.names[nodes[curr].getSpeakerID()]);
    	StartCoroutine(Speak());
    }

    private IEnumerator Speak() {
    	ClearDialogue();
    	string speech = nodes[curr].getText();
    	printing = true;
    	for (int i = 0; i < speech.Length; i++) {
    		if (!printing)
    			break;
            if (Settings.PAUSED)
                i--;
            else
    		    dialogue.text += speech[i];
    		yield return new WaitForSeconds(dialogueSpeed);
    	}
        printing = false;

        if (nodes[curr].getResponseID()[0] == -1)
            nextScene.SetActive(true);
    }

    // Change the name displayed by the name bar to the current speaker
    private void ChangeName(string name) {
    	character.SetText(name);
    }

    // Clear the text in the dialogue box
    private void ClearDialogue() {
    	dialogue.SetText("");
    }
}
