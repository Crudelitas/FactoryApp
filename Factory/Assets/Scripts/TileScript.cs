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
        if (!EventSystem.current.IsPointerOverGameObject() &&
            Input.GetMouseButtonDown(0) &&
            GameManager.Instance.ClickedBtn != null)
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
        /// <remarks>
        /// Select machines to delete
        /// </remarks>
        else if (!EventSystem.current.IsPointerOverGameObject() &&
                 Input.GetMouseButtonDown(0) &&
                 GameManager.Instance.ClickedDeleteBtn)
        {
            RaycastHit2D rayHit = GetRayHit();

            if (rayHit.collider.GetComponent<Machine>() != null && 
                transform.GetComponentInChildren<SpriteRenderer>().sprite.name != "Test_Spritesheet_6")
            {
                GameManager.Instance.DeleteMachineSelection(rayHit.collider.GetComponent<Machine>());
            }
            else
            {
                GameManager.Instance.DeselectMachine();
            }
        }
        /// <remarks>
        /// The following Part is for seleceting and moving the machines
        /// </remarks>
        else if (!EventSystem.current.IsPointerOverGameObject()  && 
                GameManager.Instance.ClickedBtn == null         && 
                Input.GetMouseButtonDown(0)                         && 
                GameManager.Instance.ClickedMoveBtn)
        {
            RaycastHit2D rayHit = GetRayHit();

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
        /// <remarks>
        /// This part is for machine rotation
        /// </remarks>
        else if (!EventSystem.current.IsPointerOverGameObject()  && 
                GameManager.Instance.ClickedBtn == null         && 
                Input.GetMouseButtonDown(0)                         && 
                GameManager.Instance.ClickedRotateBtn)
        {
            RaycastHit2D rayHit = GetRayHit();

            if (rayHit.collider.GetComponent<Machine>())
            {
                rayHit.collider.GetComponent<Machine>().Rotate();
            }
        }
    }

    private void MakeSelection()
    {
        /* Make the Selection with the actual machine - like in delete Selection!! */
        Instantiate(LevelManager.Instance.TilePrefabs[3], transform.position, Quaternion.identity);
    }

    private RaycastHit2D GetRayHit()
    {
        Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(fingerPosition, Vector2.zero);
        return rayHit;
    }
}
