using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Node {

    public float visibility = 25f;
    public float waitTime = 5f;
    private static Shop selectedShop;


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
            var rng = Random.Range(0, 100);
            if (rng <= visibility)
            {
                if (neighbors.Count == 0)
                    neighbors.Add(frank.targetNode);
                frank.targetNode = this;
            }
        }
    }

    public void OnSelect()
    {
        var previousSelected = selectedShop;

        if (previousSelected)
        {
            previousSelected.OnDeselect();

            if (previousSelected == this)
                return;
        }
        selectedShop = this;
        Debug.Log("Select: " + name);
    }

    public void OnDeselect()
    {
        if (selectedShop == this)
            selectedShop = null;
        Debug.Log("Deselect: " + name);
    }
}
