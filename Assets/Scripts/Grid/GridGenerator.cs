using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CellColor{
	WHITE,
	BLACK
}
public class GridGenerator : BaseObject {
	[Header("The Grid Generator general settings")]
	[Tooltip("The time to wait before starting the grid generation")]
	public float timeBeforeGeneration;
	
	[Tooltip("Number of cells on x axis")]
	public int numberOfXCells;

	[Tooltip("Number of cells on y axis")]
	public int numberOfYCells;

	[Tooltip("Defines the color of the first cell to be generated")]
	public CellColor firstCellColor;

	[Tooltip("Curve of time spent between each cell instanciation")]
	public SimpleCurveHandler timeToInstanciateCellCurve;

	[Tooltip("The power attributed on columns, ruled by a curve")]
	public SimpleCurveHandler powerColumnsCurve;

	[Header("Cells Settings")]
	[Tooltip("The scale size of each cell - will define positionning too")]
	public Vector2 cellScaleSize;

	[Tooltip("The offset between each cell")]
	public float offsetBetweenCell;

	[Tooltip("Sprite for the white cells")]
	public Sprite whiteCellSprite;

	[Tooltip("Sprite for the black cells")]
	public Sprite blackCellSprite;

	protected List<Columns> columns = new List<Columns>();
	protected CellColor currentCellColor;

	protected override void Start (){
		base.Start ();
		currentCellColor = firstCellColor;

		StartCoroutine(GenerateGrid ());
	}

	protected virtual IEnumerator GenerateGrid(){
		yield return WaitForSeconds (timeBeforeGeneration);

		List<Cell> cells = new List<Cell> ();

		for(int i = 0; i < numberOfXCells; i++){
			int currentPower = (int) powerColumnsCurve.Evaluate(i);
			columns.Add(new Columns(currentPower));

			for(int u = 0; u < numberOfYCells; u++){
				Cell cell = GenerateCell(i, u);
				cell.column = columns[columns.Count-1];
				cells.Add(cell);

				currentCellColor = (currentCellColor == CellColor.BLACK) ? CellColor.WHITE : CellColor.BLACK;
			}
		}
	}

	protected virtual Cell GenerateCell(int x, int y){
		GameObject go = new GameObject ("Cell - x : " + x + " - y : " + y + " - color : " + currentCellColor.ToString ());
		go.transform.parent = transform;
		go.transform.localScale = cellScaleSize;
		go.transform.localPosition = new Vector2(x * cellScaleSize.x + offsetBetweenCell * x, -y * cellScaleSize.y + offsetBetweenCell * -y);
		go.AddComponent<CircleCollider2D> ();
		SpriteRenderer sprite = go.AddComponent<SpriteRenderer> ();

		return (currentCellColor == CellColor.BLACK) ? GenerateBlackCell(go, sprite) : GenerateWhiteCell(go,sprite);
	}

	protected virtual Cell GenerateBlackCell(GameObject go, SpriteRenderer sRenderer){
		sRenderer.sprite = blackCellSprite;
		return go.AddComponent<BlackCell> ();
	}

	protected virtual Cell GenerateWhiteCell(GameObject go, SpriteRenderer sRenderer){
		sRenderer.sprite = whiteCellSprite;
		return go.AddComponent<WhiteCell> ();
	}
}
