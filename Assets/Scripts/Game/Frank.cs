﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frank : MonoBehaviour {

    public int speed = 1;
    public Node targetNode;
    private Node previousNode;
    private List<Node> visitedNodes;

	// Use this for initialization
	void Start ()
    {
        visitedNodes = new List<Node>();
        transform.position = targetNode.transform.position;
        SetNextNode();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(targetNode)
        {
            var delta = targetNode.transform.position - transform.position;
            var distance = delta.magnitude;
            var scaledSpeed = speed * Time.deltaTime;

            if(distance < scaledSpeed)
            {
                transform.position = targetNode.transform.position;
                SetNextNode();
            } else
            {
                transform.position += delta / distance * scaledSpeed;
            }
        }
	}

    public void SetNextNode()
    {
        var neighbors = targetNode.neighbors;
        var nodes = new List<Node>();

        // Add non-visited nodes
        for(int i = neighbors.Count - 1; i >= 0; i--)
        {
            if (!visitedNodes.Contains(neighbors[i]))
                nodes.Add(neighbors[i]);
        }

        // If no suitable found, randomize a node that is not the previous node
        if(nodes.Count == 0)
        {
            nodes.AddRange(neighbors);

            if (nodes.Count > 1)
                nodes.Remove(previousNode);
        }
        SetNewNode(nodes[Random.Range(0, nodes.Count)]);
    }

    private void SetNewNode(Node node)
    {
        visitedNodes.Add(targetNode);
        previousNode = targetNode;
        targetNode = node;
    }
}