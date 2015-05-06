using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateChallenge : MonoBehaviour {

    public Text buttonText;

    private string url = "http://noblehousegames.x10host.com/tictactoe/";
    private bool challengeActive = true;

    public void Start()
    {
        //check for an active challenge
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("playerid"));

        WWW www = new WWW(url + "checkchallenge.php", form);
        StartCoroutine(CheckChallenge(www));
    }

    public void PressButton()
    {
        if (challengeActive)
        {
            //delete challenge
        }
        else
            createChallenge();
    }

    IEnumerator CheckChallenge(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);

            if(www.text == "true") //challenge exists
            {
                challengeActive = true;
                buttonText.text = "Delete Challenge";
            }
            else //player has no active challenge
            {
                buttonText.text = "Create Challenge";
                challengeActive = false;
            }
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }

    void createChallenge()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("playerid"));

        WWW www = new WWW(url + "createchallenge.php", form);
        StartCoroutine(CallCreate(www));
    }

    IEnumerator CallCreate(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
            challengeActive = true;
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
