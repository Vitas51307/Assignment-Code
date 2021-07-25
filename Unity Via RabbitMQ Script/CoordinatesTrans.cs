using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoordinatesTrans : MonoBehaviour
{
    public String[] Coordinate;
    public void Data(String data)
    {
        if(data!=null)
        {
            //Debug.Log("Data_Raw:"+data);
            Coordinate =  data.Split(' ');
            Debug.Log("Data_Split:"+Coordinate);
            //print(Coordinate[0]);
        }
    }
}
