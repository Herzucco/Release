using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Cell : BaseObject {
	public Columns column;

	protected Vector2 _basePosition;
	protected Vector2 _baseScale;
	protected SpriteRenderer sRenderer;
	protected CircleCollider2D cCollider;
	protected CellSettings settings;

	protected virtual void Awake(){
		sRenderer = GetComponent<SpriteRenderer> ();
		cCollider = GetComponent<CircleCollider2D> ();

		sRenderer.enabled = false;
		cCollider.enabled = false;

		_basePosition = transform.position;
		_baseScale = transform.localScale;
	}

	public virtual void Prepare(CellSettings s){
		settings = s;

		transform.position = _basePosition + settings.fallFromPosition;
		transform.localScale = settings.scaleBeforeAnimation;
	}

	public virtual void Animate(){
		sRenderer.enabled = true;
		cCollider.enabled = true;

		//do fall
		transform.DOMove (_basePosition, settings.fallAnimationDuration, false).SetEase (settings.fallAnimationEase);

		//do fall
		transform.DOScale (_baseScale, settings.scaleAnimationDuration).SetEase (settings.scaleAnimationEase);
	}
}
