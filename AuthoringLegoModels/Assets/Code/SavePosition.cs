using System.IO;
using UnityEngine;

public class SavePosition : MonoBehaviour
{

    GameObject model1;
    GameObject model2;

    Vector3 model1Pos;
    Vector3 model2Pos;
    Vector3 relativeVector;

    Vector3 model1EulerAngles;
    Vector3 model2EulerAngles;
    Vector3 relativeEulerAngles;

    public void Button_Click()
    {
        Debug.Log("Saved position");
        model1 = GameObject.Find("Image_Part1");
        model2 = GameObject.Find("Image_Part2");
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Code\\instructions.txt";

        if (model1Pos.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
        {
            Debug.Log("First click");
            model1Pos = model1.transform.position;
            model1EulerAngles = model1.transform.eulerAngles;
        }
        else if (!model1Pos.Equals(new Vector3(0.0f, 0.0f, 0.0f)) && model2Pos.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
        {
            Debug.Log("Second click");
            model2Pos = model2.transform.position;
            model2EulerAngles = model2.transform.eulerAngles;

            relativeVector = model2Pos - model1Pos;
            relativeEulerAngles = model1EulerAngles - model2EulerAngles;

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