﻿using UnityEngine;
using System.Collections;

public class ShopNode : Node
{
    public float visibility = 25f;
    public float waitTime = 5f;

    protected override void CheckSprite() { }
    public override void CheckNeighbors() { }

    public override bool Wait(Frank frank, float time)
    {
        frank.waitTime += time;

        if (frank.waitTime >= waitTime)
        {
            frank.waitTime = 0;
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var frank = other.GetComponent<Frank>();

        if (frank)
        {
            var rng = Random.Range(0, 100);
            if (rng <= visibility)
            {
                if (neighbors.Count == 0)
                    neighbors.Add(frank.targetNode);
                frank.targetNode = this;
            }
        }
    }

}
