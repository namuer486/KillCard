using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard
{
    public CardConfig config {  get; private set; }
    public float value {  get; private set; }//»ù´¡ÊýÖµ
    public IBuff Buff {  get; internal set; }
    //Buff
    public HandCard(IBuff buff,CardConfig config)
    {
        this.config = config;
        value = config.number;
        Buff = buff;
    }
}
