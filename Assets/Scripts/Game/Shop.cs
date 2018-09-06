using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Shop : SelectableObject {

    public string shopName = "Test";
    [TextArea]
    public string description = "Just a shop";
    public ShopBuff buff1, buff2;

    public float visibility = 25f;
    public float waitTime = 5f;
    private List<ShopBuff> buffs = new List<ShopBuff>();

    protected override void Start()
    {
        base.Start();
        windowInstance.title.text = shopName;
        windowInstance.description.text = description;

        if(buff1.name.Length > 0)
        {
            windowInstance.actionButton1.GetComponentInChildren<Text>().text = buff1.ToString();
            windowInstance.actionButton1.onClick.AddListener(() => AddBuff(buff1));
        } else
        {
            windowInstance.actionButton1.gameObject.SetActive(false);
        }

        if (buff2.name.Length > 0)
        {
            windowInstance.actionButton2.GetComponentInChildren<Text>().text = buff2.ToString();
            windowInstance.actionButton2.onClick.AddListener(() => AddBuff(buff2));
        }
        else
        {
            windowInstance.actionButton2.gameObject.SetActive(false);
        }
    }

    protected override void Update()
    {
        if (!GameManager.Instance.IsIdle)
            return;
        base.Update();

        var count = buffs.Count;
        for(int i = count - 1; i >= 0; i--)
        {
            if(buffs[i].UpdateTime(Time.deltaTime))
            {
                buffs.RemoveAt(i);
            }
        }
    }

    public void AddBuff(ShopBuff buff)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].name == buff.name)
            {
                // TODO: Notification that buff is active
                GameManager.Instance.notificationManager.ShowNotification("That event is ongoing!");
                return;
            }
        }

        // TODO: Notification that buff is active
        if (GameManager.Instance.SpendMoney(buff.cost))
        {
            GameManager.Instance.notificationManager.
                ShowNotification(string.Format("Started {0} at {1}", buff.name, shopName));
            buffs.Add(buff.Clone());
        } else
        {
            GameManager.Instance.notificationManager.
                ShowNotification("You don't have enough money!");
        }
    }

    public override void OnSelect()
    {
        base.OnSelect();
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
    }

    public void OnFrankContact(Frank frank)
    {
        for (int i = 0; i < buffs.Count; i++)
            buffs[i].OnFrankContact(frank);
    }

    public float TotalWaitTime
    {
        get
        {
            var total = waitTime;
            var count = buffs.Count;
            for(int i = 0; i < count; i++)
                total += buffs[i].waitTimeBuff;
            return total;
        }
    }

    public float TotalVisibility
    {
        get
        {
            var total = visibility;
            var count = buffs.Count;
            for (int i = 0; i < count; i++)
                total += buffs[i].visibilityBuff;
            return total;
        }
    }
}
