using UnityEngine;

[AddComponentMenu("Foundation/Mono/Set Camera To Canvas")]
[RequireComponent(typeof(Canvas))]
public class CanvasCameraSetter : MonoBehaviour
{
    [SerializeField]
    private int _cameraId;

    private Canvas _canvas;

    public int CameraId => _cameraId;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        var cameras = Camera.allCameras;
        Camera camera = default;

        foreach (var cam in cameras)
        {
            if (cam.TryGetComponent<CanvasCameraMarker>(out var marker)
                && CameraId == marker.CameraId
            )
            {
                camera = cam;
                break;
            }
        }

        if (camera == false)
        {
            camera = Camera.main;
        }

        _canvas.worldCamera = camera;
    }
}
