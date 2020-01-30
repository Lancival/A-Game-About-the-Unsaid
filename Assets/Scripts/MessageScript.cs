using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MessageScript : MonoBehaviour
{
    [SerializeField] private Button button;

    void Start() {
    	button.onClick.AddListener(AddMessage);
    }

    void AddMessage() {
    	return;
    }
}
