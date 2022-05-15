using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickGamemode : MonoBehaviour
{
    [SerializeField] MainMenuManager menuManager;
    public void StartSingleplayer()
    {
        Debug.Log("start single");

        int level = 0;
        GameMode mode = GameMode.Practice;
        int playerCount = 1;
        PlayerInfo[] players = new PlayerInfo[playerCount];
        players[0] = new PlayerInfo("Player One", Color.blue);

        menuManager.InitGame(mode, level, players);
    }
}
