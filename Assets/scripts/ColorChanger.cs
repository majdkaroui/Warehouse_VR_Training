using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ColorChanger : MonoBehaviour
{   
    private  Renderer renderer;
    private Material originalmaterial;
    private bool isGrabbed = false;
    private bool ColorChange;
    public Material targetMaterial;
    private List<Material> newMaterialsList;

    public OrderManager productlist;
    public Product product;


    private void Start()

    {   renderer = GetComponent<Renderer>();
        originalmaterial = renderer.material;
        ColorChange = productlist.OrderedProducts.Contains(product);
        List<Material> newMaterialsList = new List<Material>(renderer.materials);
        if (ColorChange && !isGrabbed)
        {   
            newMaterialsList.Add(targetMaterial);
            renderer.materials = newMaterialsList.ToArray();

        }
    }

    

    
}