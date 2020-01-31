using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageScript : MonoBehaviour
{
    [SerializeField] private Button button; // Button which will add a message when clicked
    [SerializeField] private GameObject chatMessagePrefab; // Chat message prefab to be instantiated
    [SerializeField] private GameObject chatContainer; // Object which contains all of the chat messages
    private string message = "Example message! Replace me with actual dialogue!"; // Chat message to use

    void Start() {
    	// Allows the button to activate the AddMessage function when clicked
    	button.onClick.AddListener(AddMessage);
    }

    // Scroll the chat to the bottom to view the new chat message
    public void ScrollToBottom() {
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = -6.5E-2F; // Magic number obtained through testing
    }

    // Add a new chat message
    public void AddMessage() {
    	var newMessage = Instantiate(chatMessagePrefab, new Vector3(0, 0, 0), Quaternion.identity); // Create new chat message
    	newMessage.transform.SetParent(chatContainer.transform, false); // Place message inside chat container
    	newMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(message); // Change text to dialogue
        ScrollToBottom();
    }
}
