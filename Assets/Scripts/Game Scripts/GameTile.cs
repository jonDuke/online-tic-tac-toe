using UnityEngine;
using UnityEngine.UI;

public class GameTile : MonoBehaviour {

    public int tileNumber;
    public Sprite xPic, oPic;

    public bool set = false; //determines if the space has already been played, used for clearing current move

    public void PressTile()
    {
        //if it's not your turn or if the space is already used, do nothing
        if (!StaticMemory.yourTurn || set)
            return;

        Image graphic = GetComponent<Image>();

        if (StaticMemory.playerType == 1)
            graphic.sprite = xPic;
        else
            graphic.sprite = oPic;

        Color newColor = graphic.color;
        newColor.a = 1;
        graphic.color = newColor;

        GetComponentInParent<GameGrid>().PressTile(tileNumber);
    }

    public void ClearTile()
    {
        Image graphic = GetComponent<Image>();
        Color newColor = graphic.color;
        newColor.a = 0;
        graphic.color = newColor;
    }

    public void SetX()
    {
        set = true;
        Image graphic = GetComponent<Image>();
        
        graphic.sprite = xPic;

        Color newColor = graphic.color;
        newColor.a = 1;
        graphic.color = newColor;
    }

    public void SetO()
    {
        set = true;
        Image graphic = GetComponent<Image>();

        graphic.sprite = oPic;

        Color newColor = graphic.color;
        newColor.a = 1;
        graphic.color = newColor;
    }
}
