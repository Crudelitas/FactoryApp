using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Machine : MonoBehaviour {

    [SerializeField]
    private bool active;
    [SerializeField]
    private float productionSpeed;
    [SerializeField]
    private uint energyCost;

    public Point gridPosition
    {
        get; set;
    }

    private BoxCollider2D clickBoxCollider;

    private CircleCollider2D itemCircleCollider;

    GameObject machine;

    [SerializeField]
    private Sprite machineSprite;
    public Sprite MachineSprite{ get { return machineSprite; } }

    public BoxCollider2D ClickBoxCollider
    {
        get
        {
            return clickBoxCollider;
        }
    }

    public CircleCollider2D ItemCircleCollider
    {
        get
        {
            return itemCircleCollider;
        }
    }

    [SerializeField]
    private Sprite selected;

    [SerializeField]
    private Sprite delete;

    private SpriteRenderer spriteRenderer;

    public enum Direction
    {
        NORTH = 0,
        EAST = 270,
        SOUTH = 180,
        WEST = 90
    }


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        clickBoxCollider = this.GetComponent<BoxCollider2D>();
        itemCircleCollider = this.GetComponent<CircleCollider2D>();
        
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
        Debug.Log("New Orientation: " + transform.eulerAngles.z);
    }

    //private void Move();
    //private void Sell();
    public abstract void Process();

    public void MoveItem(Item item)
    {
        int dir = (int)this.transform.eulerAngles.z;
        switch (dir)
        {
            case 0:
                Vector3 destination = LevelManager.Instance.Tiles[new Point(gridPosition.X, gridPosition.Y - 1)].transform.position;
                //Debug.Log("Destination: " + LevelManager.Instance.Tiles[new Point(gridPosition.X, gridPosition.Y - 1)].transform.position);
                //item.transform.position = Vector3.MoveTowards(item.transform.position, destination, 0.01f);
                item.transform.position = destination;
                break;
            case 270:
                Vector3 destination1 = LevelManager.Instance.Tiles[new Point(gridPosition.X + 1, gridPosition.Y)].transform.position;
                item.transform.position = Vector3.MoveTowards(item.transform.position, destination1, 0.1f);
                break;
            case 180:
                Vector3 destination2 = LevelManager.Instance.Tiles[new Point(gridPosition.X, gridPosition.Y + 1)].transform.position;
                item.transform.position = Vector3.MoveTowards(item.transform.position, destination2, 0.1f);
                break;
            case 90:
                Vector3 destination3 = LevelManager.Instance.Tiles[new Point(gridPosition.X - 1, gridPosition.Y)].transform.position;
                item.transform.position = Vector3.MoveTowards(item.transform.position, destination3, 0.1f);
                break;
        }
    }
}
