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
		spriteRenderer.size = boardSize + GetSpriteOffset(boardSize);
		transform.localPosition = -boardSize * 0.5f;
	}

	private Vector2 GetSpriteOffset(Vector2 boardSize)
	{
		Sprite sprite = spriteRenderer.sprite;
		return new Vector2(sprite.pivot.x / sprite.rect.width, sprite.pivot.y / sprite.rect.height) * boardSize * 2f;
	}
}