using UnityEngine;
using System.Threading;
using System;
using System.Text;
using RabbitMQ.Client;
 
public class RabbitPosSend : MonoBehaviour
{
    private Thread dataSendThread;
    private CoordinatesTrans CoorTrans;
    private Vector3 ChPV;
    public GameObject Ch;
    Thread SendPosition;
    public int PositionSendRate=1000;
    public string routingKey="Position";
    public string queue="Position";
    public string exchange="amq.direct";
    void Start()
    {        
        dataSendThread = new Thread (Send); 
        dataSendThread.Start();
    }
    void Awake()
    {
        Ch =  GameObject.Find("figure6");
    }
    void Update()
    {
        //Debug.Log("GettingObPosition");
        ChPV = Ch.transform.position;
    }
    public void Send()
    {
        //Debug.Log(ChPV);


        var factory = new ConnectionFactory() { HostName = "localhost" };

        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue,durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            //string message = "Hello from Unity!: 1.34 2.56 3.78";
            while(true)
            {        
                string word = ChPV[0].ToString()+(" ")
                                +ChPV[1].ToString()+(" ")
                                    +ChPV[2].ToString();
                var body = Encoding.UTF8.GetBytes(word);
                try
                {
                    channel.BasicPublish(exchange,routingKey, basicProperties: null,body: body);
                    Debug.Log(" [V] Sent: "+word);

                }
                catch(Exception e)
                {
                    Debug.Log(e.Message);
                }
                Thread.Sleep(PositionSendRate);
            }

        }

        //Debug.Log(" Done ");
    }
    void OnApplicationQuit()
    {
        //if (thread != null && thread.IsAlive)
        //thread.Abort();
        //SendPosition= new Thread (Send);
        dataSendThread.Abort();
        Debug.Log ("Stop Sending Position.");
    }

}
