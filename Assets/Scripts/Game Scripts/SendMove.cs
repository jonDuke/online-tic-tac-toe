using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SendMove : MonoBehaviour {

    public GameGrid grid;
    public Button confirmButton;

    private string url = "http://noblehousegames.x10host.com/tictactoe/sendmove.php";


    public void Update()
    {
        if(grid.currentMove == -1)
        {
            confirmButton.image.color = new Color(.75f, .75f, .75f, 1);
            confirmButton.interactable = false;
        }
        else
        {
            confirmButton.image.color = new Color(1, 1, 1, 1);
            confirmButton.interactable = true;
        }
    }

    public void sendMove()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("playerid"));
        form.AddField("game", StaticMemory.currentGame);
        form.AddField("movetype", StaticMemory.playerType);
        form.AddField("space", grid.currentMove);

        WWW www = new WWW(url, form);
        StartCoroutine(callSendMove(www));
    }

    IEnumerator callSendMove(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null || !www.text.Contains("ERROR"))
        {
            Debug.Log(www.text);
            StaticMemory.yourTurn = false;
            grid.confirmMove(www.text);
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
