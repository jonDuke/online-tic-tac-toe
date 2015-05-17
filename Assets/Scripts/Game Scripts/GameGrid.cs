using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GameGrid : MonoBehaviour {

    public int currentMove = -1;
    public EndGame endGameButton;

    private GameTile[] tiles;
    public bool gameOver = false;

	public void Start()
    {
        currentMove = -1;

        tiles = GetComponentsInChildren<GameTile>();
    }

    public void PressTile(int currentTile)
    {
        if (gameOver) //don't allow pressing new tiles if game is finished
            return;

        //clear other unset tiles
        foreach (GameTile tile in tiles)
            if (tile.tileNumber != currentTile && !tile.set)
                tile.ClearTile();

        currentMove = currentTile;
    }

    public void DisplayGame(string boardState)
    {
        JSONNode node = JSON.Parse(boardState);

        int[] board = new int[9];

        for (int i = 0; i < 9; i++)
        {
            if (node[i].AsInt == 1)
                tiles[i].SetX();
            else if (node[i].AsInt == 2)
                tiles[i].SetO();
            else
                tiles[i].ClearTile();

            board[i] = node[i].AsInt;
        }

        checkBoard(board);
    }

    public void confirmMove(string response)
    {
        int move = StaticMemory.lastMove;

        //clear unset tiles, in case user pressed another before the response was recieved
        foreach (GameTile tile in tiles)
            if (!tile.set)
                tile.ClearTile();

        //set new tile
        if (StaticMemory.playerType == 1)
            tiles[move].SetX();
        else
            tiles[move].SetO();

        currentMove = -1;

        if (response == "win")
            GameOver(EndGame.GameResult.win);
        else if (response == "draw")
            GameOver(EndGame.GameResult.draw);
    }

    private void GameOver(EndGame.GameResult result)
    {
        gameOver = true;
        endGameButton.GameOver(result);

        //clear unset tiles, in case player has hit one before this was called
        foreach (GameTile tile in tiles)
            if (!tile.set)
                tile.ClearTile();
    }

    private void checkBoard(int[] board)
    {
        EndGame.GameResult status = EndGame.GameResult.inProgress;

        //check for a draw (gets overridden if a player has won)
        bool boardFilled = true;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                boardFilled = false;
                break;
            }
        }
        if (boardFilled)
            status = EndGame.GameResult.draw;

        //check all 8 win conditions
        if (board[0] == board[1] && board[1] == board[2])
        {
            if (board[0] == StaticMemory.playerType)
                status = EndGame.GameResult.win;
            else if (board[0] != 0)
                status = EndGame.GameResult.loss;
        }
        if (board[3] == board[4] && board[4] == board[5])
        {
            if (board[3] == StaticMemory.playerType)
                status = EndGame.GameResult.win;
            else if (board[3] != 0)
                status = EndGame.GameResult.loss;
        }
        if (board[6] == board[7] && board[7] == board[8])
        {
            if (board[6] == StaticMemory.playerType)
                status = EndGame.GameResult.win;
            else if (board[6] != 0)
                status = EndGame.GameResult.loss;
        }
        if (board[0] == board[3] && board[3] == board[6])
        {
            if (board[0] == StaticMemory.playerType)
                status = EndGame.GameResult.win;
            else if (board[0] != 0)
                status = EndGame.GameResult.loss;
        }
        if (board[1] == board[4] && board[4] == board[7])
        {
            if (board[1] == StaticMemory.playerType)
                status = EndGame.GameResult.win;
            else if (board[1] != 0)
                status = EndGame.GameResult.loss;
        }
        if (board[2] == board[5] && board[5] == board[8])
        {
            if (board[2] == StaticMemory.playerType)
                status = EndGame.GameResult.win;
            else if (board[2] != 0)
                status = EndGame.GameResult.loss;
        }
        if (board[0] == board[4] && board[4] == board[8])
        {
            if (board[0] == StaticMemory.playerType)
                status = EndGame.GameResult.win;
            else if (board[0] != 0)
                status = EndGame.GameResult.loss;
        }
        if (board[2] == board[4] && board[4] == board[6])
        {
            if (board[2] == StaticMemory.playerType)
                status = EndGame.GameResult.win;
            else if (board[2] != 0)
                status = EndGame.GameResult.loss;
        }

        //if game is over, change displays and button function
        if (status != EndGame.GameResult.inProgress)
            GameOver(status);
    }
}
