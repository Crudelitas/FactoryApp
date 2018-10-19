using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : Machine {

    public override void Process()
    {
        print("Hello I am Working");
    }

    private IEnumerator SpawnObject(float wait, string type)
    {
        LevelManager.Instance.Pool.GetObject("Aluminium");

        yield return new WaitForSeconds(wait);
    }
}
