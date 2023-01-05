using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerMove _playerMove;

    [SerializeField]
    private Camera _camera;
    private Plane _verticalPlaneAtZeroPosition;

    private void Start()
    {
        _verticalPlaneAtZeroPosition = new Plane(Vector3.forward, Vector3.zero);
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        float distance;
        _verticalPlaneAtZeroPosition.Raycast(ray, out distance);

        Vector3 hitPoint = ray.GetPoint(distance);

        Vector3 newPlayerPosition = new Vector3(hitPoint.x, 0, 0);
        _playerMove.MoveToPosition(newPlayerPosition);
    }
}
