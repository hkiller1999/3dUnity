using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PointType
{
    Empty,
    White,
    Black
}

public class PointItem :MonoBehaviour
{
    PointType pointType;
    SpriteRenderer sr;
    public int X, Y;

    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public PointType PointType
    {
        get { return pointType; }
        set 
        { 
            pointType = value;
            if (pointType == PointType.White)
                sr.color = new Color(1, 1, 1, 1);
            if (pointType == PointType.Black)
                sr.color = new Color(0, 0, 0, 1);  
        }
    }

    public PointItem(int x,int y)
    {
        this.X = x;
        this.Y = y;
        this.pointType = PointType.Empty;
    }
}