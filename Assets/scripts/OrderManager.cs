using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OrderManager : MonoBehaviour
{
    public mqttReceiver mqttReceiverComponent;

    public int RedQuantity;
    public int WhiteQuantity;
    public int BlueQuantity;
    private String A1;
    private String A2;
    private String A3;
    private String B1;
    private String B2;
    private String B3;
    private String C1;
    private String C2;
    private String C3;
    private bool commandready = false;
    private bool executionDone = false;

    public List<Transform> Transforms = new List<Transform>();

        

    public List<Product> WhiteProducts = new List<Product>();
    public List<Product> RedProducts = new List<Product>();
    public List<Product> BlueProducts = new List<Product>();

    public List<Product> OrderedProducts;
    public TimerScript timer;
    public scoremanager scoremanager;
    public GameObject score_text;
    private void Start()
    {   
        RedProducts.Sort((a,b)=>a.DaysRemaining.CompareTo(b.DaysRemaining));
        BlueProducts.Sort((a, b) => a.DaysRemaining.CompareTo(b.DaysRemaining));
        WhiteProducts.Sort((a, b) => a.DaysRemaining.CompareTo(b.DaysRemaining)); 
            FillCommandList(OrderedProducts, WhiteProducts, WhiteQuantity);
            FillCommandList(OrderedProducts, RedProducts, RedQuantity);
            FillCommandList(OrderedProducts, BlueProducts, BlueQuantity);
            commandready = true;
        }

    private void Execute() {
        A1 = mqttReceiverComponent.messageA1;
        A2 = mqttReceiverComponent.messageA2;
        A3 = mqttReceiverComponent.messageA3;
        B1 = mqttReceiverComponent.messageB1;
        B2 = mqttReceiverComponent.messageB2;
        B3 = mqttReceiverComponent.messageB3;
        C1 = mqttReceiverComponent.messageC1;
        C2 = mqttReceiverComponent.messageC2;
        C3 = mqttReceiverComponent.messageC3;
        List<String> positions = new List<string> { A1, A2, A3, B1, B2, B3, C1, C2, C3 };

        PlaceProducts(positions, Transforms, RedProducts, "red");
        PlaceProducts(positions, Transforms, BlueProducts, "blue");
        PlaceProducts(positions, Transforms, WhiteProducts, "white");
        timer.StartTimer();
        score_text.SetActive(true);
    }
    

    private void PlaceProducts(List<String> positions, List<Transform> Transforms, List<Product> products, String color)
    {   
        int j = 0;
        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i] == "empty")
            {
                products[j].gameObject.SetActive(false);
            }

              else if (positions[i] == color)
                {
                    products[j].gameObject.transform.position = Transforms[i].position;
                    products[j].gameObject.SetActive(true);
                    j++;
                }
           }
        }
    
    private void FillCommandList(List<Product> OrderedProducts, List<Product> Products, int quantity)
    {
        List<Product> productsCopy = new List<Product>(Products);

        for (int i = 0; i < quantity; i++)
        {
            Product product = productsCopy[GetOldestProduct(productsCopy)];
            
                productsCopy.RemoveAt(GetOldestProduct(productsCopy));
                OrderedProducts.Add(product);

                



        }

    }
    
    int GetOldestProduct(List<Product> products)
    {
        int oldestindex = 0;
        
            
            Product oldesproduct = products[0];

        for (int i = 1; i < products.Count; i++)
        {
            

                if (products[i].DaysRemaining < oldesproduct.DaysRemaining)
                {
                    oldestindex = i;

                    oldesproduct = products[i];
                
            }
        }
        
        return oldestindex;

    }
    private void Update()
    {
        if (!executionDone &&
            mqttReceiverComponent.messageA1 != "" &&
            mqttReceiverComponent.messageA2 != "" &&
            mqttReceiverComponent.messageA3 != "" &&
            mqttReceiverComponent.messageB1 != "" &&
            mqttReceiverComponent.messageB2 != "" &&
            mqttReceiverComponent.messageB3 != "" &&
            mqttReceiverComponent.messageC1 != "" &&
            mqttReceiverComponent.messageC2 != "" &&
            mqttReceiverComponent.messageC3 != "")
        {
            Execute();
            executionDone = true; 
        }


        if ((scoremanager.correctplacements == OrderedProducts.Count) &&
            (scoremanager.wrongplacements == 0) &&
            commandready)
        {
            Debug.Log("command satisfied");
            timer.StopTimer();
            SceneManager.LoadScene("ScoreMenu");
        }
    }
}


 
