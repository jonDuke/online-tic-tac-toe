using UnityEngine;
using System.Collections;

public class ButtonFunctions : MonoBehaviour {

    /*
     * Container class for functions that buttons can call
     * 
     * Button events can be linked in the inspector
     * */

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void GoToScene(int level)
    {
        Application.LoadLevel(level);
    }

    public void GoToScene(string level)
    {
        Application.LoadLevel(level);
    }
}
