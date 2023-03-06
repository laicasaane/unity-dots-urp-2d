using UnityEngine;

[AddComponentMenu("Foundation/Mono/Mark Camera For Canvas")]
[RequireComponent(typeof(Camera))]
public class CanvasCameraMarker : MonoBehaviour
{
    [SerializeField]
    private int _cameraId;

    public int CameraId => _cameraId;
}