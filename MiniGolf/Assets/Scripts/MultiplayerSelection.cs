using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerSelection : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    public int numPlayers;

    private void Start()
    {
        SelectNumPlayers(2);
    }
    public void SelectNumPlayers(int numPlayers)
    {
        Debug.Log("selected " + numPlayers + " players");

        for (int i = numPlayers; i < 4; i++) 
        {
            players[i].gameObject.SetActive(false);
        }

        for (int i = 0; i<numPlayers; i++)
        {
            players[i].gameObject.SetActive(true);
        }

        this.numPlayers = numPlayers;
    }
}
