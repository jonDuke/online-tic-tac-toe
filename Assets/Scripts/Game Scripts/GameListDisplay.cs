using UnityEngine;
using UnityEngine.UI;
public class GameListDisplay : MonoBehaviour {

    public Text nameDisplay, turnDisplay;
    public int gameID;

    public void setDisplay(string name, bool turn)
    {
        nameDisplay.text = "Opponent: " + name;
        turnDisplay.text = (turn) ? "your turn" : "their turn";
    }

    public void chooseGame()
    {
        StaticMemory.currentGame = gameID;
    }
}
