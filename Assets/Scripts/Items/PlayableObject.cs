using UnityEngine;

public abstract class PlayableObject : MonoBehaviour
{
	protected ItemData itemData;

	public abstract PlayableObject SetData(ItemData newData);
	
	public abstract PlayableObject SetParent(Transform parent);
	
	public abstract PlayableObject SetPosition(Vector2Int position);
	
	public abstract PlayableObject SetActive(bool active);
	
	public bool Compare(PlayableObject other) => other.itemData == itemData;
}