using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GridIntelligence : BaseObject {

	public Vector3 posByScale;

	[Tooltip("the settings for the grid Intelligence")]
	public GridIntelligenceSettings settings;
	[Tooltip("The settings used for the pawns")]
	public PawnSettings pawnSettings;

	[HideInInspector]
	public int[] moveForceOne;
	[HideInInspector]
	public int[] moveForceTwo;
	[HideInInspector]
	public int[] moveForceThree;
	[HideInInspector]
	public int[][] movePossibilities;

	[HideInInspector]
	public int numberOfColumns;
	[HideInInspector]
	public int numberOfRows;
	[HideInInspector]
	public List<Cell> cells;
	[HideInInspector]
	public List<Cell> whiteCells;
	[HideInInspector]
	public List<Cell> blackCells;
	[HideInInspector]
	public List<Columns> columns;
	
	private List<Cell> _cellsForMove = new List<Cell>();

	protected override void Start (){
		base.Start ();
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

	public virtual void Setup(){
		moveForceOne = new int[4] {1, -numberOfRows/2, numberOfRows/2, -1};
		moveForceTwo = new int[12] {1, 2, -numberOfRows/2, -numberOfRows, numberOfRows/2, numberOfRows,
									-numberOfRows/2 + 1, numberOfRows/2 + 1, -numberOfRows/2 -1, numberOfRows/2 - 1,
									-1, -2};
		moveForceThree = new int[24] {	1, 2, 3, 
										-numberOfRows/2, -numberOfRows, -numberOfRows - numberOfRows /2,
										numberOfRows/2, numberOfRows, numberOfRows + numberOfRows/2,
										-numberOfRows/2 + 1, numberOfRows/2 + 1, -numberOfRows/2 - 1, numberOfRows/2 - 1,
										-numberOfRows/2 + 2, numberOfRows/2 + 2, -numberOfRows/2 - 2, numberOfRows/2 - 2,
										-numberOfRows + 1, numberOfRows + 1, -numberOfRows - 1, numberOfRows - 1,
										-1, -2, -3};

		movePossibilities = new int[3][]{moveForceOne, moveForceTwo, moveForceThree};
	}

	public virtual List<Cell> GetCellsAtIndexForMove(int moveForce, int index){
		_cellsForMove.Clear ();
		Cell baseCell = cells [index];

		if (baseCell as WhiteCell != null) {
			GetCellsForForce (blackCells, _cellsForMove, movePossibilities [moveForce - 1], baseCell.colorIndex);
		} else {
			GetCellsForForce (whiteCells, _cellsForMove, movePossibilities [moveForce - 1], baseCell.colorIndex);
		}

		return _cellsForMove;
	}

	private void GetCellsForForce(List<Cell> colorCells, List<Cell> possibleCells, int[] forcePossibilities, int index){
		for (int i = 0; i < forcePossibilities.Length; i++) {

			int destinationIndex = index + forcePossibilities[i];
			if(CheckCellAt(destinationIndex, colorCells)){
				possibleCells.Add(colorCells[destinationIndex]);
			}
		}

		FilterCells (colorCells, possibleCells, index);
	}

	private void FilterCells(List<Cell> colorCells, List<Cell> possibleCells, int index){
		for(int i = 0; i < possibleCells.Count; i++){
			if(NoNeighbourOnOption(colorCells, possibleCells[i].colorIndex, index)){
				possibleCells.RemoveAt(i);
				i--;
			};
		}
	}

	private bool NoNeighbourOnOption(List<Cell> cellsToCheck, int currentIndex, int baseIndex){
		if(Vector2.Distance(cellsToCheck[currentIndex].basePosition, cellsToCheck[baseIndex].basePosition) <= 6f){
			return false;
		}

		return true;
	}

	private bool CheckCellAt(int index, List<Cell> list){
		return (index > 0 && list.Count > index);
	}
}
