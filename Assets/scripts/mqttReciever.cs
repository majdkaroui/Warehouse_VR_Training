using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

public class mqttReceiver : M2MqttUnityClient
{
    public string messageA1;
    public string messageA2;
    public string messageA3;
    public string messageB1;
    public string messageB2;
    public string messageB3;
    public string messageC1;
    public string messageC2;
    public string messageC3;
    
    public bool autoTest = false;

    //using C# Property GET/SET and event listener to reduce Update overhead in the controlled objects
    private string m_msg;

    public string msg
    {
        get
        {
            return m_msg;
        }
        set
        {
            if (m_msg == value) return;
            m_msg = value;
            if (OnMessageArrived != null)
            {
                OnMessageArrived(m_msg);
            }
        }
    }

    public event OnMessageArrivedDelegate OnMessageArrived;
    public delegate void OnMessageArrivedDelegate(string newMsg);

    //using C# Property GET/SET and event listener to expose the connection status
    private bool m_isConnected;

    public bool isConnected
    {
        get
        {
            return m_isConnected;
        }
        set
        {
            if (m_isConnected == value) return;
            m_isConnected = value;
            if (OnConnectionSucceeded != null)
            {
                OnConnectionSucceeded(isConnected);
            }
        }
    }
    public event OnConnectionSucceededDelegate OnConnectionSucceeded;
    public delegate void OnConnectionSucceededDelegate(bool isConnected);

    // a list to store the messages
    private List<string> eventMessages = new List<string>();

    
    public void SetEncrypted(bool isEncrypted)
    {
        this.isEncrypted = isEncrypted;
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        isConnected = true;

        
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        Debug.Log("CONNECTION FAILED! " + errorMessage);
    }

    protected override void OnDisconnected()
    {
        Debug.Log("Disconnected.");
        isConnected = false;
    }

    protected override void OnConnectionLost()
    {
        Debug.Log("CONNECTION LOST!");
    }

    protected override void SubscribeTopics()
    {
        client.Subscribe(new string[] { "#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    protected override void UnsubscribeTopics()
    {
        client.Unsubscribe(new string[] { "#" });
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void DecodeMessage(string topic, byte[] message)
    {
        //The message is decoded
        msg = System.Text.Encoding.UTF8.GetString(message);

        Debug.Log("Received: " + msg);
        Debug.Log("from topic: " + m_msg);
        if (topic == "warehouse/A1")
            messageA1 = msg;
        else if (topic == "warehouse/A2")
            messageA2 = msg;
        else if (topic == "warehouse/A3")
            messageA3 = msg;
        else if (topic == "warehouse/B1")
            messageB1 = msg;
        else if (topic == "warehouse/B2")
            messageB2 = msg;
        else if (topic == "warehouse/B3")
            messageB3 = msg;
        else if (topic == "warehouse/C1")
            messageC1 = msg;
        else if (topic == "warehouse/C2")
            messageC2 = msg;
        else if (topic == "warehouse/C3")
            messageC3 = msg;

        StoreMessage(msg);
        
            if (autoTest)
            {
                autoTest = false;
                Disconnect();
            }
        
    }

    private void StoreMessage(string eventMsg)
    {
        if (eventMessages.Count > 50)
        {
            eventMessages.Clear();
        }
        eventMessages.Add(eventMsg);
    }

    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()

    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void OnValidate()
    {
        if (autoTest)
        {
            autoConnect = true;
        }
    }
}