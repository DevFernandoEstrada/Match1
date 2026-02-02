using UnityEngine;

public class CameraScaler : MonoBehaviour
{
	[SerializeField] private float padding = 1f;
	private Camera _cam;

	private void Awake()
	{
		_cam = GetComponent<Camera>();
	}
	
	public void SetCameraSize(Vector2Int boardSize)
	{
		float gridWidth = boardSize.x;
		float gridHeight = boardSize.y;
		float screenRatio = Screen.width / (float)Screen.height;
		float targetRatio = gridWidth / gridHeight;

		if (screenRatio >= targetRatio)
		{
			_cam.orthographicSize = gridHeight * 0.5f + padding;
		}
		else
		{
			float differenceInSize = targetRatio / screenRatio;
			_cam.orthographicSize = (gridHeight * 0.5f + padding) * differenceInSize;
		}
	}
}