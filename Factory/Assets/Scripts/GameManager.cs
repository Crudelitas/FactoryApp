using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> 
{
    [SerializeField]
    private GameObject selectPanel;

    [SerializeField]
    private Transform machines;

    public MachineBtn ClickedBtn { get; private set;}


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickMachine(MachineBtn machineBtn)
    {
        if(this.ClickedBtn == null)
        {
            LevelManager.Instance.ShowGrid();
            selectPanel.SetActive(true);
            this.ClickedBtn = machineBtn;
        }
    }

    public void SubmitSelection()
    {
        object[] obj = Object.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if(g.name == "GreenIndicator(Clone)")
            {
                GameObject machine = Instantiate(ClickedBtn.Machine, g.transform.position, Quaternion.identity);
                machine.transform.SetParent(machines.transform);
                Destroy(g);
            }
        }
        selectPanel.SetActive(false);
        this.ClickedBtn = null;
        LevelManager.Instance.DisableGrid();
    }

    public void CancelSelection()
    {
        object[] obj = Object.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if (g.name == "GreenIndicator(Clone)")
            {
                Destroy(g);
            }
        }
        selectPanel.SetActive(false);
        this.ClickedBtn = null;
        LevelManager.Instance.DisableGrid();
    }

    public void RotateMachine()
    {
    }

    public void MoveMachine()
    {
    }
}
