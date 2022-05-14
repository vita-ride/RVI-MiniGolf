using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerInfo[] players;
    public GameObject[] playerObjs;
    private int activePlayers;
    private int currPlayerID;
    public GameObject playerPrefab;
    [SerializeField] private CameraControl cameraControl;
    private int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        players = MultiGameManager.GetInstance().players;
        playerCount = players.Length;
        playerObjs = new GameObject[playerCount];
        activePlayers = playerCount;
        currPlayerID = 0;
        cameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();

        InstantiatePlayers();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ProcessEndOfTurn(int id)
    {
        if(activePlayers != 0)
        {
            int nextPlayerID = (id + 1) % playerCount;
            while (true)
            {
                Player nextPlayer = playerObjs[nextPlayerID].GetComponent<Player>();
                if (nextPlayer.ball.hits == 0)
                {
                    playerObjs[nextPlayerID].SetActive(true);
                    nextPlayer.ball.myTurn = true;
                    nextPlayer.ball.wasHitThisTurn = false;
                    cameraControl.SetCameraAtPlayer(nextPlayerID);
                    break;
                } 
                else if (playerObjs[nextPlayerID].activeSelf) 
                {
                    Debug.Log(nextPlayerID);
                    nextPlayer.ball.myTurn = true;
                    nextPlayer.ball.wasHitThisTurn = false;
                    if(nextPlayerID != id)
                    {
                        cameraControl.SetCameraAtPlayer(nextPlayerID);
                    }
                    break;
                } 
                else
                {
                    nextPlayerID = (nextPlayerID + 1) % playerCount;
                }
            }
        }
    }

    private void ProcessPlayerFinished(int id, int hits)
    {
        ProcessEndOfTurn(id);
        // zabelezi score igraca
        activePlayers--;
        // vidi da li su svi zavrsili
        if(activePlayers == 0)
        {
            MultiGameManager.GetInstance().initNextLevel();
        }
    }

    void InstantiatePlayers()
    {
        int i = 0;
        foreach (PlayerInfo playerInfo in players)
        {
            GameObject player = Instantiate(playerPrefab);
            if (i != 0)
            {
                player.transform.GetChild(0).GetComponent<Ball>().myTurn = false;
                player.SetActive(false);
            } 
            else
            {
                // ako je playerID = 0 postavi da je njegov red
                player.transform.GetChild(0).GetComponent<Ball>().myTurn = true;
            }
            playerObjs[i] = player;
            Player p = player.GetComponent<Player>();
            p.EndOfTurn += ProcessEndOfTurn;
            p.PlayerFinished += ProcessPlayerFinished;
            p.playerName = playerInfo.name;
            p.id = i;
            player.name = playerInfo.name;
            player.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", playerInfo.color);
            i++;

        }
    }
}