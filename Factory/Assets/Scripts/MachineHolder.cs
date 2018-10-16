using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineHolder : Singleton<MachineHolder> 
{
    public void ActivateRotationMode()
    {
        foreach (Transform child in transform)
        {
            if(child.tag == "machineTag")
            {
                child.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void DeactivateRotationMode()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "machineTag")
            {
                child.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void DeleteMachines()
    {
        foreach (Transform child in transform)
        {
            if (child.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.name == "Test_Spritesheet_6")
            {
                Debug.Log("Deleted Machine: " + child.gameObject.name);
                Destroy(child.gameObject);
            }
        }
    }

    public void DeselectMachines()
    {
        foreach (Transform child in transform)
        {
            if (child.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.name == "Test_Spritesheet_6")
            {
                child.GetComponent<Machine>().Deselect();
            }
        }
    }
}
