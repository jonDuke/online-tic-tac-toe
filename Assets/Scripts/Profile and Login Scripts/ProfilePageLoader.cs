using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class ProfilePageLoader : MonoBehaviour {

    public Text nameDisplay, statDisplay;

    private string url = "http://noblehousegames.x10host.com/tictactoe/loadprofile.php";

	void Start () 
    {
        //display saved stats (so they show immediately)
        displayStats(PlayerPrefs.GetString("name"), 
                    new int[3]{PlayerPrefs.GetInt("wins"),
                    PlayerPrefs.GetInt("losses"),
                    PlayerPrefs.GetInt("draws")});

        //start loading stats from DB to update
        WWWForm form = new WWWForm();

        form.AddField("playerID", PlayerPrefs.GetInt("playerid"));

        //Call the server
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
	}

    void displayStats(string name, int[] stats)
    {
        nameDisplay.text = "Name: " + name;
        statDisplay.text = "" + stats[0] + '\n' + stats[1] + '\n' + stats[2];
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);

            var node = JSON.Parse(www.text);

            //save updated stats
            PlayerPrefs.SetString("name", node["name"]);
            PlayerPrefs.SetInt("wins", node["wins"].AsInt);
            PlayerPrefs.SetInt("wins", node["losses"].AsInt);
            PlayerPrefs.SetInt("wins", node["draws"].AsInt);

            //display stats again (in case they changed)
            string playerName = node["name"];
            int[] stats = new int[3] {node["wins"].AsInt,
                                      node["losses"].AsInt,
                                      node["draws"].AsInt};

            displayStats(playerName, stats);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
