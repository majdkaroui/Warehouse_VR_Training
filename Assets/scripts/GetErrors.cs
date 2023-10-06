using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GetErrors : MonoBehaviour
{
    public TMP_Text Number_Errors;
    // Start is called before the first frame update
    void Start()
    {
        Number_Errors.text = "Number of errors :" + scoremanager.numberoferrors.ToString();
    }

    // Update is called once per frame

}