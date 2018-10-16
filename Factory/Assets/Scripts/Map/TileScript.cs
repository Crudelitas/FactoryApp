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
            if(GameManager.Instance.ClickedBtn != null)
            {
                MakeSelection("Select");
            }
            else if (GameManager.Instance.ClickedDeleteBtn)
            {
                MakeSelection("Delete");
            }
            else if(GameManager.Instance.ClickedMoveBtn)
            {
                MakeSelection("MoveSelect");
            }
            else if(GameManager.Instance.ClickedRotateBtn)
            {
                RaycastHit2D rayHit = GameManager.Instance.GetRayHit();
                if (rayHit.collider.GetComponent<Machine>())
                {
                    rayHit.collider.GetComponent<Machine>().Rotate();
                }
            }
        }
    }

    private void MakeSelection(string mode)
    {
        /* Make the Selection with the actual machine - like in delete Selection!! */
        RaycastHit2D rayHit = GameManager.Instance.GetRayHit();

        switch (mode)
        {
            case "Select":
                if (transform.GetComponentInChildren<SpriteRenderer>().sprite.name != "Test_Spritesheet_7")
                {
                    //Instantiate(LevelManager.Instance.TilePrefabs[3], transform.position, Quaternion.identity);
                    transform.GetComponent<SpriteRenderer>().sprite = LevelManager.Instance.TilePrefabs[3].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    Destroy(transform.gameObject);
                }
                break;

            case "Delete":
                if (rayHit.collider.GetComponent<Machine>() != null && transform.GetComponentInChildren<SpriteRenderer>().sprite.name != "Test_Spritesheet_6")
                {
                    LevelManager.Instance.SelectMachinesToDelete(rayHit.collider.GetComponent<Machine>());
                }
                else
                {
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
}
