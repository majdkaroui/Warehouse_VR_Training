using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProductPlacement : MonoBehaviour
{
    public scoremanager manager;
    public OrderManager ordermanager;
    public GameObject warningBox;
    public GameObject warningShelf;

    public GameObject warningInstance;
    // Start is called before the first frame update
    void Start()
    {
        manager.InitializeScore();

    }
    private void OnTriggerEnter(Collider collider)
    {
        Product product = collider.GetComponent<Product>();

        if (product != null)
        {
            if (product.CompareTag(tag) && (ordermanager.OrderedProducts.Contains(product)))
            {

                if (!product.hascollided)
                {
                    Debug.Log("good placement");
                    product.hascollided = true;
                    manager.IncrementScore();
                    Debug.Log("good placements : "+manager.correctplacements);
                }


            }
            else
            {
                manager.Decrement_Score();
                Debug.Log("wrong placements :"+manager.wrongplacements);
                


                if (ordermanager.OrderedProducts.Contains(product))
                {   
                    

                    Debug.Log("place in right box");
                    warningInstance = Instantiate(warningBox, product.transform.position, Quaternion.identity);
                    LookAtCamera();

                    warningInstance.SetActive(true);

                }

                else
                {
                    Debug.Log("return to warehouse");
                    warningInstance = Instantiate(warningShelf, product.transform.position, Quaternion.identity);
                    LookAtCamera();

                    warningInstance.SetActive(true);

                }

            }
        }



    }
    private void LookAtCamera()
    {
        if (warningInstance != null)
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 lookDirection = cameraTransform.position - warningInstance.transform.position;
            warningInstance.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
        private void OnTriggerExit(Collider collider)
    {
        Product product = collider.GetComponent<Product>();

        if (product != null)
        {
            if ((!product.CompareTag(tag))||(!ordermanager.OrderedProducts.Contains(product)))
            {
                manager.Decrement_Wrong_placements();
                Debug.Log("wrong placements :" + manager.wrongplacements);
                warningInstance.SetActive(false);



            }

        }
    }
}

