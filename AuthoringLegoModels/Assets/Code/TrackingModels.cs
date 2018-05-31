using System;
using System.IO;
using UnityEngine;
using Vuforia;

public class TrackingModels : MonoBehaviour {

    public ImageTargetBehaviour currentPart;
    public ImageTargetBehaviour nextPart;

    private GameObject model1;
    private GameObject model2;

    private GameObject model1Target;
    private GameObject model2Target;

    private Vector3 targetPos;
    private Vector3 model2LocalPos;

    private Vector3 relativeVector;
    private Vector3 relativeEulerAngles;
    private Vector3 targetEulerAngles;
    private Quaternion targetRotation;

    string path;

    // Use this for initialization
	void Start () {
        model1 = GameObject.Find("Model 1");
        model2 = GameObject.Find("Model 2");
        model1Target = GameObject.Find("Image_Part1");
        model2Target = GameObject.Find("Image_Part2");
        model2LocalPos = model2.transform.localPosition;
        path = Directory.GetCurrentDirectory() + "\\Assets\\Code\\instructions.txt";

        // Get positions from text file
        string positions = "";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            positions = reader.ReadToEnd();
            reader.Close();

            // Parse into Vector3
            string[] lines = positions.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Debug.Log("Line array: " + lines[0] + ", " + lines[1]);
            relativeVector = StringToVector3(lines[0]);
            relativeEulerAngles = StringToVector3(lines[1]);
        }
    }

    int toggle = 1;
    int counter = 0;
	// Update is called once per frame
	void Update () {

        bool tracked1 = currentPart.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;
        bool tracked2 = nextPart.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;

        //model1.transform.position = model1Pos;

        if (tracked1)
        {
            if (tracked2)
            {
                if (!relativeVector.Equals(new Vector3(0.0f, 0.0f, 0.0f)) && !relativeEulerAngles.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
                {
                    // Calculate target position
                    targetPos = model1Target.transform.position + relativeVector + model2LocalPos;

                    // Calculate direction vector
                    Vector3 increment = (targetPos - model2.transform.position).normalized / 3;

                    // Calculate target rotation
                    targetEulerAngles = model1.transform.eulerAngles + relativeEulerAngles;
                    targetRotation = Quaternion.Euler(targetEulerAngles);

                    // Set model2 rotation to target rotation
                    model2.transform.rotation = targetRotation;

                    float epsilon = 0.1f;
                    if (Mathf.Abs(targetPos.z - model2.transform.position.z) < epsilon
                        && counter < 50)
                    {
                        counter += 1;
                    }
                    else if (Mathf.Abs(targetPos.z - model2.transform.position.z) < epsilon
                        && counter >= 50)
                    {
                        counter = 0;
                        model2.transform.localPosition = model2LocalPos;
                    }
                    else
                    {
                        model2.transform.position += increment;
                    }

                    //Debug.Log("The z position of Model 1 is: " + model1.transform.position.z);
                    //Debug.Log("The z position of Model 2 is: " + model2.transform.position.z);
                    Debug.Log("Counter: " + counter);
                }
                else
                    Debug.Log("Was not able to get the relative vector or the relative euler angles");
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

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}
