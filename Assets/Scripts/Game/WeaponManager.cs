using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

    public int poisons, bombs;
    public Text poisonCountText, bombCountText;

    public void AddPoison(int count)
    {
        poisons += count;
        poisonCountText.text = poisons.ToString();
    }

    public void AddBombs(int count)
    {
        bombs += count;
        bombCountText.text = bombs.ToString();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsIdle)
            return;
        CheckPoison();
        CheckBomb();
    }

    private void CheckPoison()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (poisons > 0)
            {

                Debug.Log("poison..");
            }
            else
            {
                GameManager.Instance.notificationManager.ShowNotification(
                    "You don't have any poison!", NotificationId.Poison);
            }
        }
    }
    private void CheckBomb()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(bombs > 0)
            {

                Debug.Log("Bomb..");
            } else
            {
                GameManager.Instance.notificationManager.ShowNotification(
                    "You don't have any bombs!", NotificationId.Bomb);
            }
        }
    }
}
