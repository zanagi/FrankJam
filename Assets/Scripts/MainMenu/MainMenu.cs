using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string targetScene;
    private bool done;

    public void LoadScene()
    {
        LoadingScreen.Instance.LoadScene(targetScene);
    }
}
