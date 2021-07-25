using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEditor;

public class GetPosition : MonoBehaviour
{
    
    public GameObject Chacater;
    Vector3 Position;


    [MenuItem("My Tools/Start To Report %F1")]
    static void PositionReceiver()
    {
        while(true)
        {
            Vector3 Position = GameObject.Find("figure6").transform.position;

            CSVManerger.AppendToReport
            (
                new string[4]
                {
                    "Join Weak",
                    Position.x.ToString(),
                    Position.y.ToString(),
                    Position.z.ToString()
                }
            );

            Thread.Sleep(100);
        }
    }
    
    [MenuItem("My Tools/Reset Report %F12")]
    static void DRV_ResetReport()
    {
        CSVManerger.CreateReport();
    }
}
        
