using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private int _numPlatformsToSpawn = 7;
    [SerializeField] private GameObject _firstPlatform;
    [SerializeField] private float _maxDistanceFromCameraBeforeSpawn = 10f;
    [SerializeField] private float _minDistanceFromCameraPreviousPlatform = 0.5f;
    [SerializeField] private float _maxDistanceFromCameraPreviousPlatform = 0.7f;
    [SerializeField] private float _xSpawnRange = 3f;
    [SerializeField] private float _platformDestroyOffset = 10f;

    private GameObject _lastSpawnedPlatform;
    private List<GameObject> _spawnedPlatforms = new List<GameObject>();
    private bool _canSpawnPlatforms = true;

    void Start()
    {
        _lastSpawnedPlatform = _firstPlatform;
        float startY = _firstPlatform.transform.position.y;
        for (int i = 0; i < _numPlatformsToSpawn; i++)
        {
            float xPosition = Random.Range(-_xSpawnRange, _xSpawnRange);
            float yPosition = startY + (i + 1) * _minDistanceFromCameraPreviousPlatform;
            Vector2 spawnPosition = new Vector2(xPosition, yPosition);
            _lastSpawnedPlatform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
            _spawnedPlatforms.Add(_lastSpawnedPlatform);
        }
    }

    void LateUpdate()
    {
        if (!_canSpawnPlatforms)
        {
            return;
        }

        if (Camera.main.transform.position.y + _maxDistanceFromCameraBeforeSpawn > _lastSpawnedPlatform.transform.position.y)
        {
            SpawnPlatforms();
        }
    }

    private void SpawnPlatforms()
    {
        // Kamera yüksekliğine göre zorluk artacak
        float cameraHeight = Camera.main.transform.position.y;
        float difficultyFactor = Mathf.Min(cameraHeight / 150f, 1.0f);
        
        // Yükseldikçe platform sayısı azalacak
        int platformCount = _numPlatformsToSpawn;
        if (cameraHeight > 100f) platformCount = 6;
        if (cameraHeight > 200f) platformCount = 5;
        if (cameraHeight > 300f) platformCount = 4;
        if (cameraHeight > 400f) platformCount = 3;
        
        for (int i = 0; i < platformCount; i++)
        {
            // Platform arası mesafe - ama çok fazla artmasın
            float baseSafeDistance = 1.2f;
            float heightBasedIncrease = (cameraHeight / 100f) * 0.2f;
            // 500+ yükseklikte mesafe artışını sınırla
            if (cameraHeight > 500f) 
            {
                heightBasedIncrease = (500f / 100f) * 0.2f;
            }
            float maxSafeDistance = baseSafeDistance + (difficultyFactor * 0.2f) + heightBasedIncrease;
            float minDistance = _minDistanceFromCameraPreviousPlatform * 0.8f;
            float distanceToNextPlatform = Random.Range(minDistance, maxSafeDistance);
            
            // X pozisyonu zorluk arttıkça daha dar alana sıkışacak
            float baseXDistance = 2.5f;
            float maxXDistance = baseXDistance - (difficultyFactor * 1.0f);
            maxXDistance = Mathf.Max(maxXDistance, 1.5f);
            float randomXVariation = Random.Range(0.7f, 1.0f);
            float actualXDistance = maxXDistance * randomXVariation;
            float xPosition = Random.Range(-actualXDistance, actualXDistance);
            
            float yPosition = _lastSpawnedPlatform.transform.position.y + distanceToNextPlatform;
            Vector2 spawnPosition = new Vector2(xPosition, yPosition);
            _lastSpawnedPlatform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
            _spawnedPlatforms.Add(_lastSpawnedPlatform);
        }
    }

    public void HandleRespawn()
    {
        _canSpawnPlatforms = true;
    }

    public void HandleDeath()
    {
        // Platformları temizle
        var toDestroy = new List<GameObject>(_spawnedPlatforms);
        foreach (var platform in toDestroy)
        {
            if (platform != null)
            {
                Destroy(platform);
            }
        }
        _spawnedPlatforms.Clear();
        
        _lastSpawnedPlatform = _firstPlatform;
        _canSpawnPlatforms = false;
    }
}
