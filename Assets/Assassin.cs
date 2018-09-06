using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : SelectableObject {

	public float speed = 1, animatorSpeed = 0.6f;
	public Node targetNode;
	public SpriteRenderer spriteRenderer, background;
	public Color normalColor = Color.yellow * 0.5f, hiredColor = Color.red * 0.5f;
	public float animModifier = 3.0f, hiringTime;
	public bool hired;
	public int price = 2000, priceIncrease = 1000;

	private Node previousNode;
	private List<Node> visitedNodes;
	private Animator animator;
	private static readonly string frontBool = "Front", killSfx = "DeathAssassin";

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		visitedNodes = new List<Node>();
		transform.position = targetNode.transform.position;
		SetNextNode();
		background.color = normalColor;

		// Animator delay
		animator = GetComponentInChildren<Animator>();
		animator.speed = animatorSpeed;
		animator.Update(Random.Range(0.0f, 1.0f));

		// Window effects
		windowInstance.actionButton1.onClick.AddListener(() => Hire());
		SetPriceText();
	}

	private void SetPriceText()
	{
		windowInstance.price.text = string.Format("{0} MK", price);
	}

	private void Hire()
	{
		if(GameManager.Instance.SpendMoney(price))
		{
			background.color = hiredColor;
			hired = true;

			// Deselect window after buying event
			OnDeselect();

			GameManager.Instance.notificationManager.
				ShowNotification("Assassin hired!");
		}
	}

	// Update is called once per frame
	protected override void Update()
	{
		if (!GameManager.Instance.IsIdle)
		{
			animator.speed = 0.0f;
			return;
		}
		animator.speed = animatorSpeed;
		base.Update();

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
		if (hired)
		{
			var frank = collision.GetComponent<Frank>();

			if (frank)
			{
				GameManager.Instance.notificationManager.
					ShowNotification("Frank killed by an assassin!");
				SFXManager.Instance.PlaySFX(killSfx);
				frank.Die();
				hired = false;
				background.color = normalColor;
				price += priceIncrease;
				SetPriceText();
			}
		}
	}
}
