using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    Text counterText;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (counterText.text != SKRIP.coinCounter.ToString())
        {
            counterText.text = SKRIP.coinCounter.ToString();
        }
    }
}
