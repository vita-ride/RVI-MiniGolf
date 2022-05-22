using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiGameManager : MonoBehaviour
{
    public PlayerInfo[] players;
    static MultiGameManager instance;
    //moze da bude matrica ako je fixed broj levela, moze i da se doda u playerInfo struct
    public List<int>[] score;
    public int curLevel;
    private int levelCount;

    public static MultiGameManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        score = new List<int>[players.Length];
        for(int i=0; i < players.Length; i++)
        {
            score[i] = new List<int>();
        }

        levelCount = 3;
        curLevel = 2;

        GameObject.DontDestroyOnLoad(this.gameObject);
        initNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initNextLevel()
    {
        if(curLevel == levelCount)
        {
            //endGame();
        }
        else
        {
        ++curLevel;
        SceneManager.LoadScene($"Scenes/Level_{curLevel}");
        }

    }
}
