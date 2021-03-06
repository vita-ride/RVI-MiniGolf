using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerInfo[] players;
    public GameObject[] playerObjs;
    private int activePlayers;
    private int[] playerHits;
    private int currPlayerID;
    public GameObject playerPrefab;
    [SerializeField] private CameraControl cameraControl;
    private CPC_CameraPath cameraPath;
    [SerializeField] private Scoreboard scoreboard;
    private PlayerNameUI playerNameUI;
    private int playerCount;
    private bool lvlStarted;

    // Start is called before the first frame update
    void Start()
    {
        players = MultiGameManager.GetInstance().players;
        playerCount = players.Length;
        playerHits = new int[playerCount];
        playerObjs = new GameObject[playerCount];
        activePlayers = playerCount;
        currPlayerID = 0;
        cameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
        cameraPath = GameObject.Find("CameraPath").GetComponent<CPC_CameraPath>();
        scoreboard = GameObject.Find("Scoreboard").GetComponent<Scoreboard>();
        scoreboard.canvas.gameObject.SetActive(false);
        playerNameUI = GameObject.Find("PlayerName").GetComponent<PlayerNameUI>();
        lvlStarted = false;

        cameraPath.PlayPath(10);
    }

    // Update is called once per frame
    void Update()
    {
        if(!lvlStarted && !cameraPath.IsPlaying())
        {
            GameObject.Find("SkipIntro").gameObject.transform.GetChild(0).gameObject.SetActive(false);

            InstantiatePlayers();
            scoreboard.FillPlayerData(players);

            currPlayerID = 0;
            playerObjs[0].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            cameraControl.SetCameraAtPlayer(0);
            playerNameUI.UpdatePlayerName(players[0].name, players[0].color);
            SoundManager.GetInstance().PlayLevelIntro();
            lvlStarted = true;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(GameObject.FindGameObjectWithTag("GameManager"));
            SceneManager.LoadScene("Scenes/MainMenu");
            Cursor.visible = true;
        }
    }

    private void ProcessEndOfTurn(int id)
    {
        playerHits[id]++;
        scoreboard.Refresh(id);
        playerObjs[id].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);

        if (activePlayers != 0)
        {
            int nextPlayerID = (id + 1) % playerCount;
            currPlayerID = nextPlayerID;
            while (true)
            {
                Player nextPlayer = playerObjs[nextPlayerID].GetComponent<Player>();
                if (nextPlayer.ball.hits == 0)
                {
                    playerObjs[nextPlayerID].SetActive(true);
                    nextPlayer.ball.myTurn = true;
                    nextPlayer.ball.wasHitThisTurn = false;
                    playerObjs[nextPlayerID].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    if (cameraControl.inMapView)
                    {
                        cameraControl.ToggleMapView();
                    }
                    cameraControl.SetCameraAtPlayer(nextPlayerID);
                    playerNameUI.UpdatePlayerName(players[nextPlayerID].name, players[nextPlayerID].color);
                    break;
                } 
                else if (playerObjs[nextPlayerID].activeSelf) 
                {
                    Debug.Log(nextPlayerID);
                    nextPlayer.ball.myTurn = true;
                    nextPlayer.ball.wasHitThisTurn = false;
                    playerObjs[nextPlayerID].transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    if (nextPlayerID != id)
                    {
                        if (cameraControl.inMapView)
                        {
                            cameraControl.ToggleMapView();
                        }

                        cameraControl.SetCameraAtPlayer(nextPlayerID);
                    }
                    playerNameUI.UpdatePlayerName(players[nextPlayerID].name, players[nextPlayerID].color);
                    break;
                } 
                else
                {
                    nextPlayerID = (nextPlayerID + 1) % playerCount;
                }
            }
        }
        else
        {
            cameraControl.ToggleMapView();
            cameraControl.lockCamera();
        }
    }

    private void ProcessPlayerFinished(int id, int hits)
    {
        activePlayers--;


        ProcessEndOfTurn(id);
        // zabelezi score igraca
        
        // vidi da li su svi zavrsili
        if(activePlayers == 0)
        {
            playerHits = new int[playerCount];

            scoreboard.isEndOfLevel = true;
            playerNameUI.gameObject.SetActive(false);
            scoreboard.title.GetComponent<TextMeshProUGUI>().SetText($"LEVEL {MultiGameManager.GetInstance().curLevel} RESULTS");
            scoreboard.nextLevel.gameObject.SetActive(true);
            scoreboard.canvas.gameObject.SetActive(true);
            
            Cursor.visible = true;
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
            p.color = playerInfo.color;
            p.id = i;
            player.name = playerInfo.name;
            player.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", playerInfo.color);
            i++;

        }
    }
}
