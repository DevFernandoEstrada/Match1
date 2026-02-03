using UnityEngine;

public class CameraScaler : MonoBehaviour
{
	[SerializeField] private float padding = 1f;
	[SerializeField, Range(0f, 1f)] private float paddingThreshold = 0.75f;
	
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
		float baseOrthoSize;

		if (screenRatio >= targetRatio)
		{
			baseOrthoSize = gridHeight * 0.5f;
		}
		else
		{
			float differenceInSize = targetRatio / screenRatio;
			baseOrthoSize = (gridHeight * 0.5f) * differenceInSize;
		}

		float cameraHeight = baseOrthoSize * 2f;
		float relativeDiff = Mathf.Abs(cameraHeight - gridHeight) / gridHeight;
		float appliedPadding = relativeDiff <= paddingThreshold ? padding : 0f;
		_cam.orthographicSize = baseOrthoSize + appliedPadding;
	}
}
