using UnityEngine;

public class PlayerBodyLook : MonoBehaviour
{
    public static PlayerBodyLook Instance
    {
        get => _instance;
    }
    private static PlayerBodyLook _instance;
    public int Direction
    {
        get
        {
            int direction;
            if (_pelvisTransform.position.x > 0)
                direction = -1;
            else
                direction = 1;

            return direction;
        }
    }

    [SerializeField]
    private Transform _pelvisTransform;

    private void Awake()
    {
        _instance = this;
    }

    private void LateUpdate()
    {
        if (_pelvisTransform.position.x > 0)
            _pelvisTransform.eulerAngles = new Vector3(0, 180, 0);
        else
            _pelvisTransform.eulerAngles = Vector3.zero;
    }
}
