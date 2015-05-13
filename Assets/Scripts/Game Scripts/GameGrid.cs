using UnityEngine;
using System.Collections;

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
}
