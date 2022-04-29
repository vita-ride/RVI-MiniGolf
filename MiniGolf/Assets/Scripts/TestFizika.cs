using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFizika : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    System.Random rnd = new System.Random();
    private double x;
    private double z;
    private Vector3 direction = new Vector3(0,0.01f,0);
    [SerializeField] private float force;

    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            x = rnd.NextDouble() * 2 - 1;
            z = rnd.NextDouble() * 2 - 1;
            direction.x = (float)x;
            direction.z = (float)z;
            rigidBody.AddForce(direction.normalized*force, ForceMode.Impulse);
        }
    }
}
