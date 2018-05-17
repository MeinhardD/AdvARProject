using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePosition : MonoBehaviour
{

    GameObject model1;
    GameObject model2;
    Vector3 model1Pos;
    Vector3 model2Pos;

    public void Button_Click()
    {
        Debug.Log("Saved position");
        model1 = GameObject.Find("Image_Part1");
        model2 = GameObject.Find("Image_Part2");
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Code\\Positions.txt";

        if (model1Pos.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
        {
            model1Pos = model1.transform.position;

            if (!File.Exists(path))
            {
                File.Create(path).Dispose(); ;
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine(model1Pos.ToString());
                tw.Close();
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(model1Pos.ToString());
                    tw.Close();
                }
            }
        }
        else if (!model1Pos.Equals(new Vector3(0.0f, 0.0f, 0.0f)) && model2Pos.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
        {
            model2Pos = model2.transform.position;
            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(model2Pos.ToString());
                tw.Close();
            }
        }
        Debug.Log(path);
    }

}