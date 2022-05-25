using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{

    [SerializeField] public GameObject canvas;
    [SerializeField] private GameObject playerLayout;
    [SerializeField] public GameObject nextLevel;
    [SerializeField] public GameObject title;
    private PlayerInfo[] players;
    private int[] hits;
    private int[] totalHits;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            canvas.transform.gameObject.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            canvas.transform.gameObject.SetActive(false);
        }
    }
    public void FillPlayerData(PlayerInfo[] players)
    {
        this.players = players;
        this.hits = new int[players.Length];
        this.totalHits = MultiGameManager.GetInstance().score;

        for (int i = 0; i < 4; i++)
        {
            GameObject playerScreenDetails = playerLayout.transform.GetChild(i).gameObject;
            playerScreenDetails.SetActive(false);
        }

        for (int i=0; i<players.Length; i++)
        {
            GameObject playerScreenDetails = playerLayout.transform.GetChild(i).gameObject;
            players[i].color.a = 0.65f;
            playerScreenDetails.GetComponent<Image>().color = players[i].color;
            playerScreenDetails.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(players[i].name);
            playerScreenDetails.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(0.ToString() + " (" + totalHits[i].ToString() + ")");
            playerScreenDetails.SetActive(true);
        }
    }

    public void Refresh(int id)
    {
        hits[id]++;
        totalHits[id]++;

        GameObject playerScreenDetails = playerLayout.transform.GetChild(id).gameObject;
        playerScreenDetails.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(hits[id].ToString() + " (" + totalHits[id].ToString() + ")");
    }
    
}
