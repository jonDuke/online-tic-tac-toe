using UnityEngine;
using UnityEngine.UI;

public class GameInfoDisplay : MonoBehaviour {

    public Text nameDisplay, turnDisplay;

    public void SetDisplay(string name, int turn)
    {
        nameDisplay.text = name;
        turnDisplay.text = "Turn: " + turn;
    }
}
