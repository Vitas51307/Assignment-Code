
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using System.Text;
 
public class PortReceiver : MonoBehaviour
{
    public string portName = "COM10";
    public int baudRate = 9600;
 
    private SerialPort sp = null;
    private Thread dataReceiveThread;
    private byte[] datasBytes;
    int i = 0;
    private string OneString;
    private string OtherString;
    private CoordinatesTrans player;
 
    public void Start()
    {
        OpenPortControl();
    }
    void Awake()
    {
        player = GameObject.FindObjectOfType<CoordinatesTrans>();        //初始化 CoordinatesTrans
    }
    public void OpenPortControl()
    {
        sp = new SerialPort(portName, baudRate);
        if (!sp.IsOpen)
        {
            try
            {
                sp.Open();
                Debug.Log("<color=green>Read Port was opened suscceesfully</color>");
                if (sp.IsOpen)
                {
                    Debug.Log("<color=green>Waiting for Data...</color>");
                }
            }
            catch(Exception e)
            {
                Debug.Log("<color=red>Port was failed to Opened: </color> " + e );
            }
            
        }
        dataReceiveThread = new Thread (ReceiveData); 
        dataReceiveThread.Start();
    }
    public void ClosePortControl()
    {
        if (sp != null && sp.IsOpen)
        {
                         sp.Close ();
                         sp.Dispose (); 
        }
    }
    public void ReceiveData()
    {
        int bytesToRead = 0;
        while (true)
        {
            if (sp != null && sp.IsOpen)
            { 
                try
                {
                    datasBytes = new byte[2048];
                    bytesToRead = sp.Read(datasBytes, 0, datasBytes.Length);
                    if (bytesToRead == 0)
                    {
                        continue;
                    }
                    else
                    {
                        string strbytes = Encoding.Default.GetString(datasBytes);
                        i++;
                        if (i == 1)
                        {
                            OneString = strbytes[0].ToString();
                        }
                        else if (i == 2)
                        {
                            OtherString = OneString + strbytes;
                            i = 0;
                            //Debug.Log("<color=orange>We got: </color>"+OtherString);
                            player.Data (OtherString);                 //將值傳給CoordinatesTrans的Data
		                    
                        }
                        //Debug.Log(strbytes);
                    }
 
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            Thread.Sleep(30);
        }
    }
    private void SentData() //未更改傳送內容
    {
        while (true)
        {
            if (sp != null && sp.IsOpen)
            { 
                try
                {
                    string word =" Hello from the other side! 200 299 300 \n ";
                    sp.Write (word);
                    Debug.Log ("<color=orange>SendToPort:</color>"+word);
 
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            Thread.Sleep(30);
        }
    }
    void OnApplicationQuit()
    {
        ClosePortControl();
    }
}
