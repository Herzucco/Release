﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Cell : BaseObject {
	[HideInInspector]
	public int index;
	[HideInInspector]
	public int colorIndex;
	[HideInInspector]
	public Columns column;

	[HideInInspector]
	public Vector2 basePosition;
	[HideInInspector]
	public Vector2 baseScale;

	protected SpriteRenderer sRenderer;
	protected CircleCollider2D cCollider;
	protected CellSettings settings;
	protected bool isListening;

	protected virtual void Awake(){
		sRenderer = GetComponent<SpriteRenderer> ();
		cCollider = GetComponent<CircleCollider2D> ();

		sRenderer.enabled = false;
		cCollider.enabled = false;

		basePosition = transform.position;
		baseScale = transform.localScale;
	}

	public virtual void Prepare(CellSettings s){
		settings = s;

		transform.position = basePosition + settings.fallFromPosition;
		transform.localScale = settings.scaleBeforeAnimation;
	}

	public virtual void Animate(){
		sRenderer.enabled = true;
		cCollider.enabled = true;

		//do fall
		transform.DOMove (basePosition, settings.fallAnimationDuration, false).SetEase (settings.fallAnimationEase);

		//do fall
		transform.DOScale (baseScale, settings.scaleAnimationDuration).SetEase (settings.scaleAnimationEase);
	}

	public virtual void ListenSelection(){
		isListening = true;

	}

	public virtual void ChangeColor(Color color){
		sRenderer.color = color;
	}
}
