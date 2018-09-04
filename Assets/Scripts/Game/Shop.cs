using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Node {

    public float visibility = 25f;
    public float waitTime = 5f;

    protected override void CheckSprite() { }
    public override void CheckNeighbors() { }

    public override bool Wait(Frank frank, float time)
    {
        frank.waitTime += time;

        if(frank.waitTime >= waitTime)
        {
            frank.waitTime = 0;
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var frank = other.GetComponent<Frank>();

        if(frank)
        {
            if (neighbors.Count == 0)
                neighbors.Add(frank.targetNode);
            frank.targetNode = this;
        }
    }
}
