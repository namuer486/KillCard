using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat//µ¥¸ö¹Ø¿¨
{
    public Beat(int id)
    {
        this.id = id;
    }
    public int id {  get; set; }
    public bool isOpen { get; set; } = false;
}
