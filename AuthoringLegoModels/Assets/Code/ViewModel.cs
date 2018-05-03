using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewModel : MonoBehaviour {



    public void Button_Click()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Active scene is: " + scene.name);

        if (scene.name.Equals("Step1"))
        {
            SceneManager.LoadScene("Steps/Step2");
            Debug.Log("Change done! Step2 is a go!");
        }
        if (scene.name.Equals("Step2"))
        {
            SceneManager.LoadScene("Steps/Step3");
            Debug.Log("Change done! Step3 is a go!");
        }
    }

}
