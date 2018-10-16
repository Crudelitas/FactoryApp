using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [SerializeField]
    private Button submitBtn;

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

    /// <summary>
    /// Build Machines
    /// </summary>

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
                machine.transform.SetParent(machines);
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

    /// <summary>
    /// Deletes the machines.
    /// </summary>

    public void DeleteMachines()
    {
        if (!clickedMoveBtn && !clickedRotateBtn)
        {
            clickedDeleteBtn = !clickedDeleteBtn;

            if (clickedDeleteBtn)
            {
                deleteBtn.GetComponentInChildren<Text>().text = "Cancel?";
                submitBtn.gameObject.SetActive(true);
            }
            else
            {
                deleteBtn.GetComponentInChildren<Text>().text = "Delete";
                submitBtn.gameObject.SetActive(false);
                MachineHolder.Instance.DeselectMachines();
            }
        }
    }

    public void DeleteMachineSelection(Machine machine)
    {
        selectedMachine = machine;
        machine.DeleteSelect();
    }

    public void SubmitDeleteSelection()
    {
        clickedDeleteBtn = false;
        deleteBtn.GetComponentInChildren<Text>().text = "Delete";
        submitBtn.gameObject.SetActive(false);
        MachineHolder.Instance.DeleteMachines();
    }

    /// <summary>
    /// Rotates the machine.
    /// </summary>

    public void RotateMachine()
    {
        if(!clickedMoveBtn && !clickedDeleteBtn)
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

    /// <summary>
    /// Moves the machine.
    /// </summary>

    public void MoveMachine()
    {
        if(!clickedRotateBtn && !clickedDeleteBtn)
        {
            clickedMoveBtn = !clickedMoveBtn;

            if (clickedMoveBtn)
            {
                moveBtn.GetComponentInChildren<Text>().text = "OK?";
                LevelManager.Instance.ShowGrid();
            }
            else
            {
                moveBtn.GetComponentInChildren<Text>().text = "Move";
                LevelManager.Instance.DisableGrid();
            }
        }
    }

    public void BuildSelectMachine(Machine machine)
    {
        selectedMachine = machine;
        machine.BuildSelect();
    }

    public void BuildDeselectMachine()
    {
        if (selectedMachine != null)
        {
            selectedMachine.BuildDeselect();
        }

        selectedMachine = null;
    }

    public RaycastHit2D GetRayHit()
    {
        Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);
        return rayHit;
    }
}
