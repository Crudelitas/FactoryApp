using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Machine : MonoBehaviour {

    [SerializeField]
    uint price;
    [SerializeField]
    bool active;
    [SerializeField]
    float productionSpeed;
    [SerializeField]
    uint energyCost;

    GameObject machine;

    public void Rotate()
    {
        //transform.Rotate(Vector3.forward * -90);
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
    }
    //private void Move();
    //private void Sell();
    public abstract void Process();

}
