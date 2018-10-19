using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField]
    private GameObject selectPanel;
    public GameObject SelectPanel
    { get { return selectPanel; } }

    [SerializeField]
    private GameObject movingToolsPanel;
    public GameObject MovingToolsPanel
    { get { return movingToolsPanel; } }

    [SerializeField]
    private GameObject currentAmountDisplay;
    public GameObject CurrentAmountDisplay
    { get { return currentAmountDisplay; } }

    [SerializeField]
    private GameObject crafterPopUpPanel;

    [SerializeField]
    private GameObject splitterPopUpPanel;

    [SerializeField]
    private GameObject filterPopUpPanel;

    [SerializeField]
    private Button submitBtn;

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

    public MachineBtn ClickedBtn { get; private set; }

    private uint currentAmount = 0;
    public uint CurrentAmount { get { return currentAmount; } set { currentAmount = value; } }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentAmount > CurrencyManager.Instance.Currency)
        {
            selectPanel.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }else{
            selectPanel.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }
    }

    public void PickMachine(MachineBtn machineBtn)
    {
        if (this.ClickedBtn == null && !clickedMoveBtn && !clickedDeleteBtn && !clickedRotateBtn)
        {
            movingToolsPanel.SetActive(false);
            LevelManager.Instance.ShowGrid();
            SelectPanel.SetActive(true);
            CurrentAmountDisplay.SetActive(true);
            CurrentAmountDisplay.GetComponent<Text>().color = Color.red;
            CurrentAmountDisplay.GetComponent<Text>().text = currentAmount + " $";
            this.ClickedBtn = machineBtn;
        }
    }

    public void PlaceMachines()
    {
        movingToolsPanel.SetActive(true);
        LevelManager.Instance.PlaceMachines(UIManager.Instance.ClickedBtn.Machine);
        CurrencyManager.Instance.Currency -= CurrentAmount;
        CurrentAmount = 0;
        SelectPanel.SetActive(false);
        CurrentAmountDisplay.SetActive(false);
        this.ClickedBtn = null;
    }

    public void CancelSelection()
    {
        movingToolsPanel.SetActive(true);
        LevelManager.Instance.CancelSelection();
        CurrentAmountDisplay.SetActive(false);
        CurrentAmount = 0;
        SelectPanel.SetActive(false);
        this.ClickedBtn = null;
    }

    public void DeleteMachines()
    {
        if (!clickedMoveBtn && !clickedRotateBtn && ClickedBtn == null)
        {
            clickedDeleteBtn = !clickedDeleteBtn;

            if (clickedDeleteBtn)
            {
                deleteBtn.GetComponentInChildren<Text>().text = "Cancel?";
                submitBtn.gameObject.SetActive(true);
                CurrentAmountDisplay.SetActive(true);
                CurrentAmountDisplay.GetComponent<Text>().color = Color.green;
                CurrentAmountDisplay.GetComponent<Text>().text = currentAmount + " $";
            }
            else
            {
                deleteBtn.GetComponentInChildren<Text>().text = "Delete";
                submitBtn.gameObject.SetActive(false);
                CurrentAmountDisplay.SetActive(false);
                currentAmount = 0;
                LevelManager.Instance.DeselectMachines();
                LevelManager.Instance.ResetSelection();
            }
        }
    }

    public void SubmitDeleteSelection()
    {
        clickedDeleteBtn = false;
        deleteBtn.GetComponentInChildren<Text>().text = "Delete";
        submitBtn.gameObject.SetActive(false);
        CurrentAmountDisplay.SetActive(false);
        LevelManager.Instance.DeleteMachines();
        LevelManager.Instance.ResetSelection();
        CurrencyManager.Instance.Currency += currentAmount;
        CurrentAmount = 0;
    }

    public void MoveMachine()
    {
        if (!clickedRotateBtn && !clickedDeleteBtn)
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

    public void RotateMachine()
    {
        if (!clickedMoveBtn && !clickedDeleteBtn)
        {
            clickedRotateBtn = !clickedRotateBtn;

            if (clickedRotateBtn)
            {
                rotateBtn.GetComponentInChildren<Text>().text = "OK?";
                LevelManager.Instance.ActivateRotationMode();
            }
            else
            {
                rotateBtn.GetComponentInChildren<Text>().text = "Rotate";
                LevelManager.Instance.DeactivateRotationMode();
            }
        }
    }

    public void SelectMachine(GameObject machine)
    {
        switch (machine.name)
        {
            case "Roller(Clone)":
                break;
        }
        crafterPopUpPanel.SetActive(true);
        Debug.Log(machine.name + " is selected!");
    }
}
