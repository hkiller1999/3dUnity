using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GameManager : MonoBehaviour {
     
    PointItem[,] points;
 
    public GameObject go;
 
    GameObject gird;
 
    private PointType State = PointType.White;
 
    void Start () {
        points = new PointItem[16,16];
 
        gird = GameObject.FindGameObjectWithTag("Gird");
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Vector3 position = new Vector3(-7.5f + Mathf.Floor(j), 7.5f - Mathf.Floor(i), 0f);
                GameObject goo = Instantiate(go,position,Quaternion.identity);
                goo.transform.SetParent(gird.transform);
                PointItem p = goo.transform.GetComponent<PointItem>();
                p.X = j;
                p.Y = i;
                points[j,i] = p;
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("点击了");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            PointItem p;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.transform.gameObject.tag == "trigger")
                {
                    p = hit.transform.GetComponent<PointItem>();
                    Choose(p.X,p.Y);
                   // Debug.Log(new Vector2(p.X, p.Y));
                }
            }
        }
    }
 
 
    public void Choose(int x,int y)
    {
        if (points[x,y].PointType != PointType.Empty)
            return;
 
        points[x,y].PointType = State;
        Comfirm(x, y, State);
        if (State == PointType.White)
            State = PointType.Black;
        else
            State = PointType.White;
    }
 
    public void Comfirm(int x, int y, PointType p)
    {
 
        //横向匹配
        if (x - 1 > -1 && points[x - 1, y].PointType == p)
        {
            if (x - 2 > -1 && points[x - 2, y].PointType == p)
            {
                if (x - 3 > -1 && points[x - 3, y].PointType == p)
                {
                    if (x - 4 > -1 && points[x - 4, y].PointType == p)
                    {
                        Debug.Log(p + "赢了");
                    }
                    else if (x + 1 < 16)
                    {
                        if (points[x + 1, y].PointType == p)
                            Debug.Log(p + "赢了");
  
                    }
                }
                else if (x + 2 < 16)
                {
                    if (points[x + 1, y].PointType == p &&
                        points[x + 2, y].PointType == p)
                        Debug.Log(p + "赢了");
 
                }
            }
            else if (x + 3 < 16)
            {
                if (points[x + 1, y].PointType == p &&
                    points[x + 2, y].PointType == p &&
                    points[x + 3, y].PointType == p)
                    Debug.Log(p + "赢了");
 
            }
        }
        else if (x + 4 < 16)
        {
            if (points[x + 1, y].PointType == p &&
                points[x + 2, y].PointType == p &&
                points[x + 3, y].PointType == p &&
                points[x + 4, y].PointType == p)
                Debug.Log(p + "赢了");
 
        }
 
        //纵向匹配
        if (y - 1 > -1 && points[x, y - 1].PointType == p)
        {
            if (y - 2 > -1 && points[x, y - 2].PointType == p)
            {
                if (y - 3 > -1 && points[x, y - 3].PointType == p)
                {
                    if (y - 4 > -1 && points[x, y - 4].PointType == p)
                    {
                        Debug.Log(p + "赢了");
 
                    }
                    else if (y + 1 < 16)
                    {
                        if (points[x, y + 1].PointType == p)
                            Debug.Log(p + "赢了");
 
                    }
                }
                else if (y + 2 < 16)
                {
                    if (points[x, y + 1].PointType == p &&
                        points[x, y + 2].PointType == p)
                        Debug.Log(p + "赢了");
 
                }
            }
            else if (y + 3 < 16)
            {
                if (points[x, y + 1].PointType == p &&
                    points[x, y + 2].PointType == p &&
                    points[x, y + 3].PointType == p)
                    Debug.Log(p + "赢了");
 
            }
        }
        else if (y + 4 < 16)
        {
            if (points[x, y + 1].PointType == p &&
                points[x, y + 2].PointType == p &&
                points[x, y + 3].PointType == p &&
                points[x, y + 4].PointType == p)
                Debug.Log(p + "赢了");
 
        }
 
        //左上右下匹配
        if (y - 1 > -1 && x - 1 > -1 && points[x - 1, y - 1].PointType == p)
        {
            if (y - 2 > -1 && x - 2 > -1 && points[x - 2, y - 2].PointType == p)
            {
                if (y - 3 > -1 && x - 3 > -1 && points[x - 3, y - 3].PointType == p)
                {
                    if (y - 4 > -1 && x - 4 > -1 && points[x - 4, y - 4].PointType == p)
                    {
                        Debug.Log(p + "赢了");
 
                    }
                    else if (y + 1 < 16 && x + 1 < 16)
                    {
                        if (points[x + 1, y + 1].PointType == p)
                            Debug.Log(p + "赢了");
 
                    }
                }
                else if (y + 2 < 16 && x + 2 < 16)
                {
                    if (points[x + 1, y + 1].PointType == p &&
                        points[x + 2, y + 2].PointType == p)
                        Debug.Log(p + "赢了");
 
                }
            }
            else if (y + 3 < 16 && x + 3 < 16)
            {
                if (points[x + 1, y + 1].PointType == p &&
                    points[x + 2, y + 2].PointType == p &&
                    points[x + 3, y + 3].PointType == p)
                    Debug.Log(p + "赢了");
 
            }
        }
        else if (y + 4 < 16 && x + 4 < 16)
        {
            if (points[x + 1, y + 1].PointType == p &&
                points[x + 2, y + 2].PointType == p &&
                points[x + 3, y + 3].PointType == p &&
                points[x + 4, y + 4].PointType == p)
                Debug.Log(p + "赢了");
 
        }
 
        //右上左下匹配
        if (y - 1 > -1 && x + 1 < 16 && points[x + 1, y - 1].PointType == p)
        {
            if (y - 2 > -1 && x + 2 < 16 && points[x + 2, y - 2].PointType == p)
            {
                if (y - 3 > -1 && x + 3 < 16 && points[x + 3, y - 3].PointType == p)
                {
                    if (y - 4 > -1 && x + 4 < 16 && points[x + 4, y - 4].PointType == p)
                    {
                        Debug.Log(p + "赢了");
 
                    }
                    else if (y + 1 < 16 && x - 1 > -1)
                    {
                        if (points[x - 1, y + 1].PointType == p)
                            Debug.Log(p + "赢了");
 
                    }
                }
                else if (y + 2 < 16 && x - 2 > -1)
                {
                    if (points[x - 1, y + 1].PointType == p &&
                        points[x - 2, y + 2].PointType == p)
                        Debug.Log(p + "赢了");
 
                }
            }
            else if (y + 3 < 16 && x - 3 > -1)
            {
                if (points[x - 1, y + 1].PointType == p &&
                    points[x - 2, y + 2].PointType == p &&
                    points[x - 3, y + 3].PointType == p)
                    Debug.Log(p + "赢了");
 
            }
        }
        else if (y + 4 < 16 && x - 4 > -1)
        {
            if (points[x - 1, y + 1].PointType == p &&
                points[x - 2, y + 2].PointType == p &&
                points[x - 3, y + 3].PointType == p &&
                points[x - 4, y + 4].PointType == p)
                Debug.Log(p + "赢了");
 
        }
    }
 
}