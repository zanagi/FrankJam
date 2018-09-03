using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetNeighbors : MonoBehaviour {

    [MenuItem("Node/Set Neighbors")]
    public static void ShowWindow()
    {
        var nodes = FindObjectsOfType<Node>();

        for (int i = 0; i < nodes.Length; i++)
            nodes[i].CheckNeighbors();
    }
}
