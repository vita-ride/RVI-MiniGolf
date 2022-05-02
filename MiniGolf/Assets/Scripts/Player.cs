using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        ball.BallInHole += ProcessBallInHole;
    }

    private void OnDisable()
    {
        ball.BallInHole -= ProcessBallInHole;
    }

    void ProcessBallInHole(int hits)
    {
        Debug.Log("ubacio sam je iz " + hits + " udarca");
    }
}
