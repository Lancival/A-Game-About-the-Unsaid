using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageScript : MonoBehaviour {

    [SerializeField] private Button button; // Button which will add a message when clicked
    [SerializeField] private GameObject chatMessagePrefab; // Chat message prefab to be instantiated

    private float SCROLLCONSTANT = -6.5E-2F; // Magic number obtained through testing

    void Start() {
    	button.onClick.AddListener(AddMessage); // Allows the button to activate the AddMessage function when clicked
    }

    // Scroll the chat to the bottom to view the new chat message
    public void ScrollToBottom() {
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = SCROLLCONSTANT;
    }

    // Reset color of button
    public void ResetButtonColor() {
        button.enabled = false;
        button.enabled = true;
    }

    // Add a new chat message
    public void AddMessage() {
    	var newMessage = Instantiate(chatMessagePrefab, new Vector3(0, 0, 0), Quaternion.identity); // Create new chat message
    	newMessage.transform.SetParent(GameObject.Find("Content").transform, false); // Place message inside chat container
    	newMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text); // Change text to dialogue
        ScrollToBottom();
        //ResetButtonColor();
    }
}
