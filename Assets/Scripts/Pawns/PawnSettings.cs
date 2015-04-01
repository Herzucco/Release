using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using DG.Tweening;

public class PawnSettings : ScriptableObject {

	[Header("Cell Settings general parameters")]

	[Tooltip("Time During the spawning of two cells")]
	public float spawnApparitionInterval;

	[Tooltip("The base Scale for apparition")]
	public Vector3 beforeApparitionScale;

	[Tooltip("The Ease for the scale animation in apparition")]
	public float scaleApparitionAnimationDuration;

	[Tooltip("white pawn start positions")]
	public bool[] whiteStartPositions; 

	[Tooltip("black pawn start positions")]
	public bool[] blackStartPositions;

	[Tooltip("pawn Prefab")]
	public GameObject pawnPrefab;

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/PawnSettings")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<PawnSettings> ();
	}


	#endif	
}
