using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] private GameObject nameField;
    public void UpdatePlayerName(string name, Color color)
    {
        nameField.GetComponent<TextMeshProUGUI>().SetText(name + "'s turn");
        nameField.GetComponent<TextMeshProUGUI>().color = color;
    }
}
