using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoadChallenges : MonoBehaviour {

    public GameObject contentPanel;
    public GameObject displayPrefab;

    private string url = "http://noblehousegames.x10host.com/tictactoe/getchallenges.php";

	void Start () 
    {
        loadChallenges();
	}

    void displayChallenges(string jsondata)
    {
        var node = JSON.Parse(jsondata);

        int numChallenges = node[0].AsInt;

        for (int i = 1; i <= numChallenges; i++)
        {
            GameObject newDisplay = Instantiate<GameObject>(displayPrefab);
            newDisplay.GetComponent<ChallengeDisplay>().SetDisplay(node[i]["name"], node[i]["wins"].AsInt, node[i]["losses"].AsInt, node[i]["draws"].AsInt);
            newDisplay.transform.SetParent(contentPanel.transform);
            newDisplay.transform.localScale = new Vector3(1, 1, 1); //was getting set to 2.5 for some reason.  no idea why...
        }
    }

    public void loadChallenges(int offset = 0)
    {
        WWWForm form = new WWWForm();
        form.AddField("offest", offset);

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

            displayChallenges(www.text);
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
