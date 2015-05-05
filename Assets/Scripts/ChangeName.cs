using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeName : MonoBehaviour {

    public GameObject changeButton;
    public InputField inputField;
    public Text nameDisplay;

    private bool settingName = false;

    private string url = "http://noblehousegames.x10host.com/tictactoe/setname.php";


    public void PressButton()
    {
        if (settingName)
        {
            //save name
            string newName = inputField.text;
            nameDisplay.text = "Name: " + newName;
            PlayerPrefs.SetString("name", newName);

            //change display
            inputField.gameObject.SetActive(false);
            changeButton.GetComponentInChildren<Text>().text = "Change";
            settingName = false;

            //set up the form
            WWWForm webForm = new WWWForm();
            webForm.AddField("id", PlayerPrefs.GetInt("playerid"));
            webForm.AddField("name", newName);

            //call the php script
            WWW www = new WWW(url, webForm);
            StartCoroutine(CallSaveName(www));
        }
        else
        {
            //show dialogue
            inputField.gameObject.SetActive(true);
            changeButton.GetComponentInChildren<Text>().text = "Save";
            settingName = true;
        }
    }

    IEnumerator CallSaveName(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
            Debug.Log(www.text);
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
