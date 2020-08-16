using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// TODO: Change script to stop all action when game is paused

public class MessageScript : MonoBehaviour {

    [SerializeField] private GameObject chatMessagePrefab; // Chat message prefab to be instantiated
    [SerializeField] private GameObject chatOptionPrefab; // Chat option prefab to be instantiated
    [SerializeField] private TextAsset file; // File which contains dialogue in semicolon separated value format
    private List<Dialogue> nodes; // Dialogue extracted from the file
    private int curr; // ID # of current dialogue node

    private const int MESSAGE_PADDING = 200; // How much to left or right pad a message
    private const int DELAY = 2; // Number of seconds to wait between messages
    // TODO: Get DELAY from settings

    private Color32 PLAYER_COLOR = new Color32(254, 215, 177, 255); // Light orange color
    private Color32 NPC_COLOR = new Color32(173, 216, 230, 255); // Light blue color

    void Start() {
        if (chatMessagePrefab == null) 
            Debug.Log("Error: Please assign the chat message prefab to MessageScript!");
        nodes = Dialogue.extract(file.text);
        curr = 0;

        gameObject.transform.GetChild(1).gameObject.SetActive(false); // Hide the next scene button
        while (nodes[curr].getSpeakerID() == 0) {
            AddMessage(nodes[curr].getText(), true);
            curr = nodes[curr].getResponseID()[0];
        }
        StartCoroutine(AddNPCMessage()); // Start chat coroutine
    }

    private IEnumerator ScrollToBottom() {
        yield return new WaitForEndOfFrame(); // Because Unity is stupid, must wait one frame before scrolling to bottom
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 0F;
    }

    // Add a new chat message
    private void AddMessage(string message, bool player) {
        // Create new message and place it in the chat container
    	var newMessage = Instantiate(chatMessagePrefab, GameObject.Find("Content").transform);
    	newMessage.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(message);

        // Format message depending on whether message was sent by player or NPC
        if (player == true) {
            newMessage.GetComponent<VerticalLayoutGroup>().padding.left = MESSAGE_PADDING;
            newMessage.transform.GetChild(0).GetComponent<Image>().color = PLAYER_COLOR;
        }
        else {
            newMessage.GetComponent<VerticalLayoutGroup>().padding.right = MESSAGE_PADDING;
            newMessage.transform.GetChild(0).GetComponent<Image>().color = NPC_COLOR;
        }

        StartCoroutine(ScrollToBottom());
    }

    // Delete all non-placeholder messages
    private void DeleteOptions() {
        foreach (Transform child in gameObject.transform)
            if (child.gameObject.name != "Wait" && child.gameObject.name != "NextScene")
                GameObject.Destroy(child.gameObject);
    }

    // Send a series of 1 or more NPC messages, with a delay between each NPC message
    private IEnumerator AddNPCMessage() {
        gameObject.transform.GetChild(0).gameObject.SetActive(true); // Add waiting for messages placeholder

        // Delete all other buttons
        DeleteOptions();

        while (nodes[curr].getResponseID()[0] >= 0 && nodes[nodes[curr].getResponseID()[0]].getSpeakerID() > 0) {
        	yield return new WaitForSeconds(DELAY);
            //yield return new WaitForSeconds(nodes[curr].getText().Length / 100 + 1); // Use length of previous message (more convenient for reader)? Or the next message (more realistic)?
            do {
                AddMessage(nodes[curr].getText(), false);
            } while (Settings.PAUSED);
            curr = nodes[curr].getResponseID()[0];
        }

        if (nodes[curr].getResponseID()[0] != 0) {
            do {
                yield return new WaitForSeconds(DELAY);
            } while (Settings.PAUSED);
            AddMessage(nodes[curr].getText(), false);
        }
        if (nodes[curr].getResponseID()[0] == -1) {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            yield break;
        }

        AddOptions();
    }

    private void ChooseOption() {
        if (!Settings.PAUSED) {
            Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            AddMessage(button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, true);
            curr = nodes[curr].getResponseID()[button.transform.GetSiblingIndex() - 2];

            if (nodes[curr].getResponseID()[0] == 0)
                AddOptions();
            else if (nodes[curr].getResponseID()[0] < 0) {
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                DeleteOptions();
            }
            else {
                curr = nodes[curr].getResponseID()[0];
                StartCoroutine(AddNPCMessage());
            }
        }
    }

    private void AddOption(string message) {
        // Create new chat option and place it in the dialogue option box
        var newOption = Instantiate(chatOptionPrefab, gameObject.transform);
        newOption.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(message);

        // Make button add message on click
        newOption.transform.GetComponent<Button>().onClick.AddListener(ChooseOption);
    }

    private void AddOptions() {
        // Disable waiting for messages button
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        foreach (int option in nodes[curr].getResponseID())
            AddOption(nodes[option].getText());
    }
}
