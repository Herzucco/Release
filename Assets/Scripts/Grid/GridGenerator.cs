﻿using UnityEngine;
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

	[Tooltip("Define if the cell start animation should start from end or begining")]
	public bool shouldStartAtEnd = true;

	[Header("Cells Settings")]
	[Tooltip("The scale size of each cell - will define positionning too")]
	public Vector2 cellScaleSize;

	[Tooltip("The offset between each cell")]
	public float offsetBetweenCell;

	[Tooltip("Sprite for the white cells")]
	public Sprite whiteCellSprite;

	[Tooltip("Sprite for the black cells")]
	public Sprite blackCellSprite;

	[Tooltip("The settings injected into the cells")]
	public CellSettings cellSettings;
	
	protected CellColor currentCellColor;

	protected override void Start (){
		base.Start ();
		currentCellColor = firstCellColor;

		StartCoroutine(GenerateGrid ());
	}

	protected virtual IEnumerator GenerateGrid(){
		yield return WaitForSeconds (timeBeforeGeneration);

		List<Cell> cells = new List<Cell> ();
		List<Columns> columns = new List<Columns>();

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

		StartCoroutine(AnimateCells (cells));
		AddIntelligence (cells, columns);
	}

	protected virtual Cell GenerateCell(int x, int y){
		GameObject go = new GameObject ("Cell - x : " + x + " - y : " + y + " - color : " + currentCellColor.ToString ());
		go.transform.parent = transform;
		go.transform.localScale = cellScaleSize;
		go.transform.localPosition = new Vector2(x * cellScaleSize.x + offsetBetweenCell * x, -y * cellScaleSize.y + offsetBetweenCell * -y);
		go.AddComponent<CircleCollider2D> ();
		SpriteRenderer sprite = go.AddComponent<SpriteRenderer> ();

		Cell toReturn = (currentCellColor == CellColor.BLACK) ? GenerateBlackCell(go, sprite) : GenerateWhiteCell(go,sprite);
		toReturn.Prepare (cellSettings);

		return toReturn;
	}

	protected virtual Cell GenerateBlackCell(GameObject go, SpriteRenderer sRenderer){
		sRenderer.sprite = blackCellSprite;
		return go.AddComponent<BlackCell> ();
	}

	protected virtual Cell GenerateWhiteCell(GameObject go, SpriteRenderer sRenderer){
		sRenderer.sprite = whiteCellSprite;
		return go.AddComponent<WhiteCell> ();
	}

	protected virtual IEnumerator AnimateCells(List<Cell> cells){
		int direction = 1;

		for(int i = numberOfYCells; i > 0; i--){

			if(direction == 1){
				for(int u = 0; u < numberOfXCells; u++){
					timeToInstanciateCellCurve.UpdateValue(1f);

					Cell cell = cells[i-1 + (numberOfYCells * u)];
					cell.Animate();
					yield return WaitForSeconds(timeToInstanciateCellCurve.currentValue);
				}
			}else{
				for(int u = numberOfXCells-1; u >= 0 ; u--){
					timeToInstanciateCellCurve.UpdateValue(1f);

					Cell cell = cells[i-1 + (numberOfYCells * u)];
					cell.Animate();
					yield return WaitForSeconds(timeToInstanciateCellCurve.currentValue);
				}
			}

			direction *= -1;
		}
	}

	protected virtual void AddIntelligence(List<Cell> cells, List<Columns> columns){
		GridIntelligence intelligence = gameObject.AddComponent<GridIntelligence> ();
		intelligence.cells = cells;
		intelligence.columns = columns;
	}
}
