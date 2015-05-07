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

        for (int i = 1; i <= numGames; i++)
        {
            GameObject newGame = Instantiate(displayPrefab);
            GameListDisplay display = newGame.GetComponent<GameListDisplay>();
            display.setDisplay(node[i]["player2name"], node[i]["turn"].AsBool);

            newGame.transform.SetParent(contentPanel.transform);
            newGame.transform.localScale = new Vector3(1, 1, 1); //transform gets set to 2.5 for some reason
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
