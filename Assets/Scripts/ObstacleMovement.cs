using System.Collections;
using UnityEditor;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private Rigidbody _rigidbody;
    private int _indexOfLastCollidedPlane;
    private float _timer;

    private void Start()
    {
        StartCoroutine(MoveInsideScene());
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        float timeToResetLastCollidedPlaneIndex = 1f;
        if (_timer > timeToResetLastCollidedPlaneIndex)
        {
            _timer = 0;
            _indexOfLastCollidedPlane = -1;
        }
        CheckCollisionWithScreenSides();
    }

    private void CheckCollisionWithScreenSides()
    {
        Vector3[] foregroundBoxVertices = GetForegroundBoxVertices();
        Plane[] cameraFrustrumPlanes = CameraFrustrumData.Instance.CameraFrustumPlanes;

        for (int i = 0; i < foregroundBoxVertices.Length; i++)
        {
            for (int j = 0; j < cameraFrustrumPlanes.Length; j++)
            {
                if (cameraFrustrumPlanes[j].GetSide(foregroundBoxVertices[i]))
                    continue;

                if (_indexOfLastCollidedPlane == j)
                    return;

                Vector3 screenSideToCenterNormal = cameraFrustrumPlanes[j].normal;
                screenSideToCenterNormal.z = 0;
                ReflectVelocity(screenSideToCenterNormal);

                _indexOfLastCollidedPlane = j;
                return;
            }
        }
    }

    private Vector3[] GetForegroundBoxVertices()
    {
        Vector3[] foregroundBoxVertices = new Vector3[4];
        Bounds bounds = _collider.bounds;
        foregroundBoxVertices[0] = bounds.max;
        foregroundBoxVertices[1] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        foregroundBoxVertices[2] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        foregroundBoxVertices[3] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        return foregroundBoxVertices;
    }

    // returns true is entire obstacle inside the camera view
    private bool IsFullInsideCameraView()
    {
        Vector3[] foregroundBoxVertices = GetForegroundBoxVertices();
        Plane[] cameraFrustrumPlanes = CameraFrustrumData.Instance.CameraFrustumPlanes;

        for (int i = 0; i < foregroundBoxVertices.Length; i++)
            for (int j = 0; j < cameraFrustrumPlanes.Length; j++)
                if (!cameraFrustrumPlanes[j].GetSide(foregroundBoxVertices[i]))
                    return false;

        return true;
    }

    private void ReflectVelocity(Vector3 normal)
    {
        float speed = _rigidbody.velocity.magnitude;
        Vector3 direction = _rigidbody.velocity.normalized;
        Vector3 reflectedDirection = Vector3.Reflect(direction, normal);
        _rigidbody.velocity = reflectedDirection * speed;
    }

    private IEnumerator MoveInsideScene()
    {
        _rigidbody.useGravity = false;
        _rigidbody.detectCollisions = false;
        enabled = false;

        Camera camera = CameraLink.Instance.Camera;
        Vector3 toCamera = transform.position - camera.transform.position;

        if (toCamera.x < 0)
            _rigidbody.AddForce(Vector3.right, ForceMode.VelocityChange);
        else
            _rigidbody.AddForce(Vector3.left, ForceMode.VelocityChange);

        while (!IsFullInsideCameraView())
            yield return new WaitForFixedUpdate();

        _rigidbody.detectCollisions = true;
        _rigidbody.useGravity = true;
        enabled = true;
    }
}
