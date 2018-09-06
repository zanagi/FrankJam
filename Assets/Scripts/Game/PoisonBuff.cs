using UnityEngine;
using System.Collections;

[System.Serializable]
public class PoisonBuff : ShopBuff
{
	private static string deathSfxName = "DeathPoison";

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
            "Killed Frank by poison at " + shop.shopName);
		SFXManager.Instance.PlaySFX(deathSfxName);
		frank.Die();
    }

    public override string EndText(Shop shop)
    {
        return string.Format("Drinks at {0} no longer poisoned.", shop.shopName);
    }

    public override string StartText(Shop shop)
    {
        return string.Format("Drinks poisoned at {0}.", shop.shopName);
    }
}
