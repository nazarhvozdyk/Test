using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float _shootRate = 0.15f;

    [Space()]
    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private Transform _bulletSpawnTransform;

    [SerializeField]
    private LayerMask _obstacleLayer;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _shootRate)
            Shoot();
    }

    private void Shoot()
    {
        Bullet newBullet = Instantiate(_bulletPrefab);
        newBullet.transform.position = _bulletSpawnTransform.position;
        newBullet.transform.forward = Vector3.up;
        _timer = 0;
    }
}
