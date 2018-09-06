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

    public ShopBuff Clone()
    {
        return new ShopBuff(name, time, visibilityBuff, waitTimeBuff, cost);
    }

    public bool UpdateTime(float timeDelta)
    {
        time -= timeDelta;
        return time <= 0;
    }

    public virtual void OnFrankContact(Frank frank)
    {
        // Do nothing for normal buffs
    }

    public override string ToString()
    {
        return name.ToUpper();
        // return string.Format("{0} ({1})", name, cost);
    }
}
