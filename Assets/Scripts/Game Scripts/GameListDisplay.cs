using UnityEngine;
using UnityEngine.UI;
public class GameListDisplay : MonoBehaviour {

    public Text nameDisplay, turnDisplay;

    public void setDisplay(string name, bool turn)
    {
        nameDisplay.text = "Opponent: " + name;
        turnDisplay.text = (turn) ? "your turn" : "their turn";
    }
}
