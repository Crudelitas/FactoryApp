using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileScript : MonoBehaviour 
{
    public Point GridPosition{ get; private set; }

    private Machine machine;

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        this.GridPosition = gridPos;
        transform.SetParent(parent);
        transform.position = worldPos;

        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            if(UIManager.Instance.ClickedBtn != null)
            {
                MakeSelection("Select");
                UIManager.Instance.CurrentAmountDisplay.GetComponent<Text>().text = "-" + UIManager.Instance.CurrentAmount + " $";

            }
            else if (UIManager.Instance.ClickedDeleteBtn)
            {
                /*Delete Currency Add problem maybe because of this if clause?*/
                MakeSelection("Delete");
                UIManager.Instance.CurrentAmountDisplay.GetComponent<Text>().text = "+" + UIManager.Instance.CurrentAmount + " $";
            }
            else if(UIManager.Instance.ClickedMoveBtn)
            {
                MakeSelection("MoveSelect");
            }
            else if(UIManager.Instance.ClickedRotateBtn)
            {
                RaycastHit2D rayHit = GetRayHit();
                if (rayHit.collider.GetComponent<Machine>())
                {
                    rayHit.collider.GetComponent<Machine>().Rotate();
                }
            }
            else
            {
                SelectMachine();
            }
        }
    }

    private void MakeSelection(string mode)
    {
        RaycastHit2D rayHit = GetRayHit();

        switch (mode)
        {
            case "Select":
                if (rayHit.collider.GetComponent<Machine>() == null)
                {
                    if (transform.GetComponentInChildren<SpriteRenderer>().sprite.name != "Test_Spritesheet_7")
                    {
                        transform.GetComponent<SpriteRenderer>().sprite = LevelManager.Instance.TilePrefabs[3].GetComponent<SpriteRenderer>().sprite;
                        UIManager.Instance.CurrentAmount += CurrencyManager.Instance.GetPrice(UIManager.Instance.ClickedBtn.Machine); 
                    }
                    else
                    {
                        UIManager.Instance.CurrentAmount -= CurrencyManager.Instance.GetPrice(UIManager.Instance.ClickedBtn.Machine);
                        transform.GetComponent<SpriteRenderer>().sprite = LevelManager.Instance.TilePrefabs[1].GetComponent<SpriteRenderer>().sprite;
                    }
                }
                break;

            case "Delete":
                if (rayHit.collider.GetComponent<Machine>() != null && transform.GetComponentInChildren<SpriteRenderer>().sprite.name != "Test_Spritesheet_6")
                {
                    UIManager.Instance.CurrentAmount += CurrencyManager.Instance.GetPrice(rayHit.collider.GetComponent<Machine>());
                    LevelManager.Instance.SelectMachinesToDelete(rayHit.collider.GetComponent<Machine>());
                }
                else
                {
                    UIManager.Instance.CurrentAmount -= CurrencyManager.Instance.GetPrice(rayHit.collider.GetComponent<Machine>());
                    LevelManager.Instance.MoveDeselectMachine();
                }
                break;

            case "MoveSelect":
                if (rayHit.collider.GetComponent<Machine>() != null && LevelManager.Instance.SelectedMachine == null)
                {
                    LevelManager.Instance.MoveSelectMachine(rayHit.collider.GetComponent<Machine>());
                }
                else if (rayHit.collider.GetComponent<Machine>() != null && LevelManager.Instance.SelectedMachine != null)
                {
                    LevelManager.Instance.MoveDeselectMachine();
                }
                else if (rayHit.collider.GetComponent<Machine>() == null && LevelManager.Instance.SelectedMachine != null)
                {
                    LevelManager.Instance.SelectedMachine.transform.position = rayHit.transform.position;
                    LevelManager.Instance.MoveDeselectMachine();
                }
                break;
        }
    }

    public void SelectMachine()
    {
        RaycastHit2D rayHit = GetRayHit();
        if (rayHit.collider.GetComponent<Machine>() != null)
        {
            UIManager.Instance.SelectMachine(rayHit.collider.gameObject);
        }
        else { Debug.Log("Nothing to select!"); }
    }

    public RaycastHit2D GetRayHit()
    {
        Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);
        return rayHit;
    }
}
