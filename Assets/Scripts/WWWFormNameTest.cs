using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WWWFormNameTest : MonoBehaviour {

    public Text displayField;
    public InputField nameInput, scoreInput;

    //The URL to the server - In our case localhost with port number 2475
    private string url = "http://noblehousegames.x10host.com/test";

    public void UploadScore()
    {
        //setup url to the php webpage that is going to be called
        string customUrl = url + "/savescore.php";

        //setup a form
        WWWForm form = new WWWForm();

        form.AddField("name", nameInput.text);
        form.AddField("score", scoreInput.text);

        //Call the server
        WWW www = new WWW(customUrl, form);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            //write data returned from php
            Debug.Log(www.text);
            displayField.text = "saved";
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
