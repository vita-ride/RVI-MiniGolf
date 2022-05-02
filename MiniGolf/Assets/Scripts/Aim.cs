using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: SHOT STRENGTH WITH VISUALS

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myTransform.Rotate(0, -0.8f, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            myTransform.Rotate(0, 0.8f, 0);
        }

    }
}
