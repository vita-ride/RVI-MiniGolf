using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillAnim : MonoBehaviour
{
    Transform blades;
    [SerializeField, Range(-0.1f,0.1f)] float rotationAngle = -0.02f;
    // Start is called before the first frame update
    void Start()
    {
        blades = this.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        blades.RotateAround(Vector3.forward, rotationAngle);
    }
}
