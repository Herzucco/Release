using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GridIntelligence : BaseObject {
	public List<Cell> cells;
	public List<Columns> columns;

	public Vector3 posByScale;

	[Tooltip("the settings for the grid Intelligence")]
	public GridIntelligenceSettings settings;
	[Tooltip("The settings used for the pawns")]
	public PawnSettings pawnSettings;

	void Start (){
		StartCoroutine (GeneratePawns ());
	}

	public IEnumerator GeneratePawns (){
		GameObject pawnsRoot = new GameObject ("PawnsRoot");
		Transform pawnsRootTransform = pawnsRoot.GetComponent<Transform>();
		pawnsRootTransform.position = GetComponent<Transform> ().position;
		for (int i = 0; i < settings.whiteStartPositions.Length; i++) {
			if(settings.whiteStartPositions[i]){
				GeneratePawn(CellColor.WHITE, new Vector2(i,0), pawnsRootTransform);
				yield return new WaitForSeconds(settings.spawnApparitionInterval);
			}
		}
		for (int i = 0; i < settings.blackStartPositions.Length; i++) {
			if(settings.blackStartPositions[i]){
				GeneratePawn(CellColor.BLACK, new Vector2(i,9), pawnsRootTransform);
				yield return new WaitForSeconds(settings.spawnApparitionInterval);
			}
		}
	}

	public void GeneratePawn(CellColor color, Vector2 position, Transform root){
		GameObject pawn = Instantiate<GameObject> (settings.pawnPrefab);
		Pawn p;
		Transform pTrans = pawn.GetComponent<Transform> ();
		if (color == CellColor.WHITE) {
			p = pawn.AddComponent<WhitePawn>() as Pawn;
			pTrans.localRotation = Quaternion.Euler (0, 0, 180);
		} else {
			p = pawn.AddComponent<BlackPawn>() as Pawn;
			p.GetComponent<SpriteRenderer>().color = Color.black;
		}
		pTrans.parent = root;
		pTrans.localPosition = new Vector3(position.x * posByScale.x, -position.y * posByScale.y, 1);
		Vector3 _baseScale = pTrans.localScale;
		pTrans.localScale = settings.beforeApparitionScale;
		pTrans.DOScale (_baseScale, settings.scaleApparitionAnimationDuration);
		//pawn
		Debug.Log ("toto " + color.ToString() + " " + position.ToString());
	}
}
