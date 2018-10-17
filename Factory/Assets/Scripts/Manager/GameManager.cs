using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> 
{
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
        UIManager.Instance.PickMachineUI(machineBtn);
    }

    public void PlaceMachines()
    {
        LevelManager.Instance.PlaceMachines(UIManager.Instance.ClickedBtn.Machine);
        UIManager.Instance.PlaceMachinesUI();
    }

    public void CancelSelection()
    {
        LevelManager.Instance.CancelSelection();
        UIManager.Instance.CancelSelectionUI();
    }

    /// <summary>
    /// Deletes the machines.
    /// </summary>

    public void SubmitDeleteSelection()
    {
        UIManager.Instance.SubmitDeleteSelectionUI();
        LevelManager.Instance.DeleteMachines();
    }

    public void DeleteMachines()
    {
        UIManager.Instance.DeleteMachinesUI();
    }

    /// <summary>
    /// Rotates the machine.
    /// </summary>

    public void RotateMachine()
    {
        UIManager.Instance.RotateMachineUI();
    }

    /// <summary>
    /// Moves the machine user interface.
    /// </summary>

    public void MoveMachine()
    {
        UIManager.Instance.MoveMachineUI();
    }

    public void SelectMachine(GameObject machine)
    {
        UIManager.Instance.SelectMachineUI(machine);
    }

    public RaycastHit2D GetRayHit()
    {
        Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);
        return rayHit;
    }
}
