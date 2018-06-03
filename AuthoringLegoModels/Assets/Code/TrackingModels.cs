using System;
using System.IO;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class TrackingModels : MonoBehaviour {

    public ImageTargetBehaviour intermediateMachineImagetargetBehavior;
    public ImageTargetBehaviour componentImagetargetBehavior;

    private GameObject intermediateMachineModel;
    private GameObject componentModel;
    private GameObject[] componentModels;
    private GameObject intermediateMachineTarget;
    private GameObject componentTarget;

    private Vector3 targetPos;
    private Vector3 componentModelLocalPos;

    private Vector3 relativeVector;
    private Vector3 relativeEulerAngles;
    private Vector3 targetEulerAngles;
    private Quaternion targetRotation;

    private Vector3[] relativeVectors;
    private Vector3[] relativeEulerAngless;

    public static bool isConsumer;
    private int index;

    string path;

    // Use this for initialization
	void Start () {
        Scene scene = SceneManager.GetActiveScene();
        index = scene.buildIndex;
        componentModels = new GameObject[index + 2];
        for(int i = 1; i <= index + 2; i++)
        {
            componentModels[i-1] = GameObject.Find("Model " + i);
        }
        intermediateMachineModel = componentModels[index];
        componentModel = componentModels[index + 1];
        intermediateMachineTarget = GameObject.Find("Image_Part" + (index + 1));
        componentTarget = GameObject.Find("Image_Part" + (index + 2));
        componentModelLocalPos = componentModels[index + 1].transform.localPosition;
        path = Directory.GetCurrentDirectory() + "\\Assets\\Code\\instructions.txt";

        // Get positions from text file
        string instructionString = "";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            instructionString = reader.ReadToEnd();
            reader.Close();

            // Parse into Vector3
            string[] lines = instructionString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Debug.Log("Line array: " + lines[0] + ", " + lines[1]);

            relativeVectors = new Vector3[index + 1];
            relativeEulerAngless = new Vector3[index + 1];
            for (int i = 0; i <= index*2; i += 2)
            {
                relativeVectors[i/2] = StringToVector3(lines[i]);
                relativeEulerAngless[i/2] = StringToVector3(lines[i+1]);
            }
        }
    }

    //int toggle = 1;
    int counter = 0;
	// Update is called once per frame
	void Update () {

        bool tracked1 = intermediateMachineImagetargetBehavior.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;
        bool tracked2 = componentImagetargetBehavior.CurrentStatus == ImageTargetBehaviour.Status.TRACKED;

        if (isConsumer)
        {
            if (tracked1)
            {
                if (tracked2)
                {
                    if (File.Exists(path))
                    {
                        Vector3 previousPos = intermediateMachineTarget.transform.position;
                        Vector3 previouisAngles = intermediateMachineModel.transform.eulerAngles;

                        // Set previous components
                        for (int i = index; i > 0; i--)
                        {
                            componentModels[i - 1].transform.position = previousPos + (-1 * relativeVectors[i - 1]) + (-1 * componentModels[i - 1].transform.localPosition);
                            previousPos = componentModels[i - 1].transform.position;

                            Vector3 wantedEulerAngles = previouisAngles + (-1 * relativeEulerAngless[i-1]);
                            componentModels[i - 1].transform.rotation = Quaternion.Euler(wantedEulerAngles);
                            previouisAngles = wantedEulerAngles;
                        }

                        // Calculate target position
                        targetPos = intermediateMachineTarget.transform.position + relativeVectors[index] + componentModelLocalPos;

                        // Calculate direction vector
                        Vector3 increment = (targetPos - componentModel.transform.position).normalized / 3;

                        // Calculate target rotation
                        targetEulerAngles = intermediateMachineModel.transform.eulerAngles + relativeEulerAngless[index-1];
                        targetRotation = Quaternion.Euler(targetEulerAngles);

                        // Set model2 rotation to target rotation
                        componentModel.transform.rotation = targetRotation;

                        float epsilon = 0.1f;
                        if (Mathf.Abs(targetPos.z - componentModel.transform.position.z) < epsilon
                            && counter < 50)
                        {
                            counter += 1;
                        }
                        else if (Mathf.Abs(targetPos.z - componentModel.transform.position.z) < epsilon
                            && counter >= 50)
                        {
                            counter = 0;
                            componentModel.transform.localPosition = componentModelLocalPos;
                        }
                        else
                        {
                            componentModel.transform.position += increment;
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
