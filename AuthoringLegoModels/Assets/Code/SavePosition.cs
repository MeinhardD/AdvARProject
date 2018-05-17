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

        if (model1Pos == null)
        {
            model1Pos = model1.transform.position;
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\Positions.txt", model1Pos.ToString());
        }
        else if (model1 != null && model2Pos == null)
        {
            model2Pos = model2.transform.position;
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\Positions.txt", model2Pos.ToString());
        }
        Debug.Log(Directory.GetCurrentDirectory());

    }

}