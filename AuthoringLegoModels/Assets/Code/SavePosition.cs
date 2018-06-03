using System.IO;
using UnityEngine;

public class SavePosition : MonoBehaviour
{

    public GameObject intermediateMachine;
    public GameObject component;

    Vector3 intermediateMachinePos;
    Vector3 componentPos;
    Vector3 relativeVector;

    Vector3 intermediateMachineEulerAngles;
    Vector3 componentEulerAngles;
    Vector3 relativeEulerAngles;

    public void Button_Click()
    {
        Debug.Log("Saved position");
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Code\\instructions.txt";

        if (intermediateMachinePos.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
        {
            Debug.Log("First click");
            intermediateMachinePos = intermediateMachine.transform.position;
            intermediateMachineEulerAngles = intermediateMachine.transform.eulerAngles;
        }
        else if (!intermediateMachinePos.Equals(new Vector3(0.0f, 0.0f, 0.0f)) && componentPos.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
        {
            Debug.Log("Second click");
            componentPos = component.transform.position;
            componentEulerAngles = component.transform.eulerAngles;

            relativeVector = componentPos - intermediateMachinePos;
            relativeEulerAngles = intermediateMachineEulerAngles - componentEulerAngles;

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine(relativeVector.ToString());
                tw.WriteLine(relativeEulerAngles.ToString());
                tw.Close();
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(relativeVector.ToString());
                    tw.WriteLine(relativeEulerAngles.ToString());
                    tw.Close();
                }
            }
        }
            Debug.Log(path);
    }

}