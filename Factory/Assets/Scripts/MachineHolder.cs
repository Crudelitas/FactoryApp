using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineHolder : Singleton<MachineHolder> {

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
}
