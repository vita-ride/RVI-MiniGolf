using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Ball ball;
    public int id;
    public string playerName;

    public delegate void EndOfTurnAction(int id);
    public event EndOfTurnAction EndOfTurn;

    public delegate void PlayerFinishedAction(int id, int hits);
    public event PlayerFinishedAction PlayerFinished;

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
        ball.BallStopped += ProcessBallStopped;
    }

    private void OnDisable()
    {
        ball.BallInHole -= ProcessBallInHole;
        ball.BallStopped -= ProcessBallStopped;
    }

    void ProcessBallStopped()
    {
        EndOfTurn?.Invoke(id);
    }

    void ProcessBallInHole(int hits)
    {
        PlayerFinished?.Invoke(id, hits);
        GameObject.FindGameObjectWithTag("Flag").GetComponent<Flag>().OnTriggerExit(ball.GetComponent<Collider>());
        transform.gameObject.SetActive(false);
        Debug.Log("ubacio sam je iz " + hits + " udarca");
    }
}
