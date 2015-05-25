using UnityEngine;

public class AndroidBackListener : MonoBehaviour {

    public string sceneToLoad;

	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (sceneToLoad == "Exit")
                Application.Quit();
            else
                Application.LoadLevel(sceneToLoad);
        }
	}
}
