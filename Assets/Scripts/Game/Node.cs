using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public List<Node> neighbors;
    public Color gizmoColor;

    private void Start()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite)
            sprite.enabled = false;

        for(int i = 0; i < neighbors.Count; i++)
        {
            var n = neighbors[i];
            if (!n.neighbors.Contains(this))
                n.neighbors.Add(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
