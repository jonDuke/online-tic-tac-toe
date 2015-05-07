using UnityEngine;
using UnityEngine.UI;

public class ChallengeDisplay : MonoBehaviour {

    public Text nameDisplay, statDisplay;
    public int playerid;

    public void SetDisplay(string name, int wins, int losses, int draws)
    {
        nameDisplay.text = "Opponent: " + name;
        statDisplay.text = "Stats: " + wins + "/" + losses + "/" + draws;
    }
}
