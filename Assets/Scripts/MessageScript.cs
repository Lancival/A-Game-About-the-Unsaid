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
    private string message = "Example message! Replace me with actual dialogue!";

    void Start() {
    	// Allows the button to activate the AddMessage function when clicked
    	button.onClick.AddListener(AddMessage);
    }

    public void AddMessage() {
    	var newMessage = Instantiate(chatMessagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    	newMessage.transform.SetParent(chatContainer.transform, false);
    	newMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(message);
    }
}
