using UnityEngine;
using System.Collections;

public class CreateChallenge : MonoBehaviour {

    private string url = "http://noblehousegames.x10host.com/tictactoe/createchallenge.php";

    public void createChallenge()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("playerid"));

        WWW www = new WWW(url, form);
        StartCoroutine(CallPHP(www));
    }

    IEnumerator CallPHP(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
            Debug.Log(www.text);
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
