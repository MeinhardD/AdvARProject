using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingModels : MonoBehaviour {

    public ImageTargetBehaviour currentPart;
    public ImageTargetBehaviour nextPart;
    private GameObject model1;
    private GameObject model2;
    private GameObject model2Target;
    private Vector3 model2LocalPos;

    // Use this for initialization
	void Start () {
        model1 = GameObject.Find("Model 1");
        model2 = GameObject.Find("Model 2");
        model2Target = GameObject.Find("Image_Part2");
        model2LocalPos = model2.transform.localPosition;
    }

    int toggle = 1;
    int counter = 0;
	// Update is called once per frame
	void Update () {

        bool tracked1 = currentPart.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;
        bool tracked2 = nextPart.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;

        if (tracked1)
        {
            if (tracked2)
            {
                Vector3 increment = (model1.transform.position - model2.transform.position).normalized / 3;

                float epsilon = 0.1f;
                if (Mathf.Abs(model1.transform.position.z - model2.transform.position.z) < epsilon
                    && counter < 50)
                {
                    counter += 1;                   
                }
                else if (Mathf.Abs(model1.transform.position.z - model2.transform.position.z) < epsilon
                    && counter >= 50)
                {
                    counter = 0;
                    model2.transform.localPosition = model2LocalPos;
                }
                else
                {
                    model2.transform.position += increment;
                }

                Debug.Log("The z position of Model 1 is: " + model1.transform.position.z);
                Debug.Log("The z position of Model 2 is: " + model2.transform.position.z);
                Debug.Log("Counter: " + counter);
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
