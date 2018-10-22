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

    private Dictionary<string, uint> machinePrices = new Dictionary<string, uint>();
    public Dictionary<string, uint> MachinePrices { get { return machinePrices; } }




    // Use this for initialization
    void Start () {
        Currency = 500;

        MachinePrices.Add("Roller", 50);
        MachinePrices.Add("Crafter", 100);
        MachinePrices.Add("Furnance", 100);
        MachinePrices.Add("HydraulicPress", 100);
        MachinePrices.Add("Starter", 100);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public uint GetPrice(GameObject machine)
    {
        switch(machine.name)
        {
            case "Roller":
                return machinePrices["Roller"];
            case "Roller(Clone)":
                return machinePrices["Roller"];
            case "Furnance":
                return machinePrices["Furnance"];
            case "Furnance(Clone)":
                return machinePrices["Furnance"];
            case "Crafter":
                return machinePrices["Crafter"];
            case "Crafter(Clone)":
                return machinePrices["Crafter"];
            case "HydraulicPress":
                return machinePrices["HydraulicPress"];
            case "HydraulicPress(Clone)":
                return machinePrices["HydraulicPress"];
            case "Starter":
                return machinePrices["Starter"];
            case "Starter(Clone)":
                return machinePrices["Starter"];
            default: 
                return 0;
        }
    }

    public uint GetPrice(Machine machine)
    {
        switch (machine.name)
        {
            case "Roller":
                return machinePrices["Roller"];
            case "Roller(Clone)":
                return machinePrices["Roller"];
            case "Furnance":
                return machinePrices["Furnance"];
            case "Furnance(Clone)":
                return machinePrices["Furnance"];
            case "Crafter":
                return machinePrices["Crafter"];
            case "Crafter(Clone)":
                return machinePrices["Crafter"];
            case "HydraulicPress":
                return machinePrices["HydraulicPress"];
            case "HydraulicPress(Clone)":
                return machinePrices["HydraulicPress"];
            case "Starter":
                return machinePrices["Starter"];
            case "Starter(Clone)":
                return machinePrices["Starter"];
            default:
                return 0;
        }
    }
}
