using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiGameManager : MonoBehaviour
{
    public Dictionary<int, PlayerInfo> players;
    static MultiGameManager instance;
    public List<int>[] score;
    public int curLevel;

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

        score = new List<int>[players.Count];
        curLevel = 0;

        GameObject.DontDestroyOnLoad(this.gameObject);
        initNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initNextLevel()
    {
        ++curLevel;
        SceneManager.LoadScene($"Scenes/Level_{curLevel}");

    }
}
