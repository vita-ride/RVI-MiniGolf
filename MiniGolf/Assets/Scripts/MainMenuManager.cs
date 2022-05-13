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
    public Dictionary<int, PlayerInfo> players;
    private int level;
    public GameObject practiceGameManager;
    public GameObject multiGameManager;
    private MultiGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        mode = GameMode.Multi;
        players = new Dictionary<int, PlayerInfo>();
        players.Add(1, new PlayerInfo("Pera", Color.blue));
        players.Add(2, new PlayerInfo("Mika", Color.red));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InitGame(mode, level, players);
        }
    }

    private void InitGame(GameMode mode, int level, Dictionary<int, PlayerInfo> players)
    {
        if(mode == GameMode.Multi) {
            gameManager = Instantiate(multiGameManager).GetComponent<MultiGameManager>();
            gameManager.name = "multiGameManager";
            gameManager.players = players;
        }
    }

}
