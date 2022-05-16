using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGame : MonoBehaviour
{
    [SerializeField] MainMenuManager menuManager;

    [SerializeField] GameObject players;
    private PlayerInfo[] activePlayers;
    private int numPlayers = 0;

    public void LoadPlayers()
    {

        for(int i = 0; i < 4; i++)
        {
            GameObject child = players.transform.GetChild(i).gameObject;

            if(child.activeSelf)
            {
                numPlayers++;
            }
        }

        activePlayers = new PlayerInfo[numPlayers];

        for (int i = 0; i < 4; i++)
        {
            GameObject player = players.transform.GetChild(i).gameObject;

            if (player.activeSelf)
            {
                string name = player.transform.GetChild(1).GetComponent<TMP_InputField>().text;
                if(name == "")
                {
                    name = "Player" + (i + 1).ToString();
                }
                string colorText = player.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

                Color color = Color.clear;
                ColorUtility.TryParseHtmlString(colorText, out color);

                Debug.Log(name);
                Debug.Log(colorText);

                activePlayers[i] = new PlayerInfo(name, color);
            }
        }


        menuManager.InitGame(GameMode.Multi, 0, activePlayers);

    }
}
