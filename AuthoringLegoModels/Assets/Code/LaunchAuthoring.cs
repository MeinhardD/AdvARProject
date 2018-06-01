using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LaunchAuthoring : MonoBehaviour {
    
    public void Button_Click()
    {
        TrackingModels.isConsumer = false;

        string path = Directory.GetCurrentDirectory() + "\\Assets\\Code\\instructions.txt";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        SceneManager.LoadScene("Steps/Step1");
        Debug.Log("Change done! Step1 is a go!");
    }
 }
