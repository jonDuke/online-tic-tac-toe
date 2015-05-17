using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGame : MonoBehaviour {

    public Text infoText;

    public enum GameResult { inProgress, win, loss, draw };
    private GameResult gameResult = GameResult.inProgress;

    private string url = "http://noblehousegames.x10host.com/tictactoe/acknowledgegame.php";

    public void PressButton()
    {
        if (gameResult != GameResult.inProgress)
            CloseGame();
        else
            Application.LoadLevel("Game List");
    }
    
    public void GameOver(GameResult result)
    {
        gameResult = result;
        GetComponentInChildren<Text>().text = "Close Game";

        if (result == GameResult.win)
            infoText.text = "You won!";
        else if (result == GameResult.draw)
            infoText.text = "It's a draw";
        else if (result == GameResult.loss)
            infoText.text = "You lost";
    }

    public void CloseGame()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", PlayerPrefs.GetInt("playerid"));
        form.AddField("game", StaticMemory.currentGame);

        WWW www = new WWW(url, form);
        StartCoroutine(callEndGame(www));
    }

    IEnumerator callEndGame(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null || !www.text.Contains("ERROR"))
        {
            Debug.Log(www.text);

            //go back to the last screen
            Application.LoadLevel("Game List");
        }
        else
            Debug.Log("WWW Error: " + www.error);
    }
}
