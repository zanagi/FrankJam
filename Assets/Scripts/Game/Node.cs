using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public List<Node> neighbors;
    public Color gizmoColor;
    private static readonly Color neighborColor = Color.magenta;

    private void Start()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite && Application.isPlaying)
            sprite.enabled = false;
        CheckNeighbors();
    }

    public void CheckNeighbors()
    {
        for (int i = 0; i < neighbors.Count; i++)
        {
            var n = neighbors[i];
            if (!n.neighbors.Contains(this))
                n.neighbors.Add(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = neighborColor;
        for (int i = 0; i < neighbors.Count; i++)
            Gizmos.DrawLine(transform.position, neighbors[i].transform.position);
    }
}
