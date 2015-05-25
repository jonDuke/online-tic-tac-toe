using UnityEngine;
using UnityEngine.UI;
public class GameListDisplay : MonoBehaviour {

    public Text nameDisplay, turnDisplay;
    public int gameID;

    public void setDisplay(string name, bool turn, int status)
    {
        nameDisplay.text = "Opponent: " + name;

        if (status == 0) //game in progress
        {
            turnDisplay.text = (turn) ? "your turn" : "their turn";

            if (turn)
                GetComponent<Image>().color = new Color(1f, 1, .3f);
        }
        else
            turnDisplay.text = "game over";
    }

    public void chooseGame()
    {
        StaticMemory.currentGame = gameID;
    }
}
