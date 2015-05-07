using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoadChallenges : MonoBehaviour {

    public GameObject contentPanel;
    public GameObject displayPrefab;

    private int offset = 0;
    private string url = "http://noblehousegames.x10host.com/tictactoe/getchallenges.php";

	void Start () 
    {
        loadChallenges();
	}

    void displayChallenges(string jsondata)
    {
        var node = JSON.Parse(jsondata);

        int numChallenges = node[0].AsInt;
        int myID = PlayerPrefs.GetInt("playerid");

        for (int i = 1; i <= numChallenges; i++)
        {
            if (node[i]["id"].AsInt != myID) //don't show the player's own challenge
            {
                GameObject newDisplay = Instantiate<GameObject>(displayPrefab);
                ChallengeDisplay display = newDisplay.GetComponent<ChallengeDisplay>();
                display.SetDisplay(node[i]["name"], node[i]["wins"].AsInt, node[i]["losses"].AsInt, node[i]["draws"].AsInt);
                display.playerid = node[i]["id"].AsInt;
                newDisplay.transform.SetParent(contentPanel.transform);
                newDisplay.transform.localScale = new Vector3(1, 1, 1); //was getting set to 2.5 for some reason.  no idea why...
            }
        }
    }

    void clearDisplay()
    {
        ChallengeDisplay[] oldDisplays = contentPanel.GetComponentsInChildren<ChallengeDisplay>();
        foreach (ChallengeDisplay display in oldDisplays)
            Destroy(display.gameObject);
    }

    public void loadChallenges()
    {
        Debug.Log("loading from offset " + offset);
        WWWForm form = new WWWForm();
        form.AddField("offset", offset);
        offset += 10; //next time this is called, the game will load the next set

        WWW www = new WWW(url, form);
        StartCoroutine(CallPHP(www));
    }

    IEnumerator CallPHP(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
            if (www.text.Equals("ERROR: no data found"))
            {
                Debug.Log("no more entries! returning to front of list");
                offset = 0;
                loadChallenges(); //reload the first group, the display loops around
            }
            else
            {
                clearDisplay();
                displayChallenges(www.text);
            }
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
