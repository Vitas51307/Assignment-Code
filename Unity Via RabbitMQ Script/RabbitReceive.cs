using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

public class RabbitReceive : MonoBehaviour
{
    Thread ReceiveMes;
    static public string message;
    public int TransitionRate=1000;
    public string routingKey="Control";
    public string queue="Control";
    public string exchange="amq.direct";
    private CoordinatesTrans CoorTrans;
    void Start()
    {
        CoorTrans = GameObject.FindObjectOfType<CoordinatesTrans>(); 
        ReceiveMes=new Thread(Recieve);
        ReceiveMes.Start();
    }
    void OnApplicationQuit()
    {
        ReceiveMes.Abort();
    }
    public void Recieve()
    {
        var factory = new ConnectionFactory(){HostName = "localhost"};
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue,durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                        arguments: null);
            channel.QueueBind(queue,exchange,routingKey); 
            Debug.Log("Receiving...");
            var consumer = new EventingBasicConsumer(channel);
            while(true)
            {
                consumer.Received += (model, ea) =>
                {
                    //Debug.Log("yes2");
                    var body = ea.Body;
                    message = Encoding.UTF8.GetString(body);
                };
                if(message!=null)
                {
                    Debug.Log(" [V] Received: "+message);
                    CoorTrans.Data(message);
                }
                channel.BasicConsume(queue,noAck: true,consumer);
                Thread.Sleep(TransitionRate);
            }
        }
    
    }
    
}
