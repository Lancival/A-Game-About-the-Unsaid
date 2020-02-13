using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageScript : MonoBehaviour {

    [SerializeField] private Button button; // Button which will add a message when clicked
    [SerializeField] private GameObject chatMessagePrefab; // Chat message prefab to be instantiated

    private float SCROLLCONSTANT = -6.5E-2F; // Magic number obtained through testing
    private int MESSAGEPADDING = 200; // How much to left or right pad a message

    void Start() {
    	button.onClick.AddListener(ChooseOption); // Allows the button to activate the AddMessage function when clicked
    }

    // Scroll the chat to the bottom to view the new chat message
    private void ScrollToBottom() {
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = SCROLLCONSTANT;
    }

    // Reset color of button
    private void ResetButtonColor() {
        button.enabled = false;
        button.enabled = true;
    }

    // Add a new chat message
    private void AddMessage(string message, bool player) {
    	var newMessage = Instantiate(chatMessagePrefab, new Vector3(0, 0, 0), Quaternion.identity); // Create new chat message
    	newMessage.transform.SetParent(GameObject.Find("Content").transform, false); // Place message inside chat container
    	newMessage.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(message); // Change text to dialogue

        // Pad message on the left or right, depending on whether message was sent by player or NPC
        if (player == true)
            newMessage.GetComponent<VerticalLayoutGroup>().padding.left = MESSAGEPADDING;
        else
            newMessage.GetComponent<VerticalLayoutGroup>().padding.right = MESSAGEPADDING;
        ScrollToBottom();
        //ResetButtonColor();
    }

    // Handle player's choice of chat message to send
    public void ChooseOption() {
        AddMessage(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, true); // Send player message
        // Change dialogue options to new options
        AddMessage("This is an NPC response placeholder!", false); // Send NPC "response message"
    }
}
