using UnityEngine;

public class BoardScaler : MonoBehaviour
{
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
		transform.localPosition = -boardSize * 0.5f;
	}
}