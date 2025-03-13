using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    const float priceRoof = 300f, priceFloor = 50f;
    public TMP_Text price, funds, stock;

    public GameObject[] assets; 

    GameObject multiplier;
    MultController multController;
    string currentMult;
    int multNumber;

    private int i_stock = 0;
    private float f_price = 100.0f, f_funds = 100000.0f;

    int trajectoryDie, changeDie;
    float changeValue;
    
    // Start is called before the first frame update
    void Start()
    {
        price.text = Formatter(f_price);
        funds.text = Formatter(f_funds);
        stock.text = i_stock.ToString();

        multiplier = GameObject.Find("Multiplier");
        multController = multiplier.GetComponent<MultController>();

        InvokeRepeating("UpdatePrices", 1f, 1f);
    }

    string Formatter (float number)
    {
        return "$" + System.Math.Round(number, 2).ToString("0.00");
    }

    void UpdatePrices ()
    {
        foreach (GameObject asset in assets)
        {
            AssetController controller = asset.GetComponent<AssetController>();
            controller.UpdatePrice();
        }
    }

    float lockedPrice;


    public void Purchase ()
    {
        currentMult = multController.currentMult;
        int workingMult;
        // Lock in the current price value, just in case it changes
        lockedPrice = f_price;

        // Check if the mult is set to max and calculate the amount purchasable
        if (!int.TryParse(currentMult, out multNumber))
        {
            // Multiplier is "MAX" 
            // Divide the price by the funds and floors the result
            // Use the result as the working mult and the stock
            workingMult = Mathf.FloorToInt(f_funds / lockedPrice);
        } else
        {
            workingMult = multNumber;
        }

        // Check if the funds are greater than the price modified by the mult
        if (f_funds < (lockedPrice * workingMult)) { return; }

        // Subtract the price from the funds
        f_funds = f_funds - (lockedPrice * workingMult);

        // Add a unit to the players stock
        i_stock = i_stock + workingMult;

        // Update the Ui
        funds.text = Formatter (f_funds);
        stock.text = i_stock.ToString();
    }

    public void Sell()
    {
        currentMult = multController.currentMult;
        int workingMult;

        if (!int.TryParse(currentMult,out multNumber))
        {
            // Sell all stock
            workingMult = i_stock;
        } else
        {
            workingMult = multNumber;
        }

        // Check if player has units to sell
        if (i_stock <= 0 || workingMult > i_stock) { return; }

        // Lock in the current price value, just in case it changes
        lockedPrice = f_price;

        // Add the price to the funds
        f_funds = f_funds + (lockedPrice * workingMult);

        // Remove one unit from the players stock
        i_stock = i_stock - workingMult;

        // Update the Ui
        funds.text = Formatter(f_funds);
        stock.text = i_stock.ToString();
    }
}
