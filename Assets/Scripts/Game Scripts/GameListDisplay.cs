using UnityEngine;
using UnityEngine.UI;
public class GameListDisplay : MonoBehaviour {

    public Text nameDisplay, turnDisplay;
    public int gameID;

    public void setDisplay(string name, bool turn)
    {
        nameDisplay.text = "Opponent: " + name;
        turnDisplay.text = (turn) ? "your turn" : "their turn";

        if (turn)
            GetComponent<Image>().color = new Color(1f, 1, .3f);
    }

    public void chooseGame()
    {
        StaticMemory.currentGame = gameID;
    }
}
