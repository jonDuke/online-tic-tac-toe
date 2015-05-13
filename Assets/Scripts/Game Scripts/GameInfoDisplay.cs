using UnityEngine;
using UnityEngine.UI;

public class GameInfoDisplay : MonoBehaviour {

    public Text nameDisplay, turnDisplay;

    public void SetDisplay(string name, bool turn)
    {
        nameDisplay.text = name;
        turnDisplay.text = "Turn: " + ((turn) ? "yours" : "theirs");
    }
}
