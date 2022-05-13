using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Dictionary<int, PlayerInfo> players;
    public GameObject[] playerObjs;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        players = MultiGameManager.GetInstance().players;
        InstantiatePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiatePlayers()
    {
        foreach (PlayerInfo playerInfo in players.Values)
        {
            GameObject player = Instantiate(playerPrefab);
            player.GetComponent<Player>().playerName = playerInfo.name;
            player.name = playerInfo.name;
            player.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", playerInfo.color);

        }
    }
}
