using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShopBuff
{
    public string name;
    public float time, visibilityBuff, waitTimeBuff;
    public int cost;

    public ShopBuff(string name, float time, float visibilityBuff, float waitTimeBuff, int cost)
    {
        this.name = name;
        this.time = time;
        this.visibilityBuff = visibilityBuff;
        this.waitTimeBuff = waitTimeBuff;
        this.cost = cost;
    }

    public virtual ShopBuff Clone()
    {
        return new ShopBuff(name, time, visibilityBuff, waitTimeBuff, cost);
    }

    public bool UpdateTime(float timeDelta)
    {
        time -= timeDelta;
        return time <= 0;
    }

    public virtual void OnFrankContact(Frank frank, Shop shop)
    {
        // Do nothing for normal buffs
    }

    public override string ToString()
    {
        return name.ToUpper();
        // return string.Format("{0} ({1})", name, cost);
    }

    public virtual string EndText(Shop shop)
    {
        return string.Format("{0} ended at {1}.", name, shop.shopName);
    }

    public virtual string StartText(Shop shop)
    {
        return string.Format("Started {0} at {1}", name, shop.shopName);
    }
}
