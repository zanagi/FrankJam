using UnityEngine;
using System.Collections;

public class ShopNode : Node
{
    private Shop shop;

    protected override void CheckSprite() { }
    public override void CheckNeighbors() { }

    private void Start()
    {
        shop = GetComponent<Shop>();
    }

    public override bool Wait(Frank frank, float time)
    {
        frank.waitTime += time;
        shop.OnFrankContact(frank);

        if (frank.waitTime >= shop.TotalWaitTime)
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
            if (rng <= shop.TotalVisibility)
            {
                if (neighbors.Count == 0)
                    neighbors.Add(frank.targetNode);
                frank.targetNode = this;
            }
        }
    }

}
