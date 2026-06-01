using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagDate
{
    public HandCard[] handCards {  get;private set; }
    public int GridNum { get; set; }
    public BagDate(int GridNum = 20)
    {
        handCards = new HandCard[GridNum];
        this.GridNum = GridNum;
    }
    public int SourchNullGrid()
    {
        for (int i = 0; i < handCards.Length; i++)
        {
            if(handCards[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
