using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : Singleton<CurrencyManager> {

    [SerializeField]
    private Text currencyTxt;
    private uint currency;
    public uint Currency
    {
        get
        {
            return currency;
        }
        set
        {
            currency = value;
            this.currencyTxt.text = value.ToString() + " <color=lime>$</color>";
        }
    }



    // Use this for initialization
    void Start () {
        Currency = 500;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
