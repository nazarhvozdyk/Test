using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private static readonly string SpeedKey = "Speed";
    private static readonly string DirectionKey = "Direction";

    [SerializeField]
    private Animator _animator;

    public void MoveToPosition(Vector3 position)
    {
        float moveDelta = position.x - transform.position.x;
        int lookDirection = PlayerBodyLook.Instance.Direction;

        _animator.SetFloat(SpeedKey, Mathf.Abs(moveDelta));
        int moveDirection = lookDirection;

        if (moveDelta < -0.01f)
        {
            moveDirection *= -1;
            _animator.SetFloat(DirectionKey, moveDirection);
        }
        else if (moveDelta > 0.01f)
        {
            _animator.SetFloat(DirectionKey, moveDirection);
        }

        transform.position = position;
    }
}
