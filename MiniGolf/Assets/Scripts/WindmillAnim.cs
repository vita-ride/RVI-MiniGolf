using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillAnim : MonoBehaviour
{
    Transform blades;
    [SerializeField, Range(-5f, 5f)] float rotationAngle;
    // Start is called before the first frame update
    void Start()
    {
        blades = this.transform.GetChild(0).transform;
        rotationAngle = -1.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        blades.Rotate(Vector3.forward, rotationAngle);
    }
}
