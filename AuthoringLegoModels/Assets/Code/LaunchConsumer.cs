using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchConsumer : MonoBehaviour
{

    public void Button_Click()
    {
        TrackingModels.isConsumer = true;
        SceneManager.LoadScene("Steps/Step1");
        Debug.Log("Change done! Step1 is a go!");
    }
}
