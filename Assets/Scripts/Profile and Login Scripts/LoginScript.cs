using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoginScript : MonoBehaviour {

    public GameObject setNamePanel;

    private bool newProfile = true;
    private string url = "http://noblehousegames.x10host.com/tictactoe/checkprofile.php";
	

	void Start() 
    {
        int playerID = -1;

        if (PlayerPrefs.HasKey("playerid"))
        {
            newProfile = false;
            playerID = PlayerPrefs.GetInt("playerid");
            Debug.Log("id found: " + playerID);
        }
        else
        {
            Debug.Log("id not found");
            newProfile = true;
        }

        WWWForm form = new WWWForm();
        form.AddField("playerID", playerID);

        WWW www = new WWW(url, form);
        StartCoroutine(LoadProfileData(www));
	}

    IEnumerator LoadProfileData(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);

            if (newProfile)
            {
                //save the new playerid
                var node = JSON.Parse(www.text);
                PlayerPrefs.SetInt("playerid", node["id"].AsInt);

                //save default values
                PlayerPrefs.SetString("name", "name"); //default name as defined in the database
                PlayerPrefs.SetInt("wins", 0);
                PlayerPrefs.SetInt("losses", 0);
                PlayerPrefs.SetInt("draws", 0);

                //show create name window
                setNamePanel.SetActive(true);
            }
            else
            {
                if (PlayerPrefs.GetString("name") != www.text)
                {
                    Debug.Log("warning, saved name didn't match the database!");
                    PlayerPrefs.SetString("name", www.text);
                }
                else
                {
                    Debug.Log("name confirmed, login complete");
                }

                Application.LoadLevel("Main Menu");
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
