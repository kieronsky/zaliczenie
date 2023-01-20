using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentCups;
    public Text cupsText;


    public void AddCups(int cupsToAdd)
    {
        currentCups += cupsToAdd;
        cupsText.text = "Cups: " + currentCups; 
    }
}
