using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _speed = 4;

    [SerializeField]
    private int _damage = 1;

    private void Start()
    {
        _rigidbody.AddForce(Vector3.up * _speed, ForceMode.VelocityChange);
        Invoke(nameof(DestroyItSelf), 3.5f);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Obstacle obstacle = collider.GetComponent<Obstacle>();

        if (obstacle == null)
            return;

        obstacle.TakeDamage(_damage);
        Destroy(gameObject);
    }

    private void DestroyItSelf()
    {
        Destroy(gameObject);
    }
}
