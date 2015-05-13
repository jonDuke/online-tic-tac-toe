using UnityEngine;

public class StaticMemory : MonoBehaviour {

	/*
     * container class for various static variables I may need to pass between scenes
     * */

    public static int currentGame; //gameid of the game selected, so we can load the correct one
    public static int playerType; //are you x (1) or o (0) for the current game?
}
