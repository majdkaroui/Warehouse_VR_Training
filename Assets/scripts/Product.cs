using UnityEngine;
using TMPro;
using System;

public class Product : MonoBehaviour
{
    public string color;
    public string id;
    public TMP_Text text_color;
    public TMP_Text text_id;    
    public GameObject ui;
    public int DaysRemaining;
   
    public Vector3 offsetui;
    public  bool hascollided=false;

    // Variable to track the grabbed product
    private static Product grabbedProduct;

    // Method to set the currently grabbed product
    public void SetGrabbedProduct()
    {
        grabbedProduct = this;
    }

    public void UpdateText()
    {
        text_color.text = "Product color: " + color;
        text_id.text = "Product ID: " + id;
    }

    void Update()
    {
        if (grabbedProduct == this)
        {
            // Update UI position based on the grabbed product's position
            ui.transform.position = this.transform.position + offsetui;
            ui.transform.LookAt(Camera.main.transform);
            

        }
    }
}