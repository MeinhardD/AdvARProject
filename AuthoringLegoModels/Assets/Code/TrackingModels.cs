using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingModels : MonoBehaviour {

    public ImageTargetBehaviour currentPart;
    public ImageTargetBehaviour nextPart;
    private Vector3 model1Position;
    private Vector3 model2Position;

    // Use this for initialization
	void Start () {
        model1Position = GameObject.Find("Model1").transform.position;
        model2Position = GameObject.Find("Model2").transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        bool tracked1 = currentPart.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;
        bool tracked2 = nextPart.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;

        if (tracked1)
        {
            if (tracked2)
            {
                Debug.Log("Put the part with a sphere on the part with a cube");

                int increment = 1;
                float epsilon = 0.05f;
                if (Mathf.Abs(2 - model1Position.z) < epsilon || Mathf.Abs(-2 - model1Position.z) < epsilon)
                {
                    increment = increment * -1;
                }

                model2Position.z = model2Position.z + increment;
            }               
            else
                Debug.Log("Can't find the next part");
        }
        else
        {
            if (tracked2)
                Debug.Log("Can't find the current part");
            else
                Debug.Log("Can't find either part");
        }
    }
}
