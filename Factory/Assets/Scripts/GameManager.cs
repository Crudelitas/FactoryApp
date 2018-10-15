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

    [SerializeField]
    private Button moveBtn;
    private bool clickedMoveBtn = false;
    public bool ClickedMoveBtn { get { return clickedMoveBtn; } set { clickedMoveBtn = value; } }

    [SerializeField]
    private Button rotateBtn;
    private bool clickedRotateBtn = false;
    public bool ClickedRotateBtn { get { return clickedRotateBtn; } set { clickedRotateBtn = value; } }

    [SerializeField]
    private Button deleteBtn;
    private bool clickedDeleteBtn = false;
    public bool ClickedDeleteBtn { get { return clickedDeleteBtn; } set { clickedDeleteBtn = value; } }

    private Machine machineScript;
    public Machine Machine { get { return machineScript; } }

    private Machine selectedMachine;
    public Machine SelectedMachine { get { return selectedMachine; } }

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
                machineScript = machine.transform.GetComponent<Machine>();
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

    public void SelectMachine(Machine machine)
    {
        selectedMachine = machine;
        machine.Select();
    }

    public void DeselectMachine()
    {
        if(selectedMachine != null)
        {
            selectedMachine.Deselect();
        }

        selectedMachine = null;
    }

    public void DeleteMachine(Machine machine)
    {
        selectedMachine = machine;
        machine.DeleteSelect();
    }

    public void DeleteButton()
    {
        clickedDeleteBtn = !clickedDeleteBtn;
    }

    public void SubmitDeleteSelection()
    {
        //Can throw Error, Maybe use the same technique as with the RotateMachine() -> MachineHolder!

        object[] obj = Object.FindObjectsOfType(typeof(GameObject));

        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if (g.GetComponentInChildren<SpriteRenderer>().sprite.name == "Test_Spritesheet_6")
            {
                Destroy(g);
            }
        }
    }

    public void RotateMachine()
    {
        if(!clickedMoveBtn)
        {
            clickedRotateBtn = !clickedRotateBtn;

            if (clickedRotateBtn)
            {
                rotateBtn.GetComponentInChildren<Text>().text = "OK?";
                MachineHolder.Instance.ActivateRotationMode();
            }
            else
            {
                rotateBtn.GetComponentInChildren<Text>().text = "Rotate";
                MachineHolder.Instance.DeactivateRotationMode();
            }
        }
    }

    public void MoveMachine()
    {
        if(!clickedRotateBtn)
        {
            clickedMoveBtn = !clickedMoveBtn;

            if (clickedMoveBtn)
            {
                moveBtn.GetComponentInChildren<Text>().text = "OK?";
            }
            else
            {
                moveBtn.GetComponentInChildren<Text>().text = "Move";
            }
            Debug.Log("Button status: " + clickedMoveBtn);
        }
    }
}
