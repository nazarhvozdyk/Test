using System.Collections;
using UnityEngine;

public class ObstaclesCreator : MonoBehaviour
{
    private static readonly Color[] _obstaclesColors =
    {
        Color.green,
        Color.red,
        Color.blue,
        Color.yellow
    };

    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private Obstacle _obstaclePrefab;

    [SerializeField]
    private float _spawnRate = 5;

    [SerializeField]
    private int _maxObstaclesInOneWave = 2;

    [SerializeField]
    private int _maxObstacleHealth = 10;
    private float _timer;

    private void Start()
    {
        int newObstaclesAmount = Random.Range(1, _maxObstaclesInOneWave + 1);
        StartCoroutine(SpawnObstacles(newObstaclesAmount));
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _spawnRate)
            return;

        int newObstaclesAmount = Random.Range(1, _maxObstaclesInOneWave);
        StartCoroutine(SpawnObstacles(newObstaclesAmount));
        _timer = 0;
    }

    private IEnumerator SpawnObstacles(int obstaclesAmount)
    {
        for (int i = 0; i < obstaclesAmount; i++)
        {
            int positionIndex = i;

            if (i >= _spawnPositions.Length)
            {
                positionIndex = i % _spawnPositions.Length;
                yield return new WaitForSeconds(0.5f);
            }

            Obstacle newObstacle = Instantiate(_obstaclePrefab);
            newObstacle.transform.position = _spawnPositions[positionIndex].position;
            int randomColorIndex = Random.Range(0, _obstaclesColors.Length);
            int randomHealthPoints = Random.Range(1, _maxObstacleHealth);
            newObstacle.SetUp(randomHealthPoints, _obstaclesColors[randomColorIndex]);
            Physics.SyncTransforms();
        }
    }
}
