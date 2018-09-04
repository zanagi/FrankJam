using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public List<Node> neighbors;
    public Color gizmoColor = Color.magenta;

    private void Start()
    {
        CheckSprite();
        CheckNeighbors();
    }

    protected virtual void CheckSprite()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite)
            sprite.enabled = false;
    }

    public virtual void CheckNeighbors()
    {
        for (int i = 0; i < neighbors.Count; i++)
        {
            var n = neighbors[i];
            if (!n.neighbors.Contains(this))
                n.neighbors.Add(this);
        }
    }

    public virtual bool Wait(Frank frank, float time)
    {
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        for (int i = 0; i < neighbors.Count; i++)
            Gizmos.DrawLine(transform.position, neighbors[i].transform.position);
    }
}
