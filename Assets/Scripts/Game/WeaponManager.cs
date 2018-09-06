using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

    public int poisons, bombs;
    public Text poisonCountText, bombCountText;
    public GameObject bombPrefab;
    public LayerMask shopLayer;
    public PoisonBuff poisonBuff;

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
                var worldPos = GameManager.Instance.GameCamera.PointerWorldPos;
                var info = Physics2D.Raycast(worldPos, Vector2.up, 0.01f, shopLayer);
                if (info.collider)
                {
                    var shop = info.collider.GetComponent<Shop>();
                    if(shop)
                    {
                        shop.AddBuff(poisonBuff.Clone());
                        AddPoison(-1);
                        return;
                    }
                }
                GameManager.Instance.notificationManager.ShowNotification(
                        "You must use poison on a bar!", NotificationId.Poison2);
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
                var worldPos = GameManager.Instance.GameCamera.Camera.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 0;
                var bomb = Instantiate(bombPrefab);
                bomb.transform.position = worldPos;
                AddBombs(-1);
            } else
            {
                GameManager.Instance.notificationManager.ShowNotification(
                    "You don't have any bombs!", NotificationId.Bomb);
            }
        }
    }
}
