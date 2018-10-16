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

    public void BuildModeSelect()
    {
        spriteRenderer.sprite = selected;
    }

    public void BuildModeDeselect()
    {
        spriteRenderer.sprite = machineSprite;
    }

    public void DeleteSelect()
    {
        spriteRenderer.sprite = delete;
    }

    public void Select()
    {

    }

    public void Rotate()
    {
        GetComponentInChildren<Transform>().eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
    }

    //private void Move();
    //private void Sell();
    public abstract void Process();



}
