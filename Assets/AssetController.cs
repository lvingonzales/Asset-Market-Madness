using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssetController : MonoBehaviour
{
    public float priceFloor, priceCeiling, priceBaseline, priceCurrent;
    public Image status;
    public Sprite[] statuses;
    public TMP_Text priceText;
    float price;
    Sprite neutral, down, dump, bubble, rise;
     

    private void Start()
    {
        neutral = statuses[0];
        rise = statuses[1];
        bubble = statuses[2];
        down = statuses[3];
        dump = statuses[4];

        priceCurrent = UnityEngine.Random.Range(priceFloor, priceCeiling);

        status.GetComponent<Image>().sprite = bubble;
    }

    string Formatter(float number)
    {
        return "$" + System.Math.Round(number, 2).ToString("0.00");
    }

    public void UpdatePrice ()
    {
        int trajectoryDie, changeDie;
        float changeValue;
        // Roll a die to determine the price trajectory
        // 1d12 , 1 - 6 is down 7 - 12 is up
        trajectoryDie = UnityEngine.Random.Range(1, 13);

        // Roll the second die to determine how much it changes by
        // The value is a percentage of its current value lost or gained
        // 1d20 
        changeDie = UnityEngine.Random.Range(1, 21);
        changeValue = (changeDie / 100f) * price;

        if (trajectoryDie <= 6)
        {
            price = price - changeValue;

            if (price < priceFloor) { price = priceFloor; }
        }
        else
        {
            price = price + changeValue;
            if (price > priceCeiling) { price = priceCeiling; }
        }

        // Update the UI
        UpdateStatus(changeDie, changeValue);
        priceText.text = Formatter(price);
    }

    void UpdateStatus(int changeType, float changeValue)
    {
        if (changeType <= 6)
        {
            if (changeValue <= 0.15)
            {
                status.sprite = down;
            } else
            {
                status.sprite = dump;
            }
        } else
        {
            if(changeValue <= 0.15)
            {
                status.sprite = rise;
            } else
            {
                status.sprite = bubble;
            }
        }
    }
}
