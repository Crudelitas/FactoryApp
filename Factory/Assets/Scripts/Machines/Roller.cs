
using UnityEngine;

public class Roller : Machine {

    Item item = null;
    private bool collision = false;

    public override void Process(){
        print("Hello I am Working");
    }

    private void Update()
    {
        /*
        //Debug.Log("X: " + gridPosition.X + " , " + "Y: " + gridPosition.Y);
        if(item != null && collision)
        {
            MoveItem(item);
            collision = false;
            Debug.Log("Current Item Position: " + item.transform.position);
        }
        */
            

    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Im colliding!" + " with " + other.GetComponent<Item>().name);
        item = other.GetComponent<Item>();
        collision = true;
        Debug.Log("Item Position onTriggerEnter: " + item.transform.position);
    }
    */
}
