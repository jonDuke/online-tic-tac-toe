using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ChallengeDisplay : MonoBehaviour {

    public Text nameDisplay, statDisplay;
    public int playerid;

    private bool clicked = false;

    private string url = "http://noblehousegames.x10host.com/tictactoe/creategame.php";

    public void SetDisplay(string name, int wins, int losses, int draws)
    {
        nameDisplay.text = "Opponent: " + name;
        statDisplay.text = "Stats: " + wins + "/" + losses + "/" + draws;
    }

    public void CreateGame()
    {
        if (clicked)
            return;

        clicked = true; //prevents sending the request multiple times

        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("playerid"));
        form.AddField("otherid", playerid);

        WWW www = new WWW(url, form);
        StartCoroutine(callCreateGame(www));
    }

    IEnumerator callCreateGame(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
            StaticMemory.currentGame = Convert.ToInt32(www.text);
            Application.LoadLevel("Gameplay");
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
            clicked = false;
        }
    }
}
