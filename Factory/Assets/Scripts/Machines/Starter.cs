using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : Machine {

    public override void Process()
    {
        print("Hello I am Working");
        StartCoroutine(SpawnObject(1.0f, "Diamond"));

    }

    private IEnumerator SpawnObject(float wait, string type)
    {
        while(UIManager.Instance.MachinesRunning)
        {
            GameObject item = LevelManager.Instance.Pool.GetObject(type);
            item.transform.position = this.transform.position;

            yield return new WaitForSeconds(wait);
        }
    }
}
