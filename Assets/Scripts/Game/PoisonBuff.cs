using UnityEngine;
using System.Collections;

public class PoisonBuff : ShopBuff
{
    public PoisonBuff(string name, float time, float visibilityBuff, float waitTimeBuff, int cost)
        :base(name, time, visibilityBuff, waitTimeBuff, cost)
    {

    }

    public override ShopBuff Clone()
    {
        return new PoisonBuff(name, time, visibilityBuff, waitTimeBuff, cost);
    }
    
    public override void OnFrankContact(Frank frank, Shop shop)
    {
        GameManager.Instance.notificationManager.ShowNotification(
            "Killed Frank by poison at " + shop.name);
        frank.Die();
    }
}
