using UnityEngine;
using System.Collections;

public class Cell : BaseObject {
	public Columns column;

	protected Vector2 _basePosition;
	protected Vector2 _baseScale;
	protected SpriteRenderer sRenderer;
	protected CircleCollider2D cCollider;

	protected virtual void Awake(){
		sRenderer = GetComponent<SpriteRenderer> ();
		cCollider = GetComponent<CircleCollider2D> ();

		sRenderer.enabled = false;
		cCollider.enabled = false;

		_basePosition = transform.position;
		_baseScale = transform.localScale;
	}
}
