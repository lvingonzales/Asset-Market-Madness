using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultController : MonoBehaviour
{
    public string[] mults;
    public TMP_Text multText;

    public string currentMult;
    int currentPosition = 0;

    private void Start()
    {
        currentMult = mults[0];
        multText.text = currentMult;
    }

    public void changeMult ()
    {
        currentPosition = currentPosition + 1;

        if (currentPosition >= mults.Length)
        {
            currentPosition = 0;
        }

        currentMult = mults[currentPosition];

        multText.text = currentMult;
    }
}
