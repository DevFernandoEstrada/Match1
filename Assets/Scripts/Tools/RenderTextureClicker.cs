using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RenderTextureClicker : MonoBehaviour, IPointerClickHandler
{
	public event Action<Vector2Int> OnCellClick;
	
	[SerializeField] private Grid grid;
	[SerializeField] private LayerMask boardLayerMask = ~0;
	
	[SerializeField] private Camera renderCamera;

	private RectTransform _rectTransform;

	private void Awake()
	{
		_rectTransform = (RectTransform)transform;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
			_rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 local))
			return;
		
		Rect rectTransformRect = _rectTransform.rect;
		float u = (local.x - rectTransformRect.xMin) / rectTransformRect.width;
		float v = (local.y - rectTransformRect.yMin) / rectTransformRect.height;

		if (u < 0f || u > 1f || v < 0f || v > 1f) return;
		
		Ray ray = renderCamera.ViewportPointToRay(new Vector3(u, v, 0f));
		
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, boardLayerMask);
		if (!hit.collider) return;

		Vector3Int cell = grid.WorldToCell(hit.point);
		OnCellClick?.Invoke(new Vector2Int(cell.x, cell.y));
	}
}