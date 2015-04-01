using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridIntelligence : BaseObject {
	public List<Cell> cells;
	public List<Columns> columns;

	[Tooltip("The settings used for the pawns")]
	public PawnSettings pawnSettings;

	void Start (){
		StartCoroutine (GeneratePawns ());
	}

	public IEnumerator GeneratePawns (){
		for (int i = 0; i < pawnSettings.whiteStartPositions.Length; i++) {
			if(pawnSettings.whiteStartPositions[i]){
				GeneratePawn(CellColor.WHITE, new Vector2(i,1));
				yield return new WaitForSeconds(pawnSettings.spawnApparitionInterval);
			}
		}
	}

	public void GeneratePawn(CellColor color, Vector2 position){
		Debug.Log ("toto " + color.ToString() + " " + position.ToString());
	}
}
