﻿using UnityEngine;
using UnityEngine.UI;

public class GameTile : MonoBehaviour {

    public int tileNumber;
    public Sprite xPic, oPic;

    public bool set = false; //determines if the space has already been played, used for clearing current move

    public void PressTile()
    {
        if (set)
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
}
