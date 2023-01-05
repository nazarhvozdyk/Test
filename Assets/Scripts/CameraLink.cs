using UnityEngine;

public class CameraLink : MonoBehaviour
{
    public static CameraLink Instance
    {
        get => _instance;
    }
    private static CameraLink _instance;

    [SerializeField]
    private Camera _camera;
    public Camera Camera
    {
        get => _camera;
    }

    private void Awake()
    {
        _instance = this;
    }
}
