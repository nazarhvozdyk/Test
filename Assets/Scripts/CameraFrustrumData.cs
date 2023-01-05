using UnityEngine;

public class CameraFrustrumData
{
    public static CameraFrustrumData Instance
    {
        get
        {
            if (_instance == null)
                _instance = new CameraFrustrumData();

            return _instance;
        }
    }
    private static CameraFrustrumData _instance;
    private Plane[] _cameraFrustumPlanes;
    public Plane[] CameraFrustumPlanes
    {
        get => _cameraFrustumPlanes;
    }

    private CameraFrustrumData()
    {
        Camera camera = CameraLink.Instance.Camera;
        _cameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
    }
}
