using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

using DG.Tweening;

public class CellSettings : ScriptableObject {
	[Header("Cell Settings general parameters")]
	[Tooltip("The up distance vector to fall from")]
	public Vector2 fallFromPosition;

	[Tooltip("The scale before animation")]
	public Vector2 scaleBeforeAnimation;

	[Tooltip("The Ease for the fall nimation")]
	public Ease fallAnimationEase;

	[Tooltip("The Ease for the scale animation")]
	public Ease scaleAnimationEase;

	[Tooltip("The duration for the fall animation")]
	public float fallAnimationDuration;
	
	[Tooltip("The Ease for the scale animation")]
	public float scaleAnimationDuration;

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/CellSettings")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CellSettings> ();
	}
	#endif		
}
