using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineBtn : MonoBehaviour {

    [SerializeField]
    private GameObject machine;

    [SerializeField]
    private uint price;

    [SerializeField]
    private Text priceTxt;

    public GameObject Machine
    {
        get
        {
            return machine;
        }
    }

    public uint Price
    {
        get
        {
            return price;
        }
    }

    private void Start()
    {
        priceTxt.text = CurrencyManager.Instance.GetPrice(machine) + " <color=lime>$</color>";
    }
}
