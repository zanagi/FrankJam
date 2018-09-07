using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationWindow : SelectableWindow {

    public int bombPrice, poisonPrice;
    public Text bombPriceText, poisonPriceText;

    protected override void Update()
    {
        base.Update();
        bombPriceText.text = string.Format("Bomb ({0})", bombPrice);
        poisonPriceText.text = string.Format("Poison ({0})", poisonPrice);
    }

    public void BuyBomb()
    {
        if(GameManager.Instance.SpendMoney(bombPrice))
        {
            GameManager.Instance.weaponManager.AddBombs(1);
            GameManager.Instance.notificationManager.ShowNotification(
                "You bought a bomb!");
        } else
        {
            GameManager.Instance.notificationManager.
                ShowNotification("You don't have enough money!", NotificationId.Money);
        }
    }

    public void BuyPoison()
    {
        if (GameManager.Instance.SpendMoney(poisonPrice))
        {

            GameManager.Instance.weaponManager.AddPoison(1);
            GameManager.Instance.notificationManager.ShowNotification(
                "You bought a dial of poison!");
        }
        else
        {
            GameManager.Instance.notificationManager.
                ShowNotification("You don't have enough money!", NotificationId.Money);
        }
    }
}
