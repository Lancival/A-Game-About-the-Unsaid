using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Script that controls dialogue flow in the visual novel */
// Usage: attach this script to the dialogue container, and provide a dialogue file

public class Conversation : MonoBehaviour {

	[SerializeField] private TextAsset file; // Dialogue source file
	[SerializeField] private GameObject optionPrefab; // Button w/ dialogue option to be instantiated
	private List<Dialogue> nodes; // Extracted dialogue from source file
	private int curr; // Number of current dialogue node
	private float dialogueSpeed = 0.04f; // time interval in seconds between characters being printed on screen

	private TextMeshProUGUI dialogue;
	private TextMeshProUGUI character;

	private bool printing = false;

    void Start() {
        if (optionPrefab == null)
        	Debug.Log("Please provide a prefab to the Conversation script!");
        if (file == null)
        	Debug.Log("Please provide a dialogue source file to the Conversation script!");

        nodes = Dialogue.extract(file.text);
        curr = 0;

        dialogue = gameObject.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        character = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        if (nodes[0].getSpeakerID() == 0)
        	ShowDialogueOptions();
        else
        	NPCResponse();
    }

    void Update() {
    	// Finish printing current dialogue if return key is pressed
    	// TODO: Change this so it will work on other (keyboardless) platforms
    	if (Input.GetKeyDown(KeyCode.Return) == true && printing) {
    		printing = false;
    		dialogue.text = nodes[curr].getText();
    		Debug.Log("Detected return!");
    	}
    }

    private void ShowDialogueOptions() {
    	// Write function here
    }

    private void NPCResponse() {
    	ChangeName();
    	StartCoroutine(Speak());
    }

    private IEnumerator Speak() {
    	ClearDialogue();
    	string speech = nodes[curr].getText();
    	printing = true;
    	for (int i = 0; i < speech.Length; i++) {
    		if (!printing)
    			break;
    		dialogue.text += speech[i];
    		yield return new WaitForSeconds(dialogueSpeed);
    	}
    }

    // Change the name displayed by the name bar to the current speaker
    private void ChangeName() {
    	character.SetText(CharacterInfo.names[nodes[curr].getSpeakerID()]);
    }

    // Clear the text in the dialogue box
    private void ClearDialogue() {
    	dialogue.SetText("");
    }
}
