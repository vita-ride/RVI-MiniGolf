using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode { Multi, Practice }

public struct PlayerInfo
{
    public string name;
    public Color color;
    public PlayerInfo(string name, Color color)
    {
        this.name = name;
        this.color = color;
    }
}

public class MainMenuManager : MonoBehaviour
{

    private GameMode mode;
    //public Dictionary<int, PlayerInfo> players;
    public PlayerInfo[] players;
    public GameObject practiceGameManager;
    public GameObject multiGameManager;
    private MultiGameManager gameManager;
    private int playerCount;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitGame(GameMode mode, int level, PlayerInfo[] players)
    {
        if(mode == GameMode.Multi) 
        {
            gameManager = Instantiate(multiGameManager).GetComponent<MultiGameManager>();
            gameManager.name = "multiGameManager";
            gameManager.players = players;
            gameManager.curLevel = level;
        }
        if(mode == GameMode.Practice)
        {
            gameManager = Instantiate(multiGameManager).GetComponent<MultiGameManager>();
            gameManager.name = "singleGameManager";
            gameManager.players = players;
            gameManager.curLevel = level;
        }
    }

}
