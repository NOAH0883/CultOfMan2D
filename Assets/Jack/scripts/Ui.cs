using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ui : MonoBehaviour
{

    //[SerializeField] GameObject sunrise;
    //[SerializeField] GameObject briefmessage;

    //if player presses E key  - SunriseUI


    [SerializeField] TextMeshProUGUI dayNightCycleText;
    [SerializeField] List<string> dayNightCycleTexts;
    int dayNightCycleIndex;
   
    
    void Start()
    {

        if (dayNightCycleIndex == dayNightCycleTexts.Count)
        {
            //dayNightCycleIndex = dayNightCycleTexts.Count;
            gameObject.SetActive(false);
        }
 
        else
        {
            dayNightCycleIndex++;

            dayNightCycleText.text = dayNightCycleTexts[dayNightCycleIndex];
        }


        // when esc is pressed
        //if the pop is not active
        //make popup active
        //else
        //make popup no active
         



    }
    
    
    void Update()
    {

    }





}
