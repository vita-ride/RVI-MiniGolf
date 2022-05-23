using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillAnim : MonoBehaviour
{
    Transform blades;
    [SerializeField, Range(-5f,5f)] float rotationAngle = -1.2f;
    // Start is called before the first frame update
    void Start()
    {
        blades = this.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        blades.Rotate(Vector3.forward, rotationAngle);
    }
}
