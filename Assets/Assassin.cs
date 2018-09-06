using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour {

	public float speed = 1, animatorSpeed = 0.6f;
	public Node targetNode;
	public SpriteRenderer spriteRenderer;
	public float animModifier = 3.0f;

	private Node previousNode;
	private List<Node> visitedNodes;
	private Animator animator;
	private static readonly string frontBool = "Front";

	// Use this for initialization
	void Start()
	{
		visitedNodes = new List<Node>();
		transform.position = targetNode.transform.position;
		SetNextNode();

		// Animator delay
		animator = GetComponentInChildren<Animator>();
		animator.speed = animatorSpeed;
		animator.Update(Random.Range(0.0f, 1.0f));
	}

	// Update is called once per frame
	void Update()
	{
		if (!GameManager.Instance.IsIdle)
		{
			return;
		}
		if (targetNode)
		{
			var delta = targetNode.transform.position - transform.position;
			var distance = delta.magnitude;
			var scaledSpeed = speed * Time.deltaTime;

			if (distance < scaledSpeed)
			{
				transform.position = targetNode.transform.position;
				SetNextNode();
			}
			else
			{
				transform.position += delta / distance * scaledSpeed;
			}
			var angle = Mathf.Min(Vector2.Angle(Vector2.right, delta), Vector2.Angle(Vector2.left, delta));

			if (angle <= 45)
			{
				animator.SetBool(frontBool, false);
				spriteRenderer.flipX = targetNode.transform.position.x < transform.position.x;
			}
			else
			{
				animator.SetBool(frontBool, true);
				spriteRenderer.flipX = false;
			}
		}
	}

	public void SetNextNode()
	{
		var neighbors = targetNode.neighbors;
		var nodes = new List<Node>();

		// Add non-visited nodes
		for (int i = neighbors.Count - 1; i >= 0; i--)
		{
			if (!visitedNodes.Contains(neighbors[i]))
				nodes.Add(neighbors[i]);
		}

		// If no suitable found, randomize a node that is not the previous node
		if (nodes.Count == 0)
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var frank = collision.GetComponent<Frank>();

		if(frank)
		{
			Debug.Log("COntacfkljldk");
		}
	}
}
