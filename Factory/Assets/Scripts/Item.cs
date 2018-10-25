using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    Machine machine = null;
    int dir = 0;
    

    private void Update()
    {

        Vector3 destination = new Vector3(-1,-1,-1);


        if (machine != null)
        {
            destination = MoveItem(machine);

            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, 0.5f * Time.deltaTime);
        }

        if (destination == transform.position)
        {
            machine = null;
        }
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
    }

    public void Release()
    {
        LevelManager.Instance.Pool.ReleaseObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        if(other.tag == "Roller")
        {
            Debug.Log("I am colliding!");

            machine = other.GetComponent<Machine>();


        }
        else if(other.tag == "Starter")
        {
            Debug.Log("I am colliding!");

            machine = other.GetComponent<Machine>();

        }
        */

        if(other.GetComponent<Machine>() != null)
        {
            if (other == other.GetComponent<Machine>().ItemCircleCollider)
            {
                Debug.Log("I am colliding!");
                machine = other.GetComponent<Machine>();
            }
        }


    }

    public Vector3 MoveItem(Machine machine)
    {
        //yield return new WaitForSeconds(0.25f);
        int dir = (int)machine.transform.eulerAngles.z;
        Vector3 dest = new Vector3(-1,-1,-1);
        switch (dir)
        {
            case 0:
                dest = LevelManager.Instance.Tiles[new Point(machine.gridPosition.X, machine.gridPosition.Y - 1)].transform.position;
                break;
            case 270:
                dest = LevelManager.Instance.Tiles[new Point(machine.gridPosition.X + 1, machine.gridPosition.Y)].transform.position;
                break;
            case 180:
                dest = LevelManager.Instance.Tiles[new Point(machine.gridPosition.X, machine.gridPosition.Y + 1)].transform.position;
                break;
            case 90:
                dest = LevelManager.Instance.Tiles[new Point(machine.gridPosition.X - 1, machine.gridPosition.Y)].transform.position;
                break;
          
        }
        Debug.Log("MoveItem processing");
        return dest;
    }
}
