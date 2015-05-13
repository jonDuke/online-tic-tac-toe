using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoadGame : MonoBehaviour {

    public GameInfoDisplay infoDisplay;
    public GameGrid gameGrid;

	private string url = "http://noblehousegames.x10host.com/tictactoe/loadgame.php";

    
    public void Start()
    {
        int gameID = StaticMemory.currentGame;

        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("playerid"));
        form.AddField("gameid", gameID);

        WWW www = new WWW(url, form);
        StartCoroutine(callLoadingScript(www));
    }

    IEnumerator callLoadingScript(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
            var node = JSON.Parse(www.text);

            infoDisplay.SetDisplay(node["othername"], node["turn"].AsBool);
            StaticMemory.playerType = node["playertype"].AsInt;

            gameGrid.DisplayGame(node["boardstate"]);
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
