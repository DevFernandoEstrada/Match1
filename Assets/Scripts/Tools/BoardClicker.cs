using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardClicker : MonoBehaviour, IPointerClickHandler
{
	public event Action<Vector2Int> OnCellClick;

	[SerializeField] private Grid grid;
	[SerializeField] private BoxCollider2D boxCollider;
	[SerializeField] private SpriteRenderer spriteRenderer;

	public void SetupBoard(Vector2Int newBoardSize)
	{
		Vector2 boardSize = newBoardSize;
		boxCollider.size = boardSize;
		boxCollider.offset = boardSize * 0.5f;
		spriteRenderer.size = boardSize;
		spriteRenderer.transform.localPosition = boardSize * 0.5f;
		transform.position = -boardSize * 0.5f;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Vector3 clickPosition = eventData.pointerCurrentRaycast.worldPosition;
		Vector3Int cellPos = grid.WorldToCell(clickPosition);
		OnCellClick?.Invoke(new Vector2Int(cellPos.x, cellPos.y));
	}
}