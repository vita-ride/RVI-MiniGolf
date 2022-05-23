using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class Winner : MonoBehaviour
{
    private int winnerID;
    public GameObject playerPrefab;
    public PhysicMaterial infBounce;

    private void Awake()
    {
        var playerScores = MultiGameManager.GetInstance().score;
        int minScore = playerScores[0].Sum();
        for (int i = 0; i < playerScores.Count(); i++)
        {
            if (playerScores[i].Sum() < minScore)
            {
                winnerID = i;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject winner = Instantiate(playerPrefab, new Vector3(0,-1,0), Quaternion.identity);
        PlayerInfo winnerInfo = MultiGameManager.GetInstance().players[winnerID];
        Debug.Log(winnerInfo.name);
        winner.GetComponent<Player>().playerName = winnerInfo.name;
        winner.GetComponent<Player>().color = winnerInfo.color;
        winner.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", winnerInfo.color);
        winner.transform.GetChild(0).GetComponent<Collider>().material = infBounce;
        winner.transform.localScale += new Vector3(1f, 1f, 1f);
        winner.transform.GetComponentInChildren<TextMeshPro>().alpha = 1;
        //winner.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArrowButton()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
