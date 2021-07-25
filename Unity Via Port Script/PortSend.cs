
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using System.Text;
 
public class PortSend : MonoBehaviour
{
    public string SendPortName = "COM12";
    public int baudRate = 9600;
    private SerialPort ssp = null;
    private Thread dataReceiveThread;
    private byte[] datasBytes;
    private string OneString;
    private string OtherString;
    private CoordinatesTrans CoorTrans;
    private Vector3 ChPV;
    public GameObject Ch;

 
    void Start()
    {
        OpenPortControl();
    }
    void Awake()
    {
        CoorTrans = GameObject.FindObjectOfType<CoordinatesTrans>();        //初始化 CoordinatesTrans
        Ch =  GameObject.Find("figure6");

    }
    void Update()
    {
        //Debug.Log("GettingObPosition");
        ChPV = Ch.transform.position;
    }

    public void OpenPortControl()
    {
        ssp = new SerialPort(SendPortName, baudRate);
        if (!ssp.IsOpen)
        {
            try
            {
                ssp.Open();
                Debug.Log("<color=green>Send Port was opened suscceesfully</color>");
            }
            catch(Exception e)
            {
                Debug.Log("<color=red>SPort was failed to Opened: </color> " + e );
            }
            //Debug.Log("sspOpen:"+ssp.IsOpen);
            
        }
        dataReceiveThread = new Thread (SendData); 
        dataReceiveThread.Start();
    }
    public void ClosePortControl()
    {
        if (ssp.IsOpen)
        {
                         ssp.Close ();
                         ssp.Dispose (); 
        }
    }
    //void Loop()
    void SendData() //已更改傳送內容
    {
        while (true)
        {
            if (ssp.IsOpen)
            { 
                try
                {
                    //GetObjectPos();
                    Debug.Log(ChPV);
                    string word = ChPV[0].ToString()+(" ")+ChPV[1].ToString()+(" ")+ChPV[2].ToString()+(" \n");
                    ssp.Write (word);
                    //Debug.Log ("<color=orange>SendToPort:</color>"+word);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            Thread.Sleep(500);
        }
    }
    void OnApplicationQuit()
    {
        ClosePortControl();
    }
}
