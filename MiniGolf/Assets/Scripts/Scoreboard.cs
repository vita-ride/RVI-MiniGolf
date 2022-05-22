using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject playerLayout;
    public Player player;
    private PlayerInfo[] players;

    private void OnEnable()
    {
        player.PlayerFinished += ProcessPlayerFinished;
    }

    private void OnDisable()
    {
        player.PlayerFinished -= ProcessPlayerFinished;
    }

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
    private void Start()
    {
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    public void FillPlayerData(PlayerInfo[] players)
    {
        this.players = players;

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
            playerScreenDetails.SetActive(true);
        }
    }

    public void Refresh(int[] playerHits)
    {
        for (int i = 0; i < players.Length; i++)
        {
            GameObject playerScreenDetails = playerLayout.transform.GetChild(i).gameObject;

            playerScreenDetails.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(playerHits[i].ToString());
        }
    }

    void ProcessPlayerFinished(int id, int hits)
    {
        Debug.Log("updated sb");
        GameObject playerScreenDetails = playerLayout.transform.GetChild(id).gameObject;

        playerScreenDetails.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(hits.ToString());

        
    }
}
