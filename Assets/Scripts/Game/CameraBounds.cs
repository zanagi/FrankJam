using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
	public static CameraBounds Instance { get; private set; }
	public Vector3 min, max;

	void Awake()
	{
		Instance = this;
	}

	public Vector3 GetPosInBounds(Vector3 newPos, Vector3 cameraMin, Vector3 cameraMax)
	{
        bool xChanged = false, yChanged = false;
		var pos = newPos;
		pos.x = Mathf.Clamp(pos.x, min.x, max.x);
		pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        var halfX = 0.5f * (cameraMax.x - cameraMin.x);
        var halfY = 0.5f * (cameraMax.y - cameraMin.y);
        
        if (cameraMin.x < min.x)
        {
            pos.x = min.x + halfX;
            xChanged = true;
        }
        if(cameraMax.x > max.x)
        {
            if (xChanged)
                pos.x = 0;
            else
                pos.x = max.x - halfX;
        }
        if (cameraMin.y < min.y)
        {
            pos.y = min.y + halfY;
            yChanged = true;
        }
        if (cameraMax.y > max.y)
        {
            if (yChanged)
                pos.y = 0;
            else
                pos.y = max.y - halfY;
        }
        return pos;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red * 0.5f;
		Gizmos.DrawCube(0.5f * (min + max) + transform.position + Vector3.up * 0.01f, max - min + Vector3.up * 0.01f);
	}
}
