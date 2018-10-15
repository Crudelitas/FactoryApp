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
        if (!EventSystem.current.IsPointerOverGameObject()      && 
            GameManager.Instance.ClickedBtn != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (transform.GetComponentInChildren<SpriteRenderer>().sprite.name != "Test_Spritesheet_7")
                {
                    MakeSelection();
                } 
                else 
                {
                    Destroy(transform.gameObject);
                }
            }
        }
        else if(!EventSystem.current.IsPointerOverGameObject() &&
                GameManager.Instance.ClickedDeleteBtn)
        {



            if(Input.GetMouseButtonDown(0))
            {
                Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);

                if (rayHit.collider.GetComponent<Machine>() != null)
                {
                    GameManager.Instance.DeleteMachine(rayHit.collider.GetComponent<Machine>());
                }
            }




        }
        else if(!EventSystem.current.IsPointerOverGameObject()  && 
                GameManager.Instance.ClickedBtn == null         && 
                Input.GetMouseButton(0)                         && 
                GameManager.Instance.ClickedMoveBtn)
        {
            Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);

            if (rayHit.collider.GetComponent<Machine>() != null && GameManager.Instance.SelectedMachine == null)
            {
                GameManager.Instance.SelectMachine(rayHit.collider.GetComponent<Machine>());
            }
            else if(rayHit.collider.GetComponent<Machine>() != null && GameManager.Instance.SelectedMachine != null)
            {
                GameManager.Instance.DeselectMachine();
            }
            else if(rayHit.collider.GetComponent<Machine>() == null && GameManager.Instance.SelectedMachine != null)
            {
                GameManager.Instance.SelectedMachine.transform.position = rayHit.transform.position;
                GameManager.Instance.DeselectMachine();
            }
        }
        else if(!EventSystem.current.IsPointerOverGameObject()  && 
                GameManager.Instance.ClickedBtn == null         && 
                Input.GetMouseButton(0)                         && 
                GameManager.Instance.ClickedRotateBtn)
        {
            Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);

            rayHit.collider.GetComponent<Machine>().Rotate();
        }
    }

    private void MakeSelection()
    {
        Instantiate(LevelManager.Instance.TilePrefabs[3], transform.position, Quaternion.identity);
    }

    private void MakeDeleteSelection()
    {
        Instantiate(LevelManager.Instance.TilePrefabs[2], transform.position, Quaternion.identity);
    }
}
