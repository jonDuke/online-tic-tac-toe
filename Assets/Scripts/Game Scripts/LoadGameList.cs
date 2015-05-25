using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoadGameList : MonoBehaviour {

    public GameObject contentPanel, displayPrefab;

    private string url = "http://noblehousegames.x10host.com/tictactoe/getgamelist.php";

    public void Start()
    {
        loadGames();
    }

    void displayGames(string jsondata)
    {
        var node = JSON.Parse(jsondata);

        int numGames = node[0].AsInt;

        ArrayList activeGames = new ArrayList();
        ArrayList inactiveGames = new ArrayList();
        for (int i = 1; i <= numGames; i++)
        {
            GameObject newGame = Instantiate(displayPrefab);
            GameListDisplay display = newGame.GetComponent<GameListDisplay>();
            display.setDisplay(node[i]["player2name"], node[i]["turn"].AsBool, node[i]["status"].AsInt);
            display.gameID = node[i]["gameid"].AsInt;

            if (node[i]["status"].AsInt == 0 && node[i]["turn"].AsBool)
                activeGames.Add(newGame.transform);
            else
                inactiveGames.Add(newGame.transform);
        }

        foreach (Transform game in activeGames)
        {
            game.SetParent(contentPanel.transform);
            game.localScale = new Vector3(1, 1, 1);  //setting parent overrides scale (sets it to 2.5)
        }
        foreach (Transform game in inactiveGames)
        {
            game.SetParent(contentPanel.transform);
            game.localScale = new Vector3(1, 1, 1);
        }
    }

    public void loadGames()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("playerid"));

        WWW www = new WWW(url, form);
        StartCoroutine(callLoadGames(www));
    }

    IEnumerator callLoadGames(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);

            displayGames(www.text);
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
