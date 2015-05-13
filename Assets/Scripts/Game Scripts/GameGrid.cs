using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GameGrid : MonoBehaviour {

    public int currentMove = -1;

    private GameTile[] tiles;

	public void Start()
    {
        currentMove = -1;

        tiles = GetComponentsInChildren<GameTile>();
    }

    public void PressTile(int currentTile)
    {
        //clear other unset tiles
        foreach (GameTile tile in tiles)
            if (tile.tileNumber != currentTile && !tile.set)
                tile.ClearTile();

        currentMove = currentTile;
    }

    public void DisplayGame(string boardState)
    {
        var node = JSON.Parse(boardState);

        for (int i = 0; i < 9; i++)
        {
            if (node[i].AsInt == 1)
                tiles[i].SetX();
            else if (node[i].AsInt == 2)
                tiles[i].SetO();
            else
                tiles[i].ClearTile();
        }
    }
}
