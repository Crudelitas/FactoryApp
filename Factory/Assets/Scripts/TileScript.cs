using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileScript : MonoBehaviour 
{
    public Point GridPosition{ get; private set; }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        this.GridPosition = gridPos;
        transform.SetParent(parent);
        transform.position = worldPos;

        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
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
        //Debug.Log(GridPosition.X + " , " + GridPosition.Y);
    }

    private void MakeSelection()
    {
        Instantiate(LevelManager.Instance.TilePrefabs[6], transform.position, Quaternion.identity);
    }
}
