using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetName : MonoBehaviour {

    public GameObject menuButtonsPanel;

    private string url = "http://noblehousegames.x10host.com/tictactoe/setname.php";


	void Start () 
    {
	
	}

    public void SaveName()
    {
        //get the name
        string newName = GetComponentInChildren<InputField>().text;

        PlayerPrefs.SetString("name", newName);

        //set up the form
        WWWForm webForm = new WWWForm();
        webForm.AddField("id", PlayerPrefs.GetInt("playerid"));
        webForm.AddField("name", newName);

        //call the php script
        WWW www = new WWW(url, webForm);
        StartCoroutine(CallSaveName(www));
    }

    IEnumerator CallSaveName(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);

            //name is set, continue to menu
            Application.LoadLevel("Main Menu");
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
