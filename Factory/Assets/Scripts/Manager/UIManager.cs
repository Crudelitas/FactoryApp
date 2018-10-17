﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField]
    private GameObject selectPanel;

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



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PickMachine(MachineBtn machineBtn)
    {
        if (this.ClickedBtn == null)
        {
            LevelManager.Instance.ShowGrid();
            selectPanel.SetActive(true);
            this.ClickedBtn = machineBtn;
        }
    }

    public void PlaceMachines()
    {
        LevelManager.Instance.PlaceMachines(UIManager.Instance.ClickedBtn.Machine);
        selectPanel.SetActive(false);
        this.ClickedBtn = null;
    }

    public void CancelSelection()
    {
        LevelManager.Instance.CancelSelection();
        selectPanel.SetActive(false);
        this.ClickedBtn = null;
    }

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
                LevelManager.Instance.DeselectMachines();
            }
        }
    }

    public void SubmitDeleteSelection()
    {
        clickedDeleteBtn = false;
        deleteBtn.GetComponentInChildren<Text>().text = "Delete";
        submitBtn.gameObject.SetActive(false);
        LevelManager.Instance.DeleteMachines();
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
