using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridIntelligence : BaseObject {
	public int[] moveForceOne;
	public int[] moveForceTwo;
	public int[] moveForceThree;
	public int[][] movePossibilities;
	
	public int numberOfColumns;
	public int numberOfRows;
	public List<Cell> cells;
	public List<Cell> whiteCells;
	public List<Cell> blackCells;
	public List<Columns> columns;

	private List<Cell> _cellsForMove = new List<Cell>();
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
