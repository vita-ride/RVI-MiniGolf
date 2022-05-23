using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBilboard : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private GameObject player;
    private string playerName = " ";
    private Color color = Color.white;
    
    public void Start()
    {
        camera = Camera.main;

        playerName = player.GetComponent<Player>().playerName;
        color = player.GetComponent<Player>().color;

        gameObject.GetComponent<TextMeshPro>().SetText(playerName);
        gameObject.GetComponent<TextMeshPro>().color = color;
    }

    public void FixedUpdate()
    {
        if (camera != null)
        {
            transform.rotation = camera.transform.rotation;
        }
    }
}
