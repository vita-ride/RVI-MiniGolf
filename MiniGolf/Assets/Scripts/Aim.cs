using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Transform myTransform;
    private Transform barTransform;
    private bool charging;
    private float force;
    private float increment;
    private bool reset;
    private const float ratio = 0.5f;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.gameObject.transform;
        barTransform = myTransform.GetChild(0);
        charging = false;
        reset = true;
        //FIXME: Doesn't work this way for varying fps
        increment = -0.05f;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > 1f/60f) {
            time = 0;
            if (!charging)
            {
                //draw full bar
                if (reset) // this doesn't need to happen every frame
                {
                    Vector3 newScale = barTransform.localScale;
                    Vector3 newPosition = barTransform.localPosition;
                    newScale.z = 1;
                    newPosition.z = ratio;
                    barTransform.localScale = newScale;
                    barTransform.localPosition = newPosition;
                    reset = false;
                }
            } 
            else 
            {
                Vector3 newScale = barTransform.localScale;
                Vector3 newPosition = barTransform.localPosition;
                newScale.z = force;
                newPosition.z = force*ratio;
                barTransform.localScale = newScale;
                barTransform.localPosition = newPosition;

                if (force <= 0.05f || force > 1f)
                {
                    increment *= -1;
                }
                //TODO: maybe have a varying increment
                force += increment;
            }
        }
    }

    public bool isCharging()
    {
        return charging;
    }

    public void charge()
    {
        charging = true;
        force = 1;
    }

    //returns force and stops charge
    public float getForce()
    {
        charging = false;
        reset = true;
        return force;
    }
}
