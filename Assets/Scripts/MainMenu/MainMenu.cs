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

    private void Update()
    {
        if (done)
            return;

        if (Input.anyKey)
        {
            done = true;
            LoadScene();
        }
    }
}
