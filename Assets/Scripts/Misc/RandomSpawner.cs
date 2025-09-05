using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private float _minX = -8f;
    [SerializeField] private float _maxX = 8f;
    [SerializeField] private float _minY = -4.5f;
    [SerializeField] private float _maxY = 4.5f;
    [SerializeField] private float _minSpawnTime = 1f;
    [SerializeField] private float _maxSpawnTime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnObjectAtRandomInterval());
    }

    private IEnumerator SpawnObjectAtRandomInterval()
    {
        while (true)
        {
            float waitTime = Random.Range(_minSpawnTime, _maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            float randomX = Random.Range(_minX, _maxX);
            float randomY = Random.Range(_minY, _maxY);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

            Instantiate(_objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
