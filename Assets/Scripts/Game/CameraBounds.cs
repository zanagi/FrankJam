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

	public Vector3 GetPosInBounds(Vector3 newPos, Vector3 offset)
	{
		var pos = newPos;
		pos.x = Mathf.Clamp(pos.x, min.x + offset.x, max.x + offset.x);
		pos.y = Mathf.Clamp(pos.y, min.y + offset.y, max.y + offset.y);
		return pos;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red * 0.5f;
		Gizmos.DrawCube(0.5f * (min + max) + transform.position + Vector3.up * 0.01f, max - min + Vector3.up * 0.01f);
	}
}
