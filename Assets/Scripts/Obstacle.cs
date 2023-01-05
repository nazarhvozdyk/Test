using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int _healthPoints;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private TextMeshPro _textMeshPro;

    public void SetUp(int health, Color color)
    {
        _meshRenderer.material.color = color;
        _healthPoints = health;
        _textMeshPro.text = _healthPoints.ToString();
    }

    public void TakeDamage(int damage)
    {
        _healthPoints -= damage;

        if (_healthPoints <= 0)
            Destroy(gameObject);

        _textMeshPro.text = _healthPoints.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.collider.GetComponent<Player>();

        if (player)
            player.Kill();
    }
}
