using UnityEngine;
public class Block : PlayableObject
{
	public SpriteRenderer spriteRenderer;
	
	public override PlayableObject SetData(ItemData newData)
	{
		spriteRenderer = newData.spriteRenderer;
		itemData = newData;
		return this;
	}
	public override PlayableObject SetParent(Transform parent)
	{
		transform.SetParent(parent);
		return this;
	}
	public override PlayableObject SetPosition(Vector2Int position)
	{
		transform.localPosition = new Vector3(position.x, position.y, 0);
		spriteRenderer.sortingOrder = position.y;
		return this;
	}
	public override PlayableObject SetActive(bool active)
	{
		gameObject.SetActive(active);
		return this;
	}
}