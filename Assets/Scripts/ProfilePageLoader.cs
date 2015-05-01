using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class ProfilePageLoader : MonoBehaviour {

    public Text nameDisplay, statDisplay;

    private string url = "http://noblehousegames.x10host.com/tictactoe/loadprofile.php";

	void Start () 
    {
        //int PlayerID = PlayerPrefs.GetInt("PlayerID");
        int PlayerID = 1;

        WWWForm form = new WWWForm();

        form.AddField("playerID", PlayerID);

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
