using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : Machine {

    Item item = null;

    private void Update()
    {
        /*
        if (item != null)
            MoveItem(item);
        */
    }

    public override void Process()
    {
        StartCoroutine(SpawnObject(100.0f, "Diamond"));
    }

    private IEnumerator SpawnObject(float wait, string type)
    {
        while(UIManager.Instance.MachinesRunning)
        {
            item = LevelManager.Instance.Pool.GetObject(type).GetComponent<Item>();
            //item.GridPosition = this.gridPosition;
            item.Spawn(this.transform.position);
            //item.MoveDirection = this.transform.eulerAngles.z;
            //Debug.Log("Item pos" + item.GridPosition.X + " , " + item.GridPosition.Y + "MoveDirection = " + item.MoveDirection);


            yield return new WaitForSeconds(wait);
        }
    }

}
