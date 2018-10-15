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

    [SerializeField]
    private Sprite machineSprite;

    public Sprite MachineSprite{ get { return machineSprite; } }

    [SerializeField]
    private Sprite selected;

    [SerializeField]
    private Sprite delete;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Rotate()
    {
        //transform.Rotate(Vector3.forward * -90);
        GetComponentInChildren<Transform>().eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
        //Debug.Log(GetComponentInChildren<Transform>().eulerAngles.z);
    }

    public void Select()
    {
        spriteRenderer.sprite = selected;
    }
    public void Deselect()
    {
        spriteRenderer.sprite = machineSprite;
    }

    //private void Move();
    //private void Sell();
    public abstract void Process();



}
